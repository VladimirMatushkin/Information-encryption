using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Steganography
{
    class Program
    {
        const string PathToImages = "images\\";
        const string Arguments = "/c java Embed -c \"\" -e bin.noise ..\\" + PathToImages;

        static void Main(string[] args)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WorkingDirectory = "f5";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            //process.StartInfo.RedirectStandardError = true;

            DirectoryInfo directoryInfo = new DirectoryInfo(PathToImages);
            FileInfo[] images = directoryInfo.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);

            Regex regexEmbedded = new Regex("embedded", RegexOptions.RightToLeft | RegexOptions.Compiled);
            Regex regexBits = new Regex("[0-9]+", RegexOptions.Compiled);
            Regex regexResolution = new Regex("[0-9]+ x [0-9]+", RegexOptions.Compiled);

            using (StreamWriter sw = new StreamWriter("result.csv", false, Encoding.UTF8))
            {
                sw.WriteLineAsync("File name,Resolution,Size of embeddeded information(bits),Size of base image(bits),% of embeddeded information");

                foreach (FileInfo image in images)
                {
                    process.StartInfo.Arguments = Arguments + image.Name;
                    process.Start();

                    string resolution = null;
                    ulong embeddedBits = 0;
                    while (process.StandardOutput.EndOfStream == false)
                    {
                        string output = process.StandardOutput.ReadLine();
                        Console.WriteLine(output);
                        if (resolution == null && regexResolution.IsMatch(output))
                        {
                            resolution = regexResolution.Match(output).Value;
                        }
                        else if (regexEmbedded.IsMatch(output))
                        {
                            embeddedBits = ulong.Parse(regexBits.Match(output).Value);
                            Console.WriteLine(process.StandardOutput.ReadToEnd());
                            break;
                        }
                    }
                    //string err = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    sw.WriteLineAsync($"{image.Name},{resolution},{embeddedBits},{image.Length * 8},"
                                      + $"{embeddedBits * 100 / (double)(image.Length * 8):0.00}");
                    //process.Close();
                }
            }
        }
    }
}
