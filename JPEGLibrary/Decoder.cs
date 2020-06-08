using JPEGLibrary.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace JPEGLibrary
{
    public class Decoder
    {
        Encoder enc;
        public RGBBlock[,] RGBBlocks { get; set; }
        public Bitmap DecodedImage { get; set; }
        public double PSNR_R { get; set; }
        public double PSNR_G { get; set; }
        public double PSNR_B { get; set; }

        public Decoder(Encoder encodedImage)
        {
            enc = encodedImage;
        }

        public async Task Decode()
        {
            await Task.Run(() =>
            {
                DeQuantization();
                IDCT();
                ReverseColorTransformation();
                DecodedImage = ConvertBlockArrayToPicture(RGBBlocks);
                PSNR();
            });
        }

        void PSNR()
        {
            double MSE_R = 0;
            double MSE_G = 0;
            double MSE_B = 0;

            double N = enc.Image.Width * enc.Image.Height;

            for (int x = 0; x < enc.Image.Width; x++)
            {
                for (int y = 0; y < enc.Image.Height; y++)
                {
                    MSE_R += Math.Pow((enc.Image.GetPixel(x, y).R - DecodedImage.GetPixel(x, y).R), 2);
                    MSE_G += Math.Pow((enc.Image.GetPixel(x, y).G - DecodedImage.GetPixel(x, y).G), 2);
                    MSE_B += Math.Pow((enc.Image.GetPixel(x, y).B - DecodedImage.GetPixel(x, y).B), 2);
                }
            }

            MSE_R = (1.0 / N) * MSE_R;
            MSE_G = (1.0 / N) * MSE_G;
            MSE_B = (1.0 / N) * MSE_B;

            PSNR_R = 10 * Math.Log10(Math.Pow(255, 2) / MSE_R);
            PSNR_G = 10 * Math.Log10(Math.Pow(255, 2) / MSE_G);
            PSNR_B = 10 * Math.Log10(Math.Pow(255, 2) / MSE_B);
        }

        #region 1. De-Quantization
        void DeQuantization()
        {

            for (int x = 0; x < enc.BlockCount.x; x++)
            {
                for (int y = 0; y < enc.BlockCount.y; y++)
                {
                    FDCTValue[,] block = new FDCTValue[enc.Blocksize, enc.Blocksize];
                    
                    for (int u = 0; u < enc.Blocksize; u++)
                    {
                        for (int v = 0; v < enc.Blocksize; v++)
                        {
                            //Y
                            if(enc.QuantizationTables[u, v].Y == 0)
                                block[u, v].Y_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Y_Quant;
                            else
                                block[u, v].Y_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Y_Quant * enc.QuantizationTables[u, v].Y;

                            //Cb
                            if (enc.QuantizationTables[u, v].Cb == 0)
                                block[u, v].Cb_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Cb_Quant;
                            else
                                block[u, v].Cb_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Cb_Quant * enc.QuantizationTables[u, v].Cb;
                            //Cr
                            if (enc.QuantizationTables[u, v].Cr == 0)
                                block[u, v].Cr_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Cr_Quant;
                            else
                                block[u, v].Cr_FDCT = enc.QuantizationBlockCollection[x, y].Block[u, v].Cr_Quant * enc.QuantizationTables[u, v].Cr;
                        }
                    }

                    enc.FDCTBlockCollection[x, y].Block = block;

                }
            }

        }

        #endregion

        #region 2. IDCT
        void IDCT()
        {
            //YCbCrBlockCollection = new YCbCrBlock[blockCount.x, blockCount.y];

            for (int i = 0; i < enc.BlockCount.x; i++)
            {
                for (int j = 0; j < enc.BlockCount.y; j++)
                {
                    enc.YCbCrBlockCollection[i, j].Block 
                        = CreateYCbCrBlock(enc.FDCTBlockCollection[i, j].Block);
                }
            }

            YCbCr[,] CreateYCbCrBlock(FDCTValue[,] block) 
            {
                var yCbCrBlock = new YCbCr[enc.Blocksize, enc.Blocksize];

                //8x8 block von dct werten
                for (int x = 0; x < enc.Blocksize; x++)
                {
                    for (int y = 0; y < enc.Blocksize; y++)
                    {
                        //einzelnen dct wert 
                        double sum_y = 0;
                        double sum_cb = 0;
                        double sum_cr = 0;

                        for (int u = 0; u < 8; u++)
                        {
                            for (int v = 0; v < 8; v++)
                            {
                                double cu = u == 0 ? 1 / Math.Sqrt(2) : 1;
                                double cv = v == 0 ? 1 / Math.Sqrt(2) : 1;

                                double cos1 = Math.Cos((2 * x + 1) * (u * Math.PI) / 16);
                                double cos2 = Math.Cos((2 * y + 1) * (v * Math.PI) / 16);

                                sum_y += cu * cv * block[u, v].Y_FDCT * cos1 * cos2;
                                sum_cb += cu * cv * block[u, v].Cb_FDCT * cos1 * cos2;
                                sum_cr += cu * cv * block[u, v].Cr_FDCT * cos1 * cos2;
                            }
                        }

                        //einzelner ycbcr wert (Syx)
                        double Sxy_Y = 0.25 * sum_y;
                        double Sxy_Cb = 0.25 * sum_cb;
                        double Sxy_Cr = 0.25 * sum_cr;

                        yCbCrBlock[x, y].Y = Sxy_Y;
                        yCbCrBlock[x, y].Cb = Sxy_Cb;
                        yCbCrBlock[x, y].Cr = Sxy_Cr;
                    }
                }

                return yCbCrBlock;
            }

        }


        #endregion

        #region 3. RGB Transformation
        void ReverseColorTransformation()
        {
            RGBBlocks = new RGBBlock[enc.BlockCount.x, enc.BlockCount.y];

            for (int i = 0; i < enc.BlockCount.x; i++)
            {
                for (int j = 0; j < enc.BlockCount.y; j++)
                {
                    var block = new RGB[enc.Blocksize, enc.Blocksize];
                    for (int x = 0; x < enc.Blocksize; x++)
                    {
                        for (int y = 0; y < enc.Blocksize; y++)
                        {
                            var y_wert = enc.YCbCrBlockCollection[i, j].Block[x, y].Y;
                            var cb_wert = enc.YCbCrBlockCollection[i, j].Block[x, y].Cb - 127.5;
                            var cr_wert = enc.YCbCrBlockCollection[i, j].Block[x, y].Cr - 127.5;

                            var r = (y_wert * 1) + (cb_wert * 0) + (1.402 * cr_wert);
                            var g = (y_wert * 1) + (cb_wert * -0.34414) + (cr_wert * -0.71414);
                            var b = (y_wert * 1) + (cb_wert * 1.772) + (cr_wert * 0);


                            if (r > 255)
                                block[x, y].R = 255;
                            else if (r < 0)
                                block[x, y].R = 0;
                            else
                                block[x, y].R = (byte)r;

                            if (g > 255)
                                block[x, y].G = 255;
                            else if (g < 0)
                                block[x, y].G = 0;
                            else
                                block[x, y].G = (byte)g;

                            if (b > 255)
                                block[x, y].B = 255;
                            else if (b < 0)
                                block[x, y].B = 0;
                            else
                                block[x, y].B = (byte)b;

                        }
                    }

                    RGBBlocks[i, j].Block = block;
                }
            }
        }

        #endregion

        #region 4. Reverse Blocktransformation 

        Color[,] ConvertBlockToColorArray(RGB[,] block)
        {
            Color[,] output = new Color[enc.Blocksize, enc.Blocksize];

            for (int x = 0; x < enc.Blocksize; x++)
            {
                for (int y = 0; y < enc.Blocksize; y++)
                {
                    int r = block[x, y].R;
                    int g = block[x, y].G;
                    int b = block[x, y].B;

                    Color color = Color.FromArgb(r, g, b);

                    output[x, y] = color;
                }
            }

            return output;
        }

        Bitmap ConvertBlockArrayToPicture(RGBBlock[,] blocks)
        {
            Bitmap output = new Bitmap(enc.Image.Width, enc.Image.Height);

            int a = 0;
            int b = 0;

            for (int i = 0; i < enc.BlockCount.x; i++)
            {
                for (int j = 0; j < enc.BlockCount.y; j++)
                {
                    Color[,] colorBlock = ConvertBlockToColorArray(blocks[i, j].Block);

                    for (int x = 0; x < enc.Blocksize; x++)
                    {
                        for (int y = 0; y < enc.Blocksize; y++)
                        {
                            output.SetPixel(y + a, x + b, colorBlock[x, y]);
                        }
                    }
                    a += enc.Blocksize;
                }
                a = 0;
                b += enc.Blocksize;
            }

            return output;
        }

        #endregion

    }
}
