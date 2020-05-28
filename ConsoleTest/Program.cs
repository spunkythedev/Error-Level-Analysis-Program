using ConsoleTest.Properties;
using JPEGLibrary;
using JPEGLibrary.Models;
using System;
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
                    imageName: "Lena", 
                    quality: 75, 
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

                decoder.DecodedImage.Save($"{ encodedImage.ImageName }_quality={ encodedImage.Quality}.png", ImageFormat.Png);

                #endregion

                Environment.Exit(0);
            });

            Console.ReadLine();
        }
    }


}
