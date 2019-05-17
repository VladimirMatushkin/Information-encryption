using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            Regex regexEmbedded = new Regex(@"([0-9]+) bits \([0-9]+ bytes\) embedded", RegexOptions.RightToLeft | RegexOptions.Compiled);
            Regex regexResolution = new Regex("[0-9]+ x [0-9]+", RegexOptions.Compiled);

            DirectoryInfo directoryInfo = new DirectoryInfo(PathToImages);
            FileInfo[] images = directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly);
            
            Console.Write("Specify delimiter in csv file: ");
            string delimiter = Char.ConvertFromUtf32(Console.Read());
            string header = string.Join(delimiter, "File name", "Resolution", "Size of embeddeded information(bits)",
                                        "Size of base image(bits)", "% of embeddeded information");

            using (FileSystemWatcher watcher = new FileSystemWatcher(PathToImages, "*.jpg"))
            using(StreamWriter sw = new StreamWriter("result.csv", false, Encoding.UTF8))
            {
                sw.WriteLineAsync(header);
                //watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;

                foreach (FileInfo image in images)
                {
                    process.StartInfo.Arguments = Arguments + image.Name;
                    process.Start();

                    Task<string> readOutput = process.StandardOutput.ReadToEndAsync();

                    WaitForChangedResult result = watcher.WaitForChanged(WatcherChangeTypes.Created);
                    string pathToImage = PathToImages + result.Name;

                    readOutput.Wait();
                    string output = readOutput.Result;

                    string resolution = regexResolution.Match(output).Value;
                    ulong embeddedBits = ulong.Parse(regexEmbedded.Match(output).Groups[1].Value);

                    process.WaitForExit();

                    long length = new FileInfo(pathToImage).Length;

                    string line = string.Join(delimiter, image.Name, resolution, embeddedBits, length * 8, 
                            string.Format("{0:0.00}", (embeddedBits * 100) / (double)(length * 8)));
                    sw.WriteLineAsync(line);

                    Console.WriteLine(result.Name);

                    File.Delete(pathToImage);
                }
            }
        }
    }
}
