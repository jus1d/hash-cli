namespace hash_cli;
using static Generator;
using static Algorithm;
using static HashCli;

public class Tests
{
    public static void Run()
    {
        Algorithm[] algorithms = { Sha1, Sha256, Sha384, Sha512, Md5 };

        string[] hashSums =
        {
            "2aae6c35c94fcfb415dbe95f408b9ce91ee846ed",
            "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9",
            "fdbd8e75a67f29f701a4e040385e2e23986303ea10239211af907fcbb83578b3e417cb71ce646efd0819dd8c088de1bd",
            "309ecc489c12d6eb4cc40f50c902f2b4d0ed77ee511a7c7a9bcd3ca86d4cd86f989dd35bc5ff499670da34255b45b0cfd830e81f605dcf7dc5542e93ae9cd76f",
            "5eb63bbbe01eeed093cb22bb8f5acdc3"
        };

        int passed = 0;
        int failed = 0;
        int total = algorithms.Length;

        for (int i = 0; i < algorithms.Length; i++)
        {
            bool result = Test(algorithms[i], "hello world", hashSums[i]);

            if (result)
            {
                passed++;

                WriteColor($"\n    {algorithms[i]}:");
                WriteLineColor(" [✓] Test passed", ConsoleColor.Green);
                
            }
            else
            {
                failed++;
                
                WriteColor($"\n    {algorithms[i]}:");
                WriteLineColor(" [×] Test failed", ConsoleColor.Red);
            }
        }

        if (failed == 0)
        {
            WriteColor("\nAll test successfully passed:");
            WriteLineColor($" [{passed}/{total}]", ConsoleColor.Green);
        }
        else
        {
            WriteColor("\nSome tests are failed:");
            WriteLineColor($" [{failed}/{total}]", ConsoleColor.Red);
        }
        
    }
    
    public static bool Test(Algorithm algorithm, string rawData, string hashSum)
    {
        if (HashCompute(algorithm, rawData, false) == hashSum)
            return true;
        
        else
            return false;
        
    }
}