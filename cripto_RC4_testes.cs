using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FiboRecursivo
{
    class Program
    {
        static void Swap(ref int val1, ref int val2)
        {
            val1 = (int)(val1 ^ val2); // val1 XOR val2
            val2 = (int)(val1 ^ val2);
            val1 = (int)(val1 ^ val2);
        }
        static void Main(string[] args)
        {
            FileStream iofile = new System.IO.FileStream("C: \\testes\\teste01.txt", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);

            int[] S = new int[256];
            int[] T = new int[256];
            int[] K = new int[256];
            byte[] C = new byte[256];
            int key;
            string cripto = String.Empty;

            for (int i = 0; i < iofile.Length; i++)
            {
                cripto += iofile.ReadByte();
            }

            Console.WriteLine("Chave: ");
            key = int.Parse(Console.ReadLine());

            for (int i = 0; i < 256; i++)
            {
                T[i] = key;
                S[i] = i;
            }
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i % T.Length]) % 256;
                Swap(ref S[i], ref S[j]);
            }
            int e = j = 0;
            int t = 0;
            for (int i = 0; i < iofile.Length; i++)
            {
                j = (j + S[i] % 256);
                Swap(ref S[i], ref S[j]);

                C[i] = (byte)(S[i] ^ (byte)cripto[i]);
                iofile.WriteByte(C[i]);
            }


            Console.ReadKey();
        }



    }
}
