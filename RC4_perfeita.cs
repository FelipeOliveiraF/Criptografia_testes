using System;
using System.IO;

namespace RC4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Arquivo onde está o texto de entrada e onde vai o texto de saida
      
            FileStream file = new System.IO.FileStream("C:\\testes\\teste01.txt", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
            //Variavel que armazenará o input em bytes
            byte[] input = new byte[file.Length];
            //Chave de criptografia
            string stringKey = Console.ReadLine();
            //Chave de criptografia em bytes
            byte[] key = new byte[stringKey.Length];
            //Variavel para key-scheduling
            byte[] S = new byte[256];
            //Variavel para armazenar o resultado em bytes
            byte[] result = new byte[input.Length];
            //Seta o valor da chave de String ( stringKey ) para byte ( key )
            for (int i = 0; i < stringKey.Length; i++)
            {
                key[i] = (byte)stringKey[i];// preenche o vetor key. O byte do primeiro caractér da chave é salvo em key[0] e assim em diante
            }
            //Seta o valor de entrada ( file ) para byte ( input )
            for (int i = 0; i < file.Length; i++)
            {
                input[i] = (byte)file.ReadByte(); // input é preenchido com o byte referente ao primeiro carácter do texto do arquivo.
            }
            //KSA
            for (int i = 0; i <= 255; i++)
            {
                S[i] = (byte)i; // S[i] = byte correspondente do próprio i
            }
            int j = 0;
            for (int i = 0; i <= 255; i++) 
            {
                j = (j + S[i] + key[i % key.Length]) % 256; // Ex = j = ( 0 + byte de i + key[i % tamanho da chave]
                Swap(ref S[i], ref S[j]); //troca s[i] com s[j]
            }
            //PRGA 
            int aux1 = 0, aux2 = 0;
            for (int i = 0; i < input.Length; i++)
            {
                aux1 = (aux1 + 1) % 256; 
                aux2 = (aux2 + S[i]) % 256;//aux2 = aux2 + s[i]
                Swap(ref S[i], ref S[j]);// troca novamente
                result[i] = (byte)(S[(S[i] + S[j]) % 256] ^ input[i]); // result[i] = (byte) S[(S na posição i + S na posição j)]
            }

            file.Position = 0;// esse comando é o responsável por deixar o arquivo "em branco" para depois escrever somente o que está criptografado.
            //Caso não houvesse esse comando, o texto criptografado apenas seria  posicionado junto ao texto original. A informação ainda estaria lá.
            //Muda o texto do arquivo para o resultado
            for (int i = 0; i < result.Length; i++)
            {
                file.WriteByte(result[i]); // preenche o arquivo com o conteúdo criptografado.

            }
            file.Close();//fecha o arquivo.

        }
        
        static void Swap(ref byte val1, ref byte val2)
        {
            val1 = (byte)(val1 ^ val2);
            val2 = (byte)(val1 ^ val2);
            val1 = (byte)(val1 ^ val2);
        }
    }
}
