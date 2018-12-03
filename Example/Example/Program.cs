using System;
using System.Diagnostics;
using Xxtea;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace Example {
    class MainClass {
        public static void Main (string[] args) {
            String str = "Hello World! 你好，中国！";
            String key = "1234567890";
            String encrypt_data = XXTEA.EncryptToBase64String(str, key);
            Console.WriteLine(encrypt_data);
            Debug.Assert("QncB1C0rHQoZ1eRiPM4dsZtRi9pNrp7sqvX76cFXvrrIHXL6" == encrypt_data);
            String decrypt_data = XXTEA.DecryptBase64StringToString(encrypt_data, key);
            Debug.Assert(str == decrypt_data);

            //DeLua();
            //DePng();
            //Rollback();
            //DePngs();
            //DeLuas();
            //DeLua();
            DeIni();
            EnIni();
        }
        static void EnIni()
        {
            var file = File.ReadAllBytes(@"2.ini");
            var data = Copy(file, 0);
            byte[] keys = {0x4A, 0x89, 0xF1, 0xF1,
            0xFB, 0xB2, 0x46, 0x23,
            0xC2, 0x1C, 7, 0x8E,
            0xF6, 0xEC, 0xCD, 0xF9};

            var newdata = XXTEA.Encrypt(data, keys);

            File.WriteAllBytes("3.enc", newdata);

            //newdata = CopyLua(newdata);

            //var newdata2 = XXTEA.Decrypt(newdata, keys);


            // File.WriteAllBytes("1.lua", newdata2);




        }
        static void DeIni()
        {
            var file = File.ReadAllBytes(@"C:\Users\liubo\Nox_share\App\LegionData");
            var data = Copy(file, 0);
            byte[] keys = {0x4A, 0x89, 0xF1, 0xF1,
            0xFB, 0xB2, 0x46, 0x23,
            0xC2, 0x1C, 7, 0x8E,
            0xF6, 0xEC, 0xCD, 0xF9};

            byte[] keys2 = {
                0x24, 0x54, 0x26, 0x89,
                0xA8, 0x1C, 0x9C, 0xA6,
                0xD1, 0x50, 0x17, 0xB,
                0x5B, 0x6C, 5, 0xF6
            };

            var newdata = XXTEA.Decrypt(data, keys);

            File.WriteAllBytes("2.ini", newdata);

            //newdata = CopyLua(newdata);

            //var newdata2 = XXTEA.Decrypt(newdata, keys);


            // File.WriteAllBytes("1.lua", newdata2);




        }

        static void DeLua()
        {
            var file = File.ReadAllBytes(@"C:\__temp\Idle Knife_1.0.17\assets\Scripts\VersionID.lua");
            var data = Copy(file, 4);
            byte[] keys = {0x4A, 0x89, 0xF1, 0xF1,
            0xFB, 0xB2, 0x46, 0x23,
            0xC2, 0x1C, 7, 0x8E,
            0xF6, 0xEC, 0xCD, 0xF9};

            byte[] keys2 = {
                0x24, 0x54, 0x26, 0x89,
                0xA8, 0x1C, 0x9C, 0xA6,
                0xD1, 0x50, 0x17, 0xB,
                0x5B, 0x6C, 5, 0xF6
            };

            var newdata = XXTEA.Decrypt(data, keys2);

            File.WriteAllBytes("1.lua", newdata);

            //newdata = CopyLua(newdata);

            //var newdata2 = XXTEA.Decrypt(newdata, keys);


            // File.WriteAllBytes("1.lua", newdata2);




        }
        static void DePng()
        {
            var file = File.ReadAllBytes(@"C:\__temp\Idle Knife_1.0.17\assets\GameScene\BulletStreak.png");
            var data = Copy(file, 4);

            byte[] keys2 = {
                0x24, 0x54, 0x26, 0x89,
                0xA8, 0x1C, 0x9C, 0xA6,
                0xD1, 0x50, 0x17, 0xB,
                0x5B, 0x6C, 5, 0xF6
            };

            var newdata = XXTEA.Decrypt(data, keys2);
            newdata = CopyPng(newdata);
            // newdata = Copy(newdata, 7 * 16 - 1);
            File.WriteAllBytes("1.png", newdata);
        }

        static void Rollback()
        {
            var files = Directory.GetFiles(@"C:\__temp\Idle Knife_1.0.17\assets", "*.png.bak", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                File.WriteAllBytes(file.Substring(0, file.Length - 4), File.ReadAllBytes(file));
            }
        }
        static void DeLuas()
        {
            var files = Directory.GetFiles(@"C:\__temp\Idle Knife_1.0.17\assets", "*.lua", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var filedata = File.ReadAllBytes(file);

                if (filedata[0] == 'K' && filedata[1] == 'I' && filedata[2] == 'N' && filedata[3] == 'G')
                {

                }
                else
                {
                    Console.WriteLine("        此文件已被解密");
                    continue;
                }

                var data = Copy(filedata, 4);

                byte[] keys2 = {
                0x24, 0x54, 0x26, 0x89,
                0xA8, 0x1C, 0x9C, 0xA6,
                0xD1, 0x50, 0x17, 0xB,
                0x5B, 0x6C, 5, 0xF6
                };

                var newdata = XXTEA.Decrypt(data, keys2);
                try
                {
                    //newdata = CopyPng2(newdata);
                    newdata = UnZip(newdata);
                }
                catch (Exception)
                {
                    Console.WriteLine("此文件解码错误。");
                }

                File.WriteAllBytes(file + ".bak", filedata);
                File.WriteAllBytes(file, newdata);
            }
        }

        static void DePngs()
        {
            var files = Directory.GetFiles(@"C:\__temp\Idle Knife_1.0.17\assets", "*.png", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Console.WriteLine(file);
                var filedata = File.ReadAllBytes(file);
                
                if (filedata[0] == 'K' && filedata[1] == 'I' && filedata[2] == 'N' && filedata[3] == 'G')
                {

                }
                else
                {
                    Console.WriteLine("        此文件已被解密");
                    continue;
                }

                var data = Copy(filedata, 4);

                byte[] keys2 = {
                0x24, 0x54, 0x26, 0x89,
                0xA8, 0x1C, 0x9C, 0xA6,
                0xD1, 0x50, 0x17, 0xB,
                0x5B, 0x6C, 5, 0xF6
                };

                var newdata = XXTEA.Decrypt(data, keys2);
                try
                {
                    //newdata = CopyPng2(newdata);
                    newdata = UnZip(newdata);
                }
                catch (Exception)
                {
                    Console.WriteLine("此文件解码错误。");
                }

                File.WriteAllBytes(file + ".bak", filedata);
                File.WriteAllBytes(file, newdata);
            }
        }

        static byte[] UnZip(byte[] src)
        {
            List<byte> ret = new List<byte>();
            //ZipFileusing (FileStream fs = new FileStream(path, FileMode.Open))            
            using (var ms = new MemoryStream(src))
            {
                using (var za = new ZipArchive(ms, ZipArchiveMode.Read))
                {
                    foreach (var entry in za.Entries)
                    {
                        using (var r = new BinaryReader(entry.Open()))
                        {

                            if (ret.Count > 0)
                            {
                                throw new System.Exception("一个zip包中有多个文件！");
                            }

                            byte[] buf = new byte[1024];
                            int last = 0;
                            while (true)
                            {
                                var cnt = r.Read(buf, last, 1024);
                                if (cnt > 0)
                                {
                                    ret.AddRange(buf);
                                }
                                if (cnt < 1024)
                                {
                                    break;
                                }
                            }

                        }
                    }
                }
            }

            return ret.ToArray();
        }


        static byte[] Copy(byte[] src, int len)
        {
            var ret = new byte[src.Length - len];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = src[i + len];
            }
            return ret;
        }
        static byte[] CopyPng(byte[] src)
        {
            int len = -1;
            for (var i = 0; i < src.Length; i++)
            {
                if (src[i] == 0x89 && src[i + 1] == 0x50 && src[i + 2] == 0x4E && src[i + 3] == 0x47)
                {
                    len = i;
                    break;
                }
            }
            if (len >= 0)
            {
                return Copy(src, len);
            }
            throw new Exception("错误了");
        }
        static byte[] CopyPng2(byte[] src)
        {
            int idx = 26;
            int cnt = src[idx + 0] * 1 + src[idx + 1] * 256 + src[idx + 2] * 256 * 256 + src[idx + 3] * 256 * 256;
            idx += 4;
            idx += cnt;
            return Copy(src, 0);
        }
        static byte[] CopyLua(byte[] src)
        {
            int len = -1;
            for (var i = 0; i < src.Length; i++)
            {
                if (src[i] == '.' && src[i + 1] == 'l' && src[i + 2] == 'u' && src[i + 3] == 'a')
                {
                    len = i + 5;
                    break;
                }
            }
            if (len >= 0)
            {
                return Copy(src, len);
            }
            throw new Exception("错误了");
        }
    }
}
