namespace hash_cli.Hash;

public class Keccak
{
    private static byte _instanceNumber = 6;
    
    private static UInt16[] _bArray = { 25, 50, 100, 200, 400, 800, 1600 };
    
    private static byte _matrixSize = 5 * 5;
    
    private static byte[] _wArray = { 1, 2, 4, 8, 16, 32, 64 };
    
    private static byte[] _lArray = { 0, 1, 2, 3, 4, 5, 6 };
    
    private static byte[] _nArray = { 12, 14, 16, 18, 20, 22, 24 };
    
    private static byte[,] _r = {
        {0, 36, 3, 41, 18},
        {1, 44, 10, 45, 2},
        {62, 6, 43, 15, 61},
        {28, 55, 25, 21, 56},
        {27, 20, 39, 8, 14}
    };
    
    private static UInt64[] _rc = {
        0x0000000000000001,
        0x0000000000008082,
        0x800000000000808A,
        0x8000000080008000,
        0x000000000000808B,
        0x0000000080000001,
        0x8000000080008081,
        0x8000000000008009,
        0x000000000000008A,
        0x0000000000000088,
        0x0000000080008009,
        0x000000008000000A,
        0x000000008000808B,
        0x800000000000008B,
        0x8000000000008089,
        0x8000000000008003,
        0x8000000000008002,
        0x8000000000000080,
        0x000000000000800A,
        0x800000008000000A,
        0x8000000080008081,
        0x8000000000008080,
        0x0000000080000001,
        0x8000000080008008
    };

    private static UInt64[,] _b = new UInt64[5, 5];
    private static UInt64[] _c = new UInt64[5];
    private static UInt64[] _d = new UInt64[5];

    /*const*/
    private static UInt16[] _rateArray = { 576, 832, 1024, 1088, 1152, 1216, 1280, 1344, 1408 };
    /*const*/
    private static UInt16[] _capacityArray = { 1024, 768, 576, 512, 448, 384, 320, 256, 192 };
    private enum Sha3 { Sha512 = 0, Sha384, Sha256 = 3, Sha224 };

    private static UInt64[,] KeccakF(UInt64[,] a)
    {
        for(Byte i = 0; i < _nArray[_instanceNumber]; i++)
            a = Round(a, _rc[i]);
        return a;
    }

    private static UInt64[,] Round(UInt64[,] a, UInt64 rcI)
    {
        Byte i, j;

        for (i = 0; i < 5; i++)
            _c[i] = a[i,0] ^ a[i,1] ^ a[i,2] ^ a[i,3] ^ a[i,4];
        for (i = 0; i < 5; i++)
            _d[i] = _c[(i + 4) % 5] ^ Rot(_c[(i + 1) % 5], 1, _wArray[_instanceNumber]);
        for (i = 0; i < 5; i++)
            for (j = 0; j < 5; j++)
                a[i,j] = a[i,j] ^ _d[i];

        for (i = 0; i < 5; i++)
            for (j = 0; j < 5; j++)
                _b[j,(2 * i + 3 * j) % 5] = Rot(a[i,j], _r[i,j], _wArray[_instanceNumber]);

        for (i = 0; i < 5; i++)
            for (j = 0; j < 5; j++)
                a[i,j] = _b[i,j] ^ ((~_b[(i + 1) % 5,j]) & _b[(i + 2) % 5,j]);

        a[0,0] = a[0,0] ^ rcI;

        return a;
    }

    private static UInt64 Rot(UInt64 x, Byte n, Byte w)
    {
        return ((x << (n % w)) | (x >> (w - (n % w))));
    }

