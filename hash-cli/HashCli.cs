using hash_cli.Hash;
using static hash_cli.Algorithm;
using static hash_cli.Generator;

namespace hash_cli
{
    class HashCli
    {
        public const ConsoleColor FlagColor = ConsoleColor.DarkGray;
        public const ConsoleColor ErrorColor = ConsoleColor.DarkRed;
        public const ConsoleColor SuccessColor = ConsoleColor.Green;
        
        
        static void Main(string[] args)
        {
            Start(args);
        }

        private static void Start(string[] args)
        {
            try
            {
                if (args[0][0] == '-')
                {
                    string parameter = args[0].ToLower();
                    if (parameter == "--help" || parameter == "-h")
                    {
                        
                        WriteColored("\nUsage: hash-cli [ hash-algorithm ] [ raw-data ]\n", FlagColor,
                                     "\n  or\n",
                                     "\nUsage for hashing files: hash-cli [ hash-algorithm ] ", FlagColor,  "--file", " [ file-name ]\n" +
                                     "\nFile-name parameter may be clear => will opens a window for selecting a file\n");
                    }
                    else if (parameter == "--test" || parameter == "-t")
                    {
                        Tests.Run();
                    }
                }
                else
                {
                    string hashType = args[0].ToLower();
                    string rawData = args[1];
                    

                    switch (hashType)
                    {
                        case "sha1":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha1, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColored("Type path to file after ", FlagColor, "--file", " flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha1, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "sha256":

                            if (rawData == "--file" || rawData == "-f")
                            {

                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha256, path, true);

                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    
                                    
                                    WriteColored("Type path to file after ", FlagColor, "--file", " flag");
                                }
                            }
                            else
                            {
                                string hash = HashCompute(Sha256, rawData, false);
                            
                                LogHash(rawData, hashType, hash);
                            }
                            
                            break;
                        
                        case "sha384":
                            
                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Sha384, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColored("Type path to file after ", FlagColor, "--file", " flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha384, rawData, false);
                                    
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

                                    string hash = HashCompute(Sha512, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColored("Type path to file after ", FlagColor, "--file", " flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Sha512, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        
                        case "md5":

                            if (rawData == "--file" || rawData == "-f")
                            {
                                try
                                {
                                    string path = args[2];

                                    string hash = HashCompute(Md5, path, true);
                                    
                                    LogHash($"file:{path}", hashType, hash);
                                }
                                catch (IndexOutOfRangeException e)
                                {
                                    WriteColored("Type path to file after ", FlagColor, "--file", " flag");
                                }
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Md5, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        case "keccak224":
                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("soon...");
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Keccak224, rawData, false);

                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        case "keccak256":
                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("soon...");
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Keccak256, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        case "keccak384":
                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("soon...");
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Keccak384, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                        case "keccak512":
                            if (rawData == "--file" || rawData == "-f")
                            {
                                Console.WriteLine("soon...");
                            }
                            else
                            {
                                {
                                    string hash = HashCompute(Keccak512, rawData, false);
                                    
                                    LogHash(rawData, hashType, hash);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                WriteColored("Use ", FlagColor, "--flag", " or ", FlagColor, "-h", " flags, to see usage list");
            }
        }

        static void LogHash(string rawData, string hashType, string hash)
        {
            WriteColored(FlagColor, $"{rawData}:{hashType} -> ", hash);
        }

        public static void WriteColored(params object[] list)
        {
            foreach (var obj in list)
            {
                if (obj is ConsoleColor)
                {
                    Console.ForegroundColor = (ConsoleColor)obj;
                }
                else if (obj is string)
                {
                    Console.Write(obj);
                    Console.ResetColor();
                }
            }
            
            Console.WriteLine();
        }
    }
}