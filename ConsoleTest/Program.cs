using ConsoleTest.Properties;
using JPEGLibrary;
using JPEGLibrary.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace JPEG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...\n");

            Task.Run(async () =>
            {
                JPEGLibrary.Encoder encodedImage = new JPEGLibrary.Encoder(
                    image: Resources.lena,
                    imageName: "Dino",
                    quality: 50,
                    saveTextfiles: false);

                await encodedImage.Encode();

                #region Print
                Console.WriteLine($"Quality: \n{encodedImage.Quality}\n");
                Console.WriteLine("Entropy: ");
                foreach (var value in encodedImage.Entropy.GetPropertyValues())
                {
                    Console.WriteLine(value);
                };
                #endregion

                Decoder decoder = new Decoder(encodedImage);
                await decoder.Decode();

                #region Print/Save
                Console.WriteLine("\nPSNR: ");
                Console.WriteLine(decoder.PSNR_R + " db");
                Console.WriteLine(decoder.PSNR_G + " db");
                Console.WriteLine(decoder.PSNR_B + " db");

                string output = $"{ encodedImage.ImageName }_quality={ encodedImage.Quality}.png";
                decoder.DecodedImage.Save(output, ImageFormat.Png);

                //ELA


                #endregion

                Environment.Exit(0);
            });

            Ela();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void Ela()
        {
            int M = 30;
            string path_input = @"C:\Users\Christoph\Desktop\original.jpg";
            string reproduziertesBildPfad = @"C:\Users\Christoph\Desktop\rep_q75.png";

            Bitmap original = new Bitmap(path_input);
            Bitmap reproduziert = new Bitmap(reproduziertesBildPfad);

            Bitmap output = new Bitmap(reproduziert.Width, reproduziert.Height);

            for (int x = 0; x < output.Width; x++)
            {
                for (int y = 0; y < output.Height; y++)
                {
                    //double difference_r = original.GetPixel(x, y).R - reproduziert.GetPixel(x, y).R;
                    //double abs_r = Math.Abs(difference_r);
                    //double r = (abs_r) * M;


                    //double difference_g = original.GetPixel(x, y).G - reproduziert.GetPixel(x, y).G;
                    //double abs_g = Math.Abs(difference_g);
                    //double g = (abs_g) * M;


                    //double difference_b = original.GetPixel(x, y).B - reproduziert.GetPixel(x, y).B;
                    //double abs_b = Math.Abs(difference_b);
                    //double b = (abs_b) * M;

                    double difference_r = original.GetPixel(x, y).R - reproduziert.GetPixel(x, y).R ;
                    double abs_r = Math.Abs(difference_r);
                    double r = (abs_r) * M;


                    double difference_g = original.GetPixel(x, y).G - reproduziert.GetPixel(x, y).G;
                    double abs_g = Math.Abs(difference_g);
                    double g = (abs_g) * M;


                    double difference_b = original.GetPixel(x, y).B - reproduziert.GetPixel(x, y).B;
                    double abs_b = Math.Abs(difference_b);
                    double b = (abs_b) * M;

                    if (r > 255)
                        r = 255;

                    if (g > 255)
                        g = 255;

                    if (b > 255)
                        b = 255;

                    Color color = Color.FromArgb((byte)r, (byte)g, (byte)b);
                    output.SetPixel(x, y, color);
                }
            }

            output.Save("ELA.png");
        }
    }


}
