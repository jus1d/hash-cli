using System.Security.Cryptography;
using System.Text;

namespace hash_cli.Hash;

public class Md
{
    public static string Compute(Algorithm algorithm, string rawData, bool isFile = false)
    {
        byte[] byteData;
        
        if (isFile)
            byteData = File.ReadAllBytes(rawData);
        else
            byteData = Encoding.ASCII.GetBytes(rawData);

        return algorithm switch
        {
            Algorithm.Md5 => Md5(byteData),
            _ => "Unsupported hash algorithm"
        };
    }

    private static string Md5(byte[] byteData)
    {
        byte[] hashBytes;
        
        MD5 md5 = MD5.Create();
        hashBytes = md5.ComputeHash(byteData);
        return Convert.ToHexString(hashBytes).ToLower();
    }
}