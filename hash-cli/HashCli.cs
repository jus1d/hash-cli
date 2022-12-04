using System.Security.Cryptography;
using System.Text;

namespace hash_cli
{
    class HashCli
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

                                try
                                {
                                    string path = args[2];
                                    
                                    string hash = FileSha256(path);

                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine("Type path to file after --file flag");
                                }
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
                                try
                                {
                                    string path = args[2];

                                    string hash = FileMd5(path);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine("Type path to file after --file flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = Md5(rawData);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha1":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = FileSha1(path);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine("Type path to file after --file flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = Sha1(rawData);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha384":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = FileSha384(path);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine("Type path to file after --file flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = Sha384(rawData);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha512":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = FileSha512(path);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    Console.WriteLine("Type path to file after --file flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = Sha512(rawData);
                                    
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
        
        static string Sha1(string rawData)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        
        static string Sha256(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        
        static string Sha384(string rawData)
        {
            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = sha384.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        
        static string Sha512(string rawData)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        
        static string Md5(string rawData)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(rawData);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        
        static string FileSha1(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var sha = SHA1.Create();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            }
        }
        
        static string FileSha256(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var sha = SHA256.Create();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            }
        }
        
        static string FileSha384(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var sha = SHA384.Create();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            }
        }
        
        static string FileSha512(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var sha = SHA512.Create();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            }
        }

        static string FileMd5(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            {
                var md5 = MD5.Create();
                byte[] checksum = md5.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
            }
        }
    }
}