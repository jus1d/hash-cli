using System;
using System.Security.Cryptography;
using System.Text;

namespace hash_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[0][0] == '-')
                {
                    string parameter = args[0].ToLower();
                    if (parameter == "--help" || parameter == "-h")
                    {
                        Console.WriteLine("\nUsage: hash-cli [ hash-algorithm ] [ raw-data ]\n" +
                                          "\n  or\n" +
                                          "\nUsage for hashing files: hash-cli [ hash-algorithm ] --file [ file-name ]\n" +
                                          "\nFile-name parameter may be clear => will opens a window for selecting a file\n");
                    }
                }
                else
                {
                    string hashType = args[0].ToLower();
                    string rawData = args[1];
                    

                    switch (hashType)
                    {
                        case "sha256":

                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("Hashing files will available soon");
                            }
                            else
                            {
                                string hash = Sha256(rawData);
                            
                                LogHash(rawData, hashType, hash);
                            }
                            
                            break;
                        
                        case "md5":

                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("Hashing files will available soon");
                            }
                            else
                            {
                                {
                                    string hash = Md5(rawData);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Use --help or -h flags, to see usage list");
            }
        }

        static void LogHash(string rawData, string hashType, string hash)
        {
            Console.WriteLine($"{rawData}:{hashType} -> {hash}");
        }

        static string Sha256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        static string Md5(string rawData)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
    }
}