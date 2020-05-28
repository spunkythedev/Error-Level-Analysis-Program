using JPEGLibrary.Models;
using System;
using System.IO;
using System.Text;

namespace JPEGLibrary.Util
{
    public class TxtHelper
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public string ImageName { get; set; }

        public TxtHelper(string imageName)
        {
            ImageName = imageName;
        }

        internal void SaveYCbCrBlockCollection(YCbCrBlock[,] input)
        {
            Console.WriteLine($"Saving YCbCr values of {ImageName} to {path}");

            StringBuilder yBlockTxt = new StringBuilder();
            StringBuilder cbBlockTxt = new StringBuilder();
            StringBuilder crBlockTxt = new StringBuilder();

            int blockNumber = 1;
            for (int x = 0; x < input.GetLength(0); x++)
            {
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    var block = input[x, y].Block;

                    yBlockTxt.AppendLine( $"Block: {blockNumber}");
                    cbBlockTxt.AppendLine($"Block: {blockNumber}");
                    crBlockTxt.AppendLine($"Block: {blockNumber}");

                    blockNumber++;

                    for (int i = 0; i < block.GetLength(0); i++)
                    {
                        for (int j = 0; j < block.GetLength(1); j++)
                        {
                            yBlockTxt.Append($"{block[i, j].Y}; ");
                            cbBlockTxt.Append($"{block[i, j].Cb}; ");
                            crBlockTxt.Append($"{block[i, j].Cr}; ");
                        }
                    }

                    yBlockTxt.AppendLine();
                    cbBlockTxt.AppendLine();
                    crBlockTxt.AppendLine();
                }
            }

            File.WriteAllText(Path.Combine(path, $"{ImageName}_y.txt"), yBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cb.txt"), cbBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cr.txt"), crBlockTxt.ToString());

        }

        internal void SaveFDCTBlockCollection(FDCTBlock[,] input)
        {
            Console.WriteLine($"Saving YCbCr values of {ImageName} to {path}");

            StringBuilder ydctBlockTxt = new StringBuilder();
            StringBuilder cbdctBlockTxt = new StringBuilder();
            StringBuilder crdctBlockTxt = new StringBuilder();

            int blockNumber = 1;
            for (int x = 0; x < input.GetLength(0); x++)
            {
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    var block = input[x, y].Block;

                    ydctBlockTxt.AppendLine($"Block: {blockNumber}");
                    cbdctBlockTxt.AppendLine($"Block: {blockNumber}");
                    crdctBlockTxt.AppendLine($"Block: {blockNumber}");

                    blockNumber++;

                    for (int i = 0; i < block.GetLength(0); i++)
                    {
                        for (int j = 0; j < block.GetLength(1); j++)
                        {
                            ydctBlockTxt.Append($"{block[i, j].Y_FDCT}; ");
                            cbdctBlockTxt.Append($"{block[i, j].Cb_FDCT}; ");
                            crdctBlockTxt.Append($"{block[i, j].Cr_FDCT}; ");
                        }
                    }

                    ydctBlockTxt.AppendLine();
                    cbdctBlockTxt.AppendLine();
                    crdctBlockTxt.AppendLine();
                }
            }

            File.WriteAllText(Path.Combine(path, $"{ImageName}_y_dct.txt"), ydctBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cb_dct.txt"), cbdctBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cr_dct.txt"), crdctBlockTxt.ToString());

        }

        internal void SaveQuantisationBlockCollection(QuantizationBlock[,] input)
        {

            Console.WriteLine($"Saving Quantisation values of {ImageName} to {path}");

            StringBuilder yQuantiBlockTxt = new StringBuilder();
            StringBuilder cbQuantiBlockTxt = new StringBuilder();
            StringBuilder crQuantiBlockTxt = new StringBuilder();

            int blockNumber = 1;
            for (int x = 0; x < input.GetLength(0); x++)
            {
                for (int y = 0; y < input.GetLength(1); y++)
                {
                    var block = input[x, y].Block;

                    yQuantiBlockTxt.AppendLine($"Block: {blockNumber}");
                    cbQuantiBlockTxt.AppendLine($"Block: {blockNumber}");
                    crQuantiBlockTxt.AppendLine($"Block: {blockNumber}");

                    blockNumber++;

                    for (int i = 0; i < block.GetLength(0); i++)
                    {
                        for (int j = 0; j < block.GetLength(1); j++)
                        {
                            yQuantiBlockTxt.Append($"{block[i, j].Y_Quant}; ");
                            cbQuantiBlockTxt.Append($"{block[i, j].Cb_Quant}; ");
                            crQuantiBlockTxt.Append($"{block[i, j].Cr_Quant}; ");
                        }
                    }

                    yQuantiBlockTxt.AppendLine();
                    cbQuantiBlockTxt.AppendLine();
                    crQuantiBlockTxt.AppendLine();
                }
            }

            File.WriteAllText(Path.Combine(path, $"{ImageName}_y_qdct.txt"), yQuantiBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cb_qdct.txt"), cbQuantiBlockTxt.ToString());
            File.WriteAllText(Path.Combine(path, $"{ImageName}_cr_qdct.txt"), crQuantiBlockTxt.ToString());

        }
    }
}
