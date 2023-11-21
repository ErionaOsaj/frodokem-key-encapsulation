using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrodoKem.Utils
{
    public static class TestFrodoKem
    {
        public static void Test()
        {
            FrodoKemHelper frodoKem = new FrodoKemHelper();

            (Matrix publicKey, Matrix privateKey) = frodoKem.KeyGen();
            Console.WriteLine("Keys generated.");

            // Encapsulate to generate ciphertext and shared secret
            (Matrix ciphertext, byte[] encapsulatedSharedSecret) = frodoKem.Encapsulate(publicKey);
            Console.WriteLine("Encapsulation done. Ciphertext and shared secret generated.");

            // Decapsulate to retrieve the shared secret
            byte[] decapsulatedSharedSecret = frodoKem.Decapsulate(ciphertext, privateKey);
            // Compare the encapsulated and decapsulated shared secrets
            bool secretsMatch = AreEqual(encapsulatedSharedSecret, decapsulatedSharedSecret);
            Console.WriteLine($"Do the shared secrets match? {secretsMatch}");

            // Prompt the user to press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        static bool AreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

    }

}
