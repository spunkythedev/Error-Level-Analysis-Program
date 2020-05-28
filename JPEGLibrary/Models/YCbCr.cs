using System;

namespace JPEGLibrary.Models
{
    public struct YCbCr
    {
        public double Y { get; set; }
        public double Cb { get; set; }
        public double Cr { get; set; }

        public YCbCr(double y, double cb, double cr)
        {
            Y = y;
            Cb = cb;
            Cr = cr;
        }

        public override string ToString()
        {
            return $"({Y}, {Cb}, {Cr})";
        }
    }

    public struct YCbCrBlock
    {
        public YCbCr[,] Block { get; set; } 

        public YCbCrBlock(int blocksize)
        {
            Block = new YCbCr[blocksize, blocksize];
        }
    }
}