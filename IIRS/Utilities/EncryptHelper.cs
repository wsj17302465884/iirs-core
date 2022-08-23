﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IIRS.Utilities
{
    /// <summary>
    /// MD5 辅助类
    /// </summary>
    public class EncryptHelper
    {
        private static string _basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
        /// <summary>
        /// 为MD5加密串加的盐
        /// </summary>
        private static string _salt = "!@#trt3aiwlw!#@";

        /// <summary>
        /// MD5 加密 字符串 
        /// <param name="input">输入的内容</param>
        /// <returns>MD5加密后的字符串</returns>
        /// </summary>
        public static string Md5Method(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.Default.GetBytes(input);

            byte[] bytesMd5 = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytesMd5.Length; i++)
            {
                //sb.Append(bytesMd5[i].ToString("x2")); //32位小写
                sb.Append(bytesMd5[i].ToString("X2")); //32位大写
            }

            return sb.ToString();
        }


        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password + _salt)), 4, 8);
            t2 = t2.Replace("-", string.Empty);
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password = "")
        {
            string pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
                {
                    MD5 md5 = MD5.Create(); //实例化一个md5对像
                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
                    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password + _salt));
                    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    foreach (var item in s)
                    {
                        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                        pwd = string.Concat(pwd, item.ToString("X2"));
                    }
                }
            }
            catch
            {
                throw new Exception($"错误的 password 字符串:【{password}】");
            }
            return pwd;
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            // 实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password + _salt));
            return Convert.ToBase64String(s);
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="EncryptString"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string EncryptString)
        {
            var _privateKeyRsaProvider = CreateRsaProviderFromPrivateKey(GetRSAPrivateKey());
            return Encoding.UTF8.GetString(_privateKeyRsaProvider.Decrypt(Convert.FromBase64String(EncryptString), RSAEncryptionPadding.Pkcs1));
        }

        public static string RsaEncrypt(string text)
        {
            var _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(GetRSAPublicKey());
            return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1));
        }

        #region RSA公钥
        public static string GetRSAPublicKey()
        {
            const string RsaPublicKeyHeader = "-----BEGIN PUBLIC KEY-----";
            const string RsaPublicKeyFooter = "-----END PUBLIC KEY-----";
            var _filePath = Path.Combine(_basePath, "RSA_KEY/rsa_1024_pub.pem");
            if (File.Exists(_filePath))
            {
                string pemContents = File.ReadAllText(_filePath);
                if (pemContents.StartsWith(RsaPublicKeyHeader))
                {
                    int endIdx = pemContents.IndexOf(
                        RsaPublicKeyFooter,
                        RsaPublicKeyHeader.Length,
                        StringComparison.Ordinal);

                    string der = pemContents.Substring(
                        RsaPublicKeyHeader.Length,
                        endIdx - RsaPublicKeyHeader.Length);
                    return der;
                }
                else
                {
                    throw new Exception("Not a valid pem file.");
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        #endregion

        #region RSA私钥

        private static byte[] GetRSAPrivateKey()
        {
            const string RsaPrivateKeyHeader = "-----BEGIN RSA PRIVATE KEY-----";
            const string RsaPrivateKeyFooter = "-----END RSA PRIVATE KEY-----";
            var _filePath = Path.Combine(_basePath, "RSA_KEY/rsa_1024_priv.pem");
            if (File.Exists(_filePath))
            {
                string pemContents = File.ReadAllText(_filePath);
                if (pemContents.StartsWith(RsaPrivateKeyHeader))
                {
                    int endIdx = pemContents.IndexOf(
                        RsaPrivateKeyFooter,
                        RsaPrivateKeyHeader.Length,
                        StringComparison.Ordinal);

                    string base64 = pemContents.Substring(
                        RsaPrivateKeyHeader.Length,
                        endIdx - RsaPrivateKeyHeader.Length);

                    byte[] der = Convert.FromBase64String(base64);
                    return der;
                }
                else
                {
                    throw new Exception("Not a valid pem file.");
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        #endregion


        #region 使用私钥创建RSA实例

        private static RSA CreateRsaProviderFromPrivateKey(byte[] privateKey)
        {
            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKey)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        #endregion

        #region 使用公钥创建RSA实例
        private static RSA CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(publicKeyString);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }
        #endregion

        #region 导入密钥算法

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = binr.ReadByte();
                var lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion
    }
}
