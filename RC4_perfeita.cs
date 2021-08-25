using System;
using System.IO;

namespace RC4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Arquivo onde está o texto de entrada e onde vai o texto de saida
            FileStream file = new FileStream("E:/others/crypt/teste.txt", FileMode.Open, FileAccess.ReadWrite);
            //Variavel que armazenará o input em bytes
            byte[] input = new byte[file.Length];
            //Chave de criptografia
            String stringKey = "chave";
            //Chave de criptografia em bytes
            byte[] key = new byte[stringKey.Length];
            //Variavel para key-scheduling
            byte[] S = new byte[256];
            //Variavel para armazenar o resultado em bytes
            byte[] result = new byte[input.Length];
            //Seta o valor da chave de String ( stringKey ) para byte ( key )
            for (int i = 0; i < stringKey.Length; i++)
            {
                key[i] = (byte)stringKey[i];
            }
            //Seta o valor de entrada ( file ) para byte ( input )
            for (int i = 0; i < file.Length; i++)
            {
                input[i] = (byte)file.ReadByte();
            }
            //KSA
            for (int i = 0; i <= 255; i++)
            {
                S[i] = (byte)i;
            }
            int j = 0;
            for (int i = 0; i <= 255; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                Swap(ref S[i], ref S[j]);
            }
            //PRGA 
            int aux1 = 0, aux2 = 0;
            for (int i = 0; i < input.Length; i++)
            {
                aux1 = (aux1 + 1) % 256;
                aux2 = (aux2 + S[aux1]) % 256;
                Swap(ref S[aux1], ref S[aux2]);
                result[i] = (byte)(S[(S[aux1] + S[aux2]) % 256] ^ input[i]);
            }
            file.Position = 0;
            //Muda o texto do arquivo para o resultado
            for (int i = 0; i < result.Length; i++)
            {
                file.WriteByte(result[i]);
            }
            file.Close();

        }

        static void Swap(ref byte val1, ref byte val2)
        {
            val1 = (byte)(val1 ^ val2);
            val2 = (byte)(val1 ^ val2);
            val1 = (byte)(val1 ^ val2);
        }
    }
}
