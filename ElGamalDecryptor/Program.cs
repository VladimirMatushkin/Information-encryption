using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElGamalDecryptor
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Regex regex = new Regex("[0-9]+");

            using (StreamReader sr = new StreamReader(args[0]))
            {
                long p = long.Parse(regex.Match(sr.ReadLine()).Value);
                long q = long.Parse(regex.Match(sr.ReadLine()).Value);
                long y = long.Parse(regex.Match(sr.ReadLine()).Value);
                sr.ReadLine();

                
            }
        }
    }
}
