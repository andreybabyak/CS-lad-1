using System;

namespace CS_Lab_2_2
{
    class Division
    {
        public int Divide { get; set; }
        public int Divisor { get; set; }
        public Division(int x, int y)
        {
            Divide = x;
            Divisor = y;
        }
        public void Do()
        {
            Int64 register = 0 | Divide,
                remainderRegisterBits = 0b1_1111_1111_1111_1111_0000_0000_0000_0000,
                quotientRegisterBits = 0b1111_1111_1111_1111,
                shiftedDivisor = Divisor << 16,
                shiftedMinusDivisor = -Divisor << 16;

            const int remainderBitsAmount = 17,
                quotientBitsAmount = 16,
                registerBitsAmount = 33;

            Console.WriteLine("\tRegister:\n\t\t          {0}", RegisterPartToBinaryString(register, registerBitsAmount));
            for (int i = 0; i < 16; ++i)
            {
                register <<= 1;
                Console.WriteLine("\tShift left:\n\t\t   {0}", RegisterPartToBinaryString(register, registerBitsAmount));

                if ((register >> 32 & 1) == 0)
                {
                    Console.WriteLine("Substract divisor: {0}", RegisterPartToBinaryString(shiftedMinusDivisor, remainderBitsAmount, true));
                    register += shiftedMinusDivisor;
                    Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, registerBitsAmount));
                }
                else
                {
                    Console.WriteLine("      Add divisor: {0}", RegisterPartToBinaryString(shiftedDivisor, remainderBitsAmount, true));
                    register += shiftedDivisor;
                    Console.WriteLine("\tRegister:\n\t\t   {0}", RegisterPartToBinaryString(register, registerBitsAmount));
                }

                if ((register >> 32 & 1) == 0)
                {
                    register |= 1;
                    Console.WriteLine("\tSet last quotient bit to 1:\n\t\t   {0}", RegisterPartToBinaryString(register, registerBitsAmount));
                }
                else
                    Console.WriteLine("\tSet last quotient bit to 0:\n\t\t   {0}", RegisterPartToBinaryString(register, registerBitsAmount));
            }

            if ((register >> 32 & 1) == 1)
            {
                Console.WriteLine("      Add divisor:  {0}", RegisterPartToBinaryString(shiftedDivisor, remainderBitsAmount, true));
                register += shiftedDivisor;
                Console.WriteLine("\tRegister:\n\t\t    {0}", RegisterPartToBinaryString(register, registerBitsAmount));
            }

            Console.WriteLine("\tAnswer is:");
            Console.WriteLine("\t\tRemainder:\t    {0} (in decimal: {1})",
                RegisterPartToBinaryString(register & remainderRegisterBits, remainderBitsAmount, true),
                (register & remainderRegisterBits) >> 16);
            Console.WriteLine("\t\tQuotient:\t      {0} (in decimal: {1})",
                RegisterPartToBinaryString(register & quotientRegisterBits, quotientBitsAmount),
                register & quotientRegisterBits);
        }

        private string RegisterPartToBinaryString(Int64 register, byte bitsAmount, bool isDivisor = false)
        {
            string result = string.Empty;

            int last_index = isDivisor ? 15 : -1;
            for (int i = bitsAmount - 1 + (isDivisor ? 16 : 0); i > last_index; --i)
                result += (register >> i & 1) + (i % 4 == 0 && i != 0 ? " " : "");

            return result;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first number:");
            Int16 x = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Enter second number:");
            Int16 y = Int16.Parse(Console.ReadLine());
            Division d = new Division(x, y);
            d.Do();
        }
    }
}
