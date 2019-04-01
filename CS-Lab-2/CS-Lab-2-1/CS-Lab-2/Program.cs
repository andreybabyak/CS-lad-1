using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Lab_2
{
    public class Multiplication
    {
        public int MultiplierFirst { get; set; }
        public int MultiplierSecond { get; set; }
        public Multiplication(int x, int y)
        {
            MultiplierFirst = x;
            MultiplierSecond = y;
        }
        public void Do()
        {
            Int64 A = (Int64)MultiplierFirst << 17;
            Int64 S = (Int64)(-MultiplierFirst) << 17;
            Int64 P = (MultiplierSecond << 1) & 0b0000_0000_0000_0000_1111_1111_1111_1111_0;
            string A_str = IntToBinaryString(A);
            string S_str = IntToBinaryString(S);
            Console.WriteLine("Booth's algorithm:");
            for (int i = 1; i < 17; ++i)
            {
                Console.WriteLine("  Step " + i + ":");
                switch (P & 0b11)
                {
                    case 0b01:
                        Console.WriteLine("  \tAdd A:\t{0}\n\tTo P:\t{1}", A_str, IntToBinaryString(P));
                        P += A;
                        break;
                    case 0b10:
                        Console.WriteLine("  \tAdd S:\t{0}\n\tTo P:\t{1}", S_str, IntToBinaryString(P));
                        P += S;
                        break;
                }
                Console.WriteLine("  \tShift right:\t" + IntToBinaryString(P));
                P >>= 1;
                Console.WriteLine("  \t\t        " + IntToBinaryString(P));
            }
            P >>= 1; 
            Console.WriteLine("  Answer is:\n\tIn decemal: {0}\n\tIn binary: {1}", P, IntToBinaryString(P, true));
        }

        private string IntToBinaryString(Int64 number, bool is_end_result = false)
        {
            const int mask = 1;
            var binary = string.Empty;
            for (int i = (is_end_result ? 1 : 0); i < 33; ++i)
            {
                binary = (i % 4 == 0 ? " " : "") + (number & mask) + binary;
                number >>= 1;
            }

            return binary;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Int16 x, y;
            Console.WriteLine("Enter first number:");
            x = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            y = Int16.Parse(Console.ReadLine());
            Multiplication m = new Multiplication(x, y);
            m.Do();
        }
    }
}
