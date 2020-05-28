using System;
using System.Collections.Generic;
using System.Text;

namespace JPEGLibrary.Models
{
    public struct QuantizationValue
    {
        public int Y_Quant { get; set; }
        public int Cb_Quant { get; set; }
        public int Cr_Quant { get; set; }

        public QuantizationValue(int y_fdct, int cb_fdct, int cr_fdct)
        {
            Y_Quant = y_fdct;
            Cb_Quant = cb_fdct;
            Cr_Quant = cr_fdct;
        }
    }
    public struct QuantizationBlock
    {
        public QuantizationValue[,] Block { get; set; }

        public QuantizationBlock(int blocksize)
        {
            Block = new QuantizationValue[blocksize, blocksize];
        }
    }
}
