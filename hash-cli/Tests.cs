namespace hash_cli;
using static Generator;
using static Algorithm;
using static HashCli;

public class Tests
{
    public static void Run()
    {
        Algorithm[] algorithms = { Sha1, Sha256, Sha384, Sha512, Md5, Keccak224, Keccak256, Keccak384, Keccak512 };

        string[] hashSums =
        {
            "ab8f37d89b1154ba18c78a7e4b8eef2acdfec1eb",
            "03725d0a96e114361230a7978eeefa0d646d7656dce5e44ae4e70a4dea5e674c",
            "91418bfed5c5a82781aaa319872432859196a6b1d396e987fb29b98b741c0d75d8720b702e814ad85d6dba5537a5a94e",
            "a9b24bcbe376186d21cd9c35beacadb9f4ad8c31423ea69fb1e98aa329b14ec0b4b767f38f1588bbe194cf558eb387e0bef2d9ddc8a4dab3cd772ec588eee92a",
            "7b0fc9a6fa7c72459e61992ad8927d77",
            "d891ece606c9353ce0186576b85e5e7c7e57aa559e917819108a572e",
            "6128c713655aba794a3909bbc150458aad952069212788363db9a210b7aef6b7",
            "6a4e7978fa2bbd610a30423833b859605422df800bbcb172ca9bcd544196d86b9b6187ec3e7dc2eb2cb26fe3c7e2e823",
            "9315fb2bae0e526ca52355cd3d34b42e0204228589233aa515b74b1afd904dfb430d37522b4a5421657c601bdf81a1a2051c044066057b4cb3c644614e2ae913"
        };

        int passed = 0;
        int failed = 0;
        int total = algorithms.Length;

        for (int i = 0; i < algorithms.Length; i++)
        {
            bool result = Test(algorithms[i], "test phrase", hashSums[i]);

            if (result)
            {
                passed++;
                
                WriteColored($"\n    {algorithms[i]}:", SuccessColor, " [✓] Test passed");
            }
            else
            {
                failed++;
                
                WriteColored($"\n    {algorithms[i]}:", ErrorColor, " [×] Test failed");
            }
        }

        if (failed == 0)
        {
            WriteColored("\nAll test successfully passed:", SuccessColor, $" [{passed}/{total}]");
        }
        else
        {
            WriteColored("\nSome tests are failed:", ErrorColor, $" [{failed}/{total}]");
        }
        
    }
    
    public static bool Test(Algorithm algorithm, string rawData, string hashSum)
    {
        if (HashCompute(algorithm, rawData, false) == hashSum)
        {
            return true;
        }
        return false;
        
    }
}