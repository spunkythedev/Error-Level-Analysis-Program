using System;
using System.Collections.Generic;
using System.Text;

namespace JPEGLibrary.Models
{
    public struct FDCTValue
    {
        public double Y_FDCT { get; set; }
        public double Cb_FDCT { get; set; }
        public double Cr_FDCT { get; set; }

        public FDCTValue(double y_fdct, double cb_fdct, double cr_fdct)
        {
            Y_FDCT = y_fdct;
            Cb_FDCT = cb_fdct;
            Cr_FDCT = cr_fdct;
        }

        public override string ToString()
        {
            return $"({Y_FDCT:F2}, {Cb_FDCT:F2}, {Cr_FDCT:F2})";
        }
    }
    public struct FDCTBlock
    {
        public FDCTValue[,] Block { get; set; }

        public FDCTBlock(int blocksize)
        {
            Block = new FDCTValue[blocksize, blocksize];
        }
    }
}
