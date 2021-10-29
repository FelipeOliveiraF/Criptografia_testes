
using System;
using System.IO;
using System.Text;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static Random rnd = new Random();
        static String filePath = "E:/teste.txt";


        static void Main(string[] args)
        {
            Console.WriteLine("Você deseja criptografar ou descriptografar o arquivo: {0}?", filePath);
            Console.WriteLine("Insira, C -> criptografar, D -> descriptografar, qualquer outra tecla -> encerrar");
            ConsoleKeyInfo c = Console.ReadKey();
            Console.WriteLine("");
            if (c.Key == ConsoleKey.C) EncryptSequence();
            else if (c.Key == ConsoleKey.D) DecryptSequence();
            else Console.WriteLine("Programa encerrado!");



            DateTime inicio, fim;
            int k = 0;

            inicio = DateTime.Now;
            
            fim = DateTime.Now;
            Console.WriteLine("Tempo = " + (fim - inicio));
            Console.ReadKey();
        }

        static void EncryptSequence()
        {
            int p = RandomPrime(new int[] { });
            int q = RandomPrime(new int[] { p });
            int n = p * q;
            int pTotient = p - 1;
            int qTotient = q - 1;
            int nTotient = pTotient * qTotient;
            int e = E_Generate(nTotient);
            String publicKey = "(" + n + "," + e + ")";
            BigInteger[] encryptedText = Encrypt(File.ReadAllText(filePath), e, n);
            EncryptFile(encryptedText);
            int privateKey = PrivateKey(nTotient, e);
            Console.WriteLine("Sua chave pública: {0}", publicKey);
            Console.WriteLine("Sua chave privada: {0}", privateKey);
        }

        static void DecryptSequence()
        {
            Console.WriteLine("Insira o primeiro numero da chave publica:");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("Insira sua chave privada:");
            int privateKey = int.Parse(Console.ReadLine());
            DecryptFile(n, privateKey);
        }

        static int RandomPrime(int[] numbersBlocked)
        {
            int number = rnd.Next(1, 501);
            for (int i = 0; i < numbersBlocked.Length; i++)
            {
                if (number == numbersBlocked[i]) return RandomPrime(numbersBlocked);
            }
            return isPrime(number) ? number : RandomPrime(numbersBlocked);
        }

        static bool isPrime(int n)
        {
            if (n <= 1)
            {
                return false;
            }
            if (n <= 3)
            {
                return true;
            }
            if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }
            int i = 5;
            while (i * i <= n)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
                i += 6;
            }
            return true;
        }

        static int E_Generate(int nT)
        {
            int e = rnd.Next(2, nT);
            if (mdc(nT, e) == 1) return e;
            else return E_Generate(nT);
        }

        static int mdc(int n1, int n2)
        {
            int rest;
            while (n2 != 0)
            {
                rest = n1 % n2;
                n1 = n2;
                n2 = rest;
            }
            return n1;
        }

        static BigInteger[] Encrypt(String text, int e, int n)
        {
            BigInteger[] letters = new BigInteger[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                BigInteger k = text[i];
                k = BigInteger.Pow(k, e);
                BigInteger d = k % n;
                letters[i] = d;
            }
            return letters;
        }

        static void EncryptFile(BigInteger[] encryptedText)
        {
            String output = "";
            for (int i = 0; i < encryptedText.Length; i++)
            {
                if (i == encryptedText.Length - 1) output += encryptedText[i];
                else output += encryptedText[i] + ",";
            }
            File.WriteAllText(filePath, output);
            Console.WriteLine("Arquivo criptografado!");
        }

        static int PrivateKey(int nTotient, int e)
        {
            int privateKey = 0;
            while (privateKey * e % nTotient != 1)
            {
                privateKey += 1;
            }
            return privateKey;
        }

        static void DecryptFile(int n, int privateKey)
        {
            String text = File.ReadAllText(filePath);
            String[] fileValues = text.Split(',');
            int[] intFileValues = new int[fileValues.Length];
            for (int i = 0; i < fileValues.Length; i++)
            {
                intFileValues[i] = int.Parse(fileValues[i]);
            }
            String output = "";
            for (int i = 0; i < intFileValues.Length; i++)
            {
                int decrypted = (int)(BigInteger.Pow(intFileValues[i], privateKey) % n);
                output += Convert.ToChar(decrypted);
            }
            File.WriteAllText(filePath, output);
            Console.WriteLine("Arquivo descriptografado!");
        }
    }
}
