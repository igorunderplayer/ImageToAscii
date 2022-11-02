using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageToAscii
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            string? imagePath = args.FirstOrDefault();

            if (imagePath is null)
            {
                Console.WriteLine("You must pass a image path parameter");
                Environment.Exit(1);
            }

            using (Image image = Image.Load(imagePath))
            {
                image.Mutate(img => img.Resize(Console.WindowWidth / 2, Console.WindowHeight ));
                using (var imageB = image.CloneAs<Rgb24>())
                {
                    imageB.ProcessPixelRows(accessor =>
                    {
                        for (var y = 0; y < image.Height; y++)
                        {

                            var row = accessor.GetRowSpan(y);


                            for (var x = 0; x < image.Width; x++)
                            {
                                var color = row[x];

                                var whiteness = (color.R + color.G + color.B) / 3;

                                if (whiteness > 140) Console.Write('#');
                                else if (whiteness > 80) Console.Write('*');
                                else if (whiteness > 40) Console.Write('=');
                                else if (whiteness > 20) Console.Write('-');

                                else Console.Write(" ");
                            }

                            Console.WriteLine();
                        }
                    });
                }
            }
        }
    }
}