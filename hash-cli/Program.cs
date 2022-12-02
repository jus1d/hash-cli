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
                    string parameter = args[0];
                    if (parameter == "--help" || parameter == "-h")
                    {
                        Console.WriteLine("help command");
                    }
                }
                else
                {
                    string hashType = args[0];

                    switch (hashType)
                    {
                        case "sha256":
                            string rawData = args[1];

                            string hash = Sha256(rawData);
                            
                            Console.WriteLine($"hash:sha256 -> {hash}");
                            
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Type --help or -h, to see command list");
            }
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
    }
}