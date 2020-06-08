using JPEGLibrary.Models;
using JPEGLibrary.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace JPEGLibrary
{
    public class Encoder
    {
        #region Fields
        private string imagePath;
        private bool saveTextFiles;
        private int quality;
        #endregion

        #region Properties
        //Image
        public Bitmap Image { get; set; }
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                Image = new Bitmap(value);
            }
        }
        public string ImageName { get; set; }
        public int Quality
        {
            get => quality;
            set
            {
                if (value > 0 && value <= 100)
                {
                    quality = value;
                }
                else
                {
                    throw new ArgumentException("Quality must be between 0 and 100.");
                }
            }
        }
        public (int x, int y) BlockCount { get; set; }
        public int Blocksize { get; private set; } = 8;

        //Collections/Calculated Values
        public RGB[,] RGBArray { get; set; }
        public YCbCr[,] YCbCrArray { get; set; }
        public YCbCrBlock[,] YCbCrBlockCollection { get; set; }
        public FDCTBlock[,] FDCTBlockCollection { get; set; }
        public QuantizationBlock[,] QuantizationBlockCollection { get; set; }
        public QuantizationTableValue[,] QuantizationTables { get; set; }
        public Entropy Entropy { get => CalculateEntropies(); }

        //Save to textfile/ entropy
        public bool SaveTextFiles
        {
            get => saveTextFiles;
            set
            {
                saveTextFiles = value;
                if (saveTextFiles)
                {
                    TxtHelper = new TxtHelper(ImageName);
                }
            }
        }
        public TxtHelper TxtHelper { get; set; }


        #endregion

        #region Constructors

        //constructor for image from filesystem
        public Encoder(string path, int quality, bool saveTextfiles = false)
        {
            ImagePath = path;
            ImageName = Path.GetFileNameWithoutExtension(path);
            SaveTextFiles = saveTextfiles;
            Quality = quality;
        }

        //for Image as input (from Winforms picturebox)
        public Encoder(Image image, string path, int quality, bool saveTextfiles = false)
        {
            Image = new Bitmap(image);
            ImageName = Path.GetFileNameWithoutExtension(path);
            SaveTextFiles = saveTextfiles;
            Quality = quality;
        }

        //for Bitmap as input (from resources)
        public Encoder(Bitmap image, string imageName, int quality, bool saveTextfiles = false)
        {
            Image = image;
            ImageName = imageName;
            SaveTextFiles = saveTextfiles;
            Quality = quality;
        }

        //Default logic for all constructors
        Encoder(int quality, bool saveTextfiles)
        {
            
        }

        #endregion

        public async Task Encode()
        {
            await Task.Run(() =>
            {
                //1. Colortransformation - After this method, the properties RGBArray and YCbCrArray are filled with values
                RGBtoYCbCr();

                //2. Blocktransformation - After this method, the Image is splitted in a 2D Array of 8 x 8 Blocks
                BlockTransformation();

                //3. DCT - After this method, each 8 x 8 Block creates a new FDCTBlock
                FDCT();

                //4. Calculates Tables
                CalculateQuantizationTable();

                //5. Quantization
                Quantization();
            });

        }

        #region 1. YCbCr Transformation
        void RGBtoYCbCr()
        {
            //initialzes empty RGB and YCbCr 2D-Array
            RGBArray = new RGB[Image.Width, Image.Height];
            YCbCrArray = new YCbCr[Image.Width, Image.Height];

            //Iterates over all Pixels from Input Image starting from (0,0) -> Top Left
            for (int x = 0; x < Image.Width; x++)
            {
                for (int y = 0; y < Image.Height; y++)
                {
                    //Gets pixel of each position
                    Color pixel = Image.GetPixel(x, y);

                    //Adds each value of pixel to new RGB Class
                    RGB rgb = new RGB(pixel.R, pixel.G, pixel.B);

                    //Calculates YCbCr Values
                    var yData = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    var cbData = (-0.169 * pixel.R - 0.331 * pixel.G + 0.5 * pixel.B) + 127.5;
                    var crData = (0.5 * pixel.R - 0.419 * pixel.G - 0.081 * pixel.B) + 127.5;

                    //Adds each value of pixel to new YCbCr Class
                    YCbCr yCbCr = new YCbCr(yData, cbData, crData);

                    //Adds each value of Arrays
                    RGBArray[x, y] = rgb;
                    YCbCrArray[x, y] = yCbCr;
                }
            }

        }

        #endregion

        #region 2. Blocktransformation
        //Splits 2D YCbCr Array in 2D Array with 
        void BlockTransformation()
        {
            if (Image.Width % Blocksize != 0 || Image.Height % Blocksize != 0)
            {
                throw new NotImplementedException("Imagesize can't be devided by blocksize. Padding not yet implemented");
            }

            BlockCount = (x: Image.Height / Blocksize,
                          y: Image.Width / Blocksize);

            YCbCrBlockCollection = new YCbCrBlock[BlockCount.x, BlockCount.y];

            int a = 0;
            int b = 0;
            int block_x = 0;
            int block_y = 0;

            for (int height = 0; height < Image.Height; height += Blocksize)
            {
                block_y = 0;
                for (int width = 0; width < Image.Width; width += Blocksize)
                {
                    a = 0;
                    YCbCr[,] block = new YCbCr[Blocksize, Blocksize];

                    for (int x = width; x < width + Blocksize; x++)
                    {
                        b = 0;
                        for (int y = height; y < height + Blocksize; y++)
                        {
                            block[a, b] = YCbCrArray[width + b, height + a];
                            b++;
                        }
                        a++;
                    }

                    YCbCrBlockCollection[block_x, block_y].Block = block;
                    block_y++;
                }

                block_x++;
            }

            TxtHelper?.SaveYCbCrBlockCollection(YCbCrBlockCollection);
        }
        #endregion

        #region 3. Fast DCT
        void FDCT()
        {
            FDCTBlockCollection = new FDCTBlock[BlockCount.x, BlockCount.y];

            for (int i = 0; i < BlockCount.x; i++)
            {
                for (int j = 0; j < BlockCount.y; j++)
                {
                    FDCTBlockCollection[i, j].Block = CreateFDCTBlock(YCbCrBlockCollection[i, j].Block);
                }
            }

            FDCTValue[,] CreateFDCTBlock(YCbCr[,] block)
            {
                FDCTValue[,] blockvalues = new FDCTValue[Blocksize, Blocksize];

                for (int u = 0; u < Blocksize; u++)
                {
                    for (int v = 0; v < Blocksize; v++)
                    {
                        double cu = u == 0 ? 1 / Math.Sqrt(2) : 1;
                        double cv = v == 0 ? 1 / Math.Sqrt(2) : 1;

                        double sum_y = 0;
                        double sum_cb = 0;
                        double sum_cr = 0;

                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                double cos1 = Math.Cos((2 * x + 1) * (u * Math.PI) / 16);
                                double cos2 = Math.Cos((2 * y + 1) * (v * Math.PI) / 16);

                                sum_y += block[x, y].Y * cos1 * cos2;
                                sum_cb += block[x, y].Cb * cos1 * cos2;
                                sum_cr += block[x, y].Cr * cos1 * cos2;
                            }
                        }

                        double Svu_Y = 0.25 * cu * cv * sum_y;
                        double Svu_Cb = 0.25 * cu * cv * sum_cb;
                        double Svu_Cr = 0.25 * cu * cv * sum_cr;

                        //2D-Array hinzufügen
                        blockvalues[u, v].Y_FDCT = Svu_Y;
                        blockvalues[u, v].Cb_FDCT = Svu_Cb;
                        blockvalues[u, v].Cr_FDCT = Svu_Cr;
                    }
                }

                return blockvalues;

            }

            TxtHelper?.SaveFDCTBlockCollection(FDCTBlockCollection);
        }
        #endregion

        #region 4. Quantization
        void CalculateQuantizationTable()
        {
            QuantizationTables = new QuantizationTableValue[8, 8];

            var S = Quality < 50
                  ? 5000 / Quality
                  : 200 - (2 * Quality);

            for (int u = 0; u < 8; u++)
            {
                for (int v = 0; v < 8; v++)
                {
                    int Quv_y = (S * Models.QuantizationTables.Luminance[u, v] + 50) / 100;
                    int Quv_cb = (S * Models.QuantizationTables.Chrominance[u, v] + 50) / 100;
                    var Quv_cr = Quv_cb;

                    QuantizationTables[u, v] = new QuantizationTableValue
                    {
                        Y = Quv_y,
                        Cb = Quv_cb,
                        Cr = Quv_cr
                    };

                    //double Quv_y = (S * Models.QuantizationTables.Luminance[u, v] + 50.0) / 100.0;
                    //double Quv_cb = (S * Models.QuantizationTables.Chrominance[u, v] + 50.0) / 100.0;
                    //double Quv_cr = Quv_cb;

                    //QuantizationTables[u, v] = new QuantizationTableValue
                    //{
                    //    //Y = (int)Math.Round(Quv_y, MidpointRounding.AwayFromZero),
                    //    //Cb = (int)Math.Round(Quv_cb, MidpointRounding.AwayFromZero),
                    //    //Cr = (int)Math.Round(Quv_cr, MidpointRounding.AwayFromZero)
                    //    Y = (int)Math.Round(Quv_y),
                    //    Cb = (int)Math.Round(Quv_cb),
                    //    Cr = (int)Math.Round(Quv_cr)
                    //};
                }
            }
        }

        void Quantization()
        {
            QuantizationBlockCollection = new QuantizationBlock[BlockCount.x, BlockCount.y];

            double Sq_y;
            double Sq_cb;
            double Sq_cr;

            //alle blöcke
            for (int x = 0; x < BlockCount.x; x++)
            {
                for (int y = 0; y < BlockCount.y; y++)
                {
                    //einzelner 8 x 8 block
                    QuantizationValue[,] block = new QuantizationValue[Blocksize, Blocksize];
                    for (int u = 0; u < Blocksize; u++)
                    {
                        for (int v = 0; v < Blocksize; v++)
                        {

                            if (QuantizationTables[u, v].Y == 0)
                                Sq_y = FDCTBlockCollection[x, y].Block[u, v].Y_FDCT;
                            else
                                Sq_y = FDCTBlockCollection[x, y].Block[u, v].Y_FDCT / QuantizationTables[u, v].Y;


                            if (QuantizationTables[u, v].Cb == 0)
                                Sq_cb = FDCTBlockCollection[x, y].Block[u, v].Cb_FDCT;
                            else
                                Sq_cb = FDCTBlockCollection[x, y].Block[u, v].Cb_FDCT / QuantizationTables[u, v].Cb;


                            if (QuantizationTables[u, v].Cr == 0)
                                Sq_cr = FDCTBlockCollection[x, y].Block[u, v].Cr_FDCT;
                            else
                                Sq_cr = FDCTBlockCollection[x, y].Block[u, v].Cr_FDCT / QuantizationTables[u, v].Cr;

                            block[u, v].Y_Quant = (int)Math.Round(Sq_y);
                            block[u, v].Cb_Quant = (int)Math.Round(Sq_cb);
                            block[u, v].Cr_Quant = (int)Math.Round(Sq_cr);
                        }
                    }

                    QuantizationBlockCollection[x, y].Block = block;
                }
            }

            TxtHelper?.SaveQuantisationBlockCollection(QuantizationBlockCollection);
        }
        public double CalculateQualityValue()
        {
            if (QuantizationTables == null)
            {
                throw new NullReferenceException("Image not yet encoded.");
            }
            int sum_y = 0;
            int sum_cb = 0;
            int sum_cr = 0;

            for (int u = 0; u < 8; u++)
            {
                for (int v = 0; v < 8; v++)
                {
                    if (u == 0 && v == 0)
                    {
                        continue;
                    }

                    sum_y += QuantizationTables[u, v].Y;
                    sum_cb += QuantizationTables[u, v].Cb;
                    sum_cr += QuantizationTables[u, v].Cr;
                }
            }

            double µ_y = sum_y / (QuantizationTables.Length - 1);
            double µ_cb = sum_cb / (QuantizationTables.Length - 1);
            double µ_cr = sum_cr / (QuantizationTables.Length - 1);

            double µ = (µ_y + µ_cb + µ_cr) / 3;

            double q = 1 - ((double)Quality / 100);
            q = 0.49;

            double D = (Math.Abs(µ_y - µ_cb) * q)
                     + (Math.Abs(µ_y - µ_cr) * q);

            return 100 - µ + D;
        }
        #endregion

        #region Entropy
        Entropy CalculateEntropies()
        {
            Entropy output = new Entropy(Image.Width, Image.Height);

            Dictionary<int, int> histogram_r = new Dictionary<int, int>();
            Dictionary<int, int> histogram_g = new Dictionary<int, int>();
            Dictionary<int, int> histogram_b = new Dictionary<int, int>();

            Dictionary<int, int> histogram_y = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cb = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cr = new Dictionary<int, int>();

            Dictionary<int, int> histogram_y_dct = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cb_dct = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cr_dct = new Dictionary<int, int>();

            Dictionary<int, int> histogram_y_quanti = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cb_quanti = new Dictionary<int, int>();
            Dictionary<int, int> histogram_cr_quanti = new Dictionary<int, int>();

            for (int x = 0; x < Image.Width; x++)
            {
                for (int y = 0; y < Image.Height; y++)
                {
                    output.PopulateHistogram(histogram_r, RGBArray[x, y].R);
                    output.PopulateHistogram(histogram_g, RGBArray[x, y].G);
                    output.PopulateHistogram(histogram_b, RGBArray[x, y].B);
                }
            }

            for (int i = 0; i < BlockCount.x; i++)
            {
                for (int j = 0; j < BlockCount.y; j++)
                {
                    //block
                    for (int x = 0; x < Blocksize; x++)
                    {
                        for (int y = 0; y < Blocksize; y++)
                        {
                            
                            output.PopulateHistogram(histogram_y, YCbCrBlockCollection[i, j].Block[x, y].Y);
                            output.PopulateHistogram(histogram_cb, YCbCrBlockCollection[i, j].Block[x, y].Cb);
                            output.PopulateHistogram(histogram_cr, YCbCrBlockCollection[i, j].Block[x, y].Cr);

                            output.PopulateHistogram(histogram_y_dct, FDCTBlockCollection[i, j].Block[x, y].Y_FDCT);
                            output.PopulateHistogram(histogram_cb_dct, FDCTBlockCollection[i, j].Block[x, y].Cb_FDCT);
                            output.PopulateHistogram(histogram_cr_dct, FDCTBlockCollection[i, j].Block[x, y].Cr_FDCT);

                            //output.PopulateHistogramWithDouble(histogram_y_dct, FDCTBlockCollection[i, j].Block[x, y].Y_FDCT);
                            //output.PopulateHistogramWithDouble(histogram_cb_dct, FDCTBlockCollection[i, j].Block[x, y].Cb_FDCT);
                            //output.PopulateHistogramWithDouble(histogram_cr_dct, FDCTBlockCollection[i, j].Block[x, y].Cr_FDCT);

                            output.PopulateHistogram(histogram_y_quanti, QuantizationBlockCollection[i, j].Block[x, y].Y_Quant);
                            output.PopulateHistogram(histogram_cb_quanti, QuantizationBlockCollection[i, j].Block[x, y].Cb_Quant);
                            output.PopulateHistogram(histogram_cr_quanti, QuantizationBlockCollection[i, j].Block[x, y].Cr_Quant);
                        }
                    }
                }
            }

            output.R = output.CalculateEntropyFromHistogram(histogram_r);
            output.G = output.CalculateEntropyFromHistogram(histogram_g);
            output.B = output.CalculateEntropyFromHistogram(histogram_b);

            output.Y = output.CalculateEntropyFromHistogram(histogram_y);
            output.Cb = output.CalculateEntropyFromHistogram(histogram_cb);
            output.Cr = output.CalculateEntropyFromHistogram(histogram_cr);

            output.Y_dct = output.CalculateEntropyFromHistogram(histogram_y_dct);
            output.Cb_dct = output.CalculateEntropyFromHistogram(histogram_cb_dct);
            output.Cr_dct = output.CalculateEntropyFromHistogram(histogram_cr_dct);

            output.Y_quanti = output.CalculateEntropyFromHistogram(histogram_y_quanti);
            output.Cb_quanti = output.CalculateEntropyFromHistogram(histogram_cb_quanti);
            output.Cr_quanti = output.CalculateEntropyFromHistogram(histogram_cr_quanti);

            return output;
        }
        #endregion
    }
}