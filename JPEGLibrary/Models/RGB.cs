using System;

namespace JPEGLibrary.Models
{
    public struct RGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public override string ToString()
        {
            return $"({R}, {G}, {B})";
        }
    }

    public struct RGBBlock
    {
        public RGB[,] Block { get; set; }

        public RGBBlock(int blocksize)
        {
            Block = new RGB[blocksize, blocksize];
        }
    }
}