using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CS_1_2
{
    class Program
    {
        static byte[] ReadFile(string path)
        {
            List<byte> arr = new List<Byte>();
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while(br.BaseStream.Position != br.BaseStream.Length)
                {
                    arr.Add(br.ReadByte());
                    //Console.WriteLine(arr[arr.Count - 1]);
                }
            }
            byte[] result = arr.ToArray();
            return result;
        }

        static void Base64(byte[] byteCode)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

            string[] binaryCode8 = new string[byteCode.Length];
            string temnCode = "";
            int numberBit = 0;
            for (int i = 0; i < byteCode.Length; i++) 
            {
                int temp = byteCode[i];
                temnCode = "";
                numberBit = 0;
                while ((temp > 1) || (numberBit != 8)) 
                {
                    temnCode += temp % 2;
                    temp = temp / 2;
                    numberBit++;
                }
                char[] c = temnCode.ToCharArray();
                Array.Reverse(c);
                binaryCode8[i] = new string(c);
            }
            //foreach(string a in binaryCode8)
            //{
            //    Console.WriteLine(a);
            //}

            List<string> binaryCode6 = new List<string>();

            for(int i = 0; i < binaryCode8.Length; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        binaryCode6.Add(binaryCode8[i].Substring(0, 6));
                        if(i+1 == binaryCode8.Length)
                        {
                            binaryCode6.Add(binaryCode8[i].Substring(6, 2) + "0000");
                        }
                        break;
                    case 1:
                        binaryCode6.Add(binaryCode8[i - 1].Substring(6, 2) + binaryCode8[i].Substring(0, 4));
                        if (i + 1 == binaryCode8.Length)
                        {
                            binaryCode6.Add(binaryCode8[i].Substring(4, 4) + "00");
                        }
                        break;
                    case 2:
                        binaryCode6.Add(binaryCode8[i - 1].Substring(4, 4) + binaryCode8[i].Substring(0, 2));
                        binaryCode6.Add(binaryCode8[i].Substring(2, 6));
                        break;
                }
            }

            //foreach (string a in binaryCode6)
            //    Console.WriteLine(a);

            string answer = "";
            for(int i = 0; i < binaryCode6.Count; i++)
            {
                int temp = 0;
                int q = 5;
                for(int j = 0; j < 6; j++)
                {
                    temp += (int)Math.Pow(2, q) * int.Parse(binaryCode6[i][j].ToString());
                    q--;
                }
                //Console.Write(temp + " ");
                answer += alphabet[temp];
            }
            if (answer.Length % 4 == 3) answer += "=";
            else if (answer.Length % 4 == 2) answer += "==";
            Console.WriteLine(answer);
        }

        static void Main(string[] args)
        {
            byte[] byteCode = ReadFile(@"C:\__study\text1.rar");
            Base64(byteCode);
            Console.WriteLine("\n\n");
            byteCode = ReadFile(@"C:\__study\text2.rar");
            Base64(byteCode);
            Console.WriteLine("\n\n");
            byteCode = ReadFile(@"C:\__study\text3.rar");
            Base64(byteCode);
        }
    }
}
