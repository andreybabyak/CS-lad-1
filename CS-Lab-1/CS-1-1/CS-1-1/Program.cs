using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CS_1_1
{
    class Program
    {
        static int lettersNumber;

        static string ReadFile(string path)
        {
            string text ;

            using(StreamReader st = new StreamReader(path, Encoding.Default))
            {
                text = st.ReadToEnd();
            }

            return text;
        }

        static Dictionary<char, double> Frequency(string text)
        {

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";//"абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";
            Dictionary<char, double> frequency = new Dictionary<char, double>(33);
            Dictionary<char, double> tempFrequency = new Dictionary<char, double>(33);
            foreach (char c in alphabet)
            {
                frequency.Add(c, 0);
                tempFrequency.Add(c, 0);
            }
            char tempChar;
            int symbolNumbers = 0;
            foreach(char c in text)
            {
                tempChar = c;//char.ToLower(c);
                if (!alphabet.Contains(tempChar)) continue;
                symbolNumbers++;
                tempFrequency[tempChar]++;
            }
            lettersNumber = symbolNumbers;
            foreach(KeyValuePair<char,double> a in tempFrequency)
            {
                frequency[a.Key] = tempFrequency[a.Key] / symbolNumbers;
                Console.WriteLine("The frequency of the symbol \'{0}\' is {1}",a.Key, frequency[a.Key]);
            }

            return frequency;
        }

        static double Entropy(Dictionary<char, double> frequency)
        {
            double entropy = 0;
            foreach(KeyValuePair<char,double> a in frequency)
            {
                if (a.Value == 0) continue;
                entropy += a.Value * Math.Log(1/a.Value, 2);
            }
            Console.WriteLine("Entropy: " + entropy);
            return entropy;
        }

        static double NumberOfInformation(double entropy, string path)
        {
            double numberInfo = 0;

            numberInfo = entropy / 8 * lettersNumber;
            FileInfo f = new FileInfo(path);
            Console.WriteLine("File size: " + f.Length);
            Console.WriteLine("Number of informations: " + numberInfo);

            return numberInfo;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Enter file path:");
            string[] paths = { @"C:\__study\text1-rar-base64.txt", @"C:\__study\text2-rar-base64.txt", @"C:\__study\text3-rar-base64.txt" };

            for (int i = 0; i < paths.Length; i++)
            {
                Console.WriteLine("\n");
                Console.WriteLine("File " + (i + 1) + "\n");

                string path = paths[i];
                string text = ReadFile(path);
                Dictionary<char, double> frequency = Frequency(text);
                double entropy = Entropy(frequency);
                NumberOfInformation(entropy, path);

                Console.WriteLine("\n");
            }
        }
    }
}