    private static string ComputeKeccak(UInt16 rate, UInt16 capacity, List<Byte> message)
    {
        message.Add(0x01);

        UInt16 min = (UInt16)((rate - 8) / 8);
        UInt16 n = (UInt16)Math.Truncate((Double)message.Count / min);
        UInt32 messageFullCount = 0;
        if (n < 2)
        {
            messageFullCount = min;
        }
        else
        {
            messageFullCount = (UInt32)(n * min + (n - 1));
        }

        UInt32 delta = (UInt32)(messageFullCount - message.Count);
        if ((message.Count + delta) > UInt16.MaxValue - 1)
            throw (new Exception("Message might be too large"));

        while (delta > 0)
        {
            message.Add(0x00);
            delta--;
        }

        if (message.Count * 8 % rate != rate - 8)
            throw (new Exception("Length was incorrect calculated"));

        message.Add(0x80);
        /*const*/
        Int32 size = (message.Count * 8) / rate;

        UInt64[] p = new UInt64[size * _matrixSize];
        Int32 xF = 0, count = 0;
        Byte i = 0, j = 0;

        for(xF = 0; xF < message.Count; xF++)
        {
            if (j > (rate / _wArray[_instanceNumber] - 1))
            {
                j = 0;
                i++;
            }
            count++;
            if ((count * 8 % _wArray[_instanceNumber]) == 0)
            {
                p[size * i + j] = ReverseEightBytesAndToUInt64(
                    message.GetRange(count - _wArray[_instanceNumber] / 8, 8).ToArray()
                    );
                j++;
            }
        }

        UInt64 [,]S = new UInt64[5,5];
        for(i = 0; i < 5; i++)
            for(j = 0; j < 5; j++)
                S[i,j] = 0;

        for(xF = 0; xF < size; xF++)
        {
            for(i = 0; i < 5; i++)
                for(j = 0; j < 5; j++)
                    if ((i + j * 5) < (rate / _wArray[_instanceNumber]))
                    {
                        S[i, j] = S[i, j] ^ p[size * xF + i + j * 5];
                    }
            KeccakF(S);
        }

        Byte a = 0;
        Byte dMax = (Byte)(capacity / (2 * 8));
        List<Byte> retHash = new List<Byte>(dMax);

        for( ; ; )
        {
            for(i = 0; i < 5; i++)
                for(j = 0; j < 5; j++)
                    if((5 * i + j) < (rate / _wArray[_instanceNumber]))
                    {
                        if(a >= dMax)
                            i = j = 5;
                        else
                        {
                            retHash.AddRange(FromUInt64ToReverseEightBytes(S[j, i]));
                            a = (Byte)retHash.Count;
                        }
                    }
            if(a >= dMax)
                break;
            KeccakF(S);
        }

        return ByteArrayToString(retHash.GetRange(0, dMax).ToArray());
    }

    private static UInt64 ReverseEightBytesAndToUInt64(Byte[] bVal)
    {
        UInt64 ulVal = 0L;
        for (Byte i = 8, j = 0; i > 0; i--)
        {
            ulVal += (UInt64)((bVal[i - 1] & 0xF0) >> 4) * (UInt64)Math.Pow(16.0F, 15 - (j++));
            ulVal += (UInt64)(bVal[i - 1] & 0x0F) * (UInt64)Math.Pow(16.0F, 15 - (j++));
        }
        return ulVal;
    }

    private static byte[] FromUInt64ToReverseEightBytes(UInt64 ulVal)
    {
        Byte[] bVal = new Byte[8];
        Byte a = 0;
        do
        {
            bVal[a] = (Byte)((ulVal % 16) * 1);
            ulVal = ulVal / 16;
            bVal[a] += (Byte)((ulVal % 16) * 16);
            a++;
        }
        while (15 < (ulVal = ulVal / 16));
        while (a < 8)
        {
            bVal[a++] = (Byte)ulVal;
            ulVal = 0;
        }

        return bVal;
    }

    static List<Byte> StringToByteList(string str)
    {
        List<Byte> ret = new List<byte>(str.Length);

        foreach(char ch in str)
        {
            ret.Add((Byte)ch);
        }

        return ret;
    }

    private static string ByteArrayToString(Byte[] b)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(16);
        for (Int32 i = 0; i < Math.Min(b.Length, Int32.MaxValue - 1); i++)
            sb.Append($"{b[i]:X2}");
        return sb.ToString();
    }

    public static string Compute(Algorithm algorithm, string rawData, bool isFile = false)
    {
        List<byte> byteData;
        
        if (isFile)
            byteData = File.ReadAllBytes(rawData).ToList();
        else
            byteData = StringToByteList(rawData);

        return algorithm switch
        {
            Algorithm.Keccak224 => ComputeKeccak(_rateArray[(Byte)Sha3.Sha224], _capacityArray[(Byte)Sha3.Sha224], byteData)
                .ToLower(),
            Algorithm.Keccak256 => ComputeKeccak(_rateArray[(Byte)Sha3.Sha256], _capacityArray[(Byte)Sha3.Sha256], byteData)
                .ToLower(),
            Algorithm.Keccak384 => ComputeKeccak(_rateArray[(Byte)Sha3.Sha384], _capacityArray[(Byte)Sha3.Sha384], byteData)
                .ToLower(),
            Algorithm.Keccak512 => ComputeKeccak(_rateArray[(Byte)Sha3.Sha512], _capacityArray[(Byte)Sha3.Sha512], byteData)
                .ToLower(),
            _ => "Unsupported hash algorithm"
        };
    }
}