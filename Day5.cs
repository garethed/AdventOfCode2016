using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day5
    {
        public static string Part1()
        {
            string doorId = "reyedfim";
            string password = "";


            for (int i = 0; password.Length < 8; i++)
            {
                    var md5 = System.Security.Cryptography.MD5.Create();
                    var input = doorId + i.ToString();
                    var hash = BitConverter.ToString(
                        md5.ComputeHash(Encoding.ASCII.GetBytes(input))).Replace("-", "");

                    if (hash.StartsWith("00000"))
                    {
                        password += hash[5];
                        Console.Write(hash[5]);
                    }
            }

            return password.ToLower();
        }

        public static string Part2()
        {
            string doorId = "reyedfim";
            char[] password = "________".ToCharArray();            


            for (int i = 0; Array.IndexOf(password, '_') >= 0; i++)
            {
                var md5 = System.Security.Cryptography.MD5.Create();
                var input = doorId + i.ToString();
                var hash = BitConverter.ToString(
                    md5.ComputeHash(Encoding.ASCII.GetBytes(input))).Replace("-", "");

                if (hash.StartsWith("00000"))
                {
                    int index;
                    if (int.TryParse(hash[5].ToString(), out index) && index < 8 && password[index] == '_')
                    {
                        password[index] = hash[6];                        
                        Console.Write(new string(password).ToLower());
                        Console.CursorLeft = 0;
                    }
                }
            }            

            return new string(password).ToLower();
        }
    }
}
