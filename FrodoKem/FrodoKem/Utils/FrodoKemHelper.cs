using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FrodoKem.Utils
{
    public class FrodoKemHelper
    {
        private readonly Matrix matrixA;
        private const int MatrixSize = 128; // Define according to FrodoKEM specification
        private readonly RNGCryptoServiceProvider rng;
        private const int NoiseMax = 10;
        public FrodoKemHelper()
        {
            rng = new RNGCryptoServiceProvider();
            matrixA = GenerateRandomMatrix(MatrixSize, MatrixSize);
        }
        private Matrix GenerateNoiseMatrix(int rows, int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            byte[] randomNumber = new byte[4]; // Size of an integer

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rng.GetBytes(randomNumber);
                    int noiseValue = BitConverter.ToInt32(randomNumber, 0);
                    matrix[i, j] = Mod(noiseValue, NoiseMax) - NoiseMax / 2; // Example to create a simple noise
                }
            }

            return matrix;
        }

        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        private Matrix GenerateRandomMatrix(int rows, int cols)
        {
            Matrix matrix = new Matrix(rows, cols);

            byte[] randomNumber = new byte[4]; // Size of an integer

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rng.GetBytes(randomNumber);
                    matrix[i, j] = BitConverter.ToInt32(randomNumber, 0);
                }
            }

            return matrix;
        }

        public (Matrix publicKey, Matrix privateKey) KeyGen()
        {
            Matrix s = GenerateNoiseMatrix(MatrixSize, MatrixSize);
            Matrix e = GenerateNoiseMatrix(MatrixSize, MatrixSize);
            Matrix B = matrixA.Multiply(s).Add(e);

            return (B, s);
        }

        public (Matrix ciphertext, byte[] sharedSecret) Encapsulate(Matrix publicKey)
        {
            Matrix r = GenerateNoiseMatrix(MatrixSize, MatrixSize);
            Matrix e1 = GenerateNoiseMatrix(MatrixSize, MatrixSize);
            Matrix e2 = GenerateNoiseVectorMatrix(MatrixSize); // Adjusted method call
            Matrix C1 = publicKey.Multiply(r).Add(e1);
            Matrix C2 = r.Transpose().Multiply(matrixA).Add(e2);

            byte[] sharedSecret = HashMatrices(C1, C2);
            return (new Matrix(C1, C2), sharedSecret);
        }
        public byte[] Decapsulate(Matrix ciphertext, Matrix privateKey)
        {
            //Matrix C1 = ExtractC1(ciphertext);
            //Matrix C2 = ExtractC2(ciphertext);
            //Matrix r_prime = C2.Subtract(privateKey.Transpose().Multiply(C1));

            //return HashMatrices(C1, r_prime);
            // Extract C1 and C2 from the ciphertext.
            Matrix C1 = ExtractC1(ciphertext);
            Matrix C2 = ExtractC2(ciphertext);

            Matrix r_prime = privateKey.Transpose().Multiply(C1); // Placeholder for the correct operation.

            // Check if the recomputed C2 matches the C2 from the ciphertext.
            // This is a placeholder and might not be the correct way to verify the shared secret.
            Matrix C2_recomputed = r_prime.Transpose().Multiply(matrixA); // You might need to re-add e2 if you have it.


            return HashMatrices(C1, r_prime);

            // Only if C2 matches, we proceed. Otherwise, we throw an error or handle the case when the verification fails.
            //if (C2.Equals(C2_recomputed))
            //{
            //    // If C2 matches, hash the matrices to produce the shared secret.
            //    return HashMatrices(C1, r_prime);
            //}
            //else
            //{
            //    throw new InvalidOperationException("Decapsulation failed: C2 mismatch.");
            //}
        }

        private byte[] HashMatrices(Matrix m1, Matrix m2)
        {
            // Convert both matrices to byte arrays
            byte[] m1Bytes = MatrixToByteArray(m1);
            byte[] m2Bytes = MatrixToByteArray(m2);

            // Concatenate the byte arrays
            byte[] concatenated = new byte[m1Bytes.Length + m2Bytes.Length];
            Buffer.BlockCopy(m1Bytes, 0, concatenated, 0, m1Bytes.Length);
            Buffer.BlockCopy(m2Bytes, 0, concatenated, m1Bytes.Length, m2Bytes.Length);

            // Compute the hash of the concatenated byte arrays
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(concatenated);
            }
        }
        private Matrix GenerateNoiseVectorMatrix(int size)
        {
            Matrix vectorMatrix = new Matrix(size, size);

            for (int i = 0; i < size; i++)
            {
                byte[] randomNumber = new byte[4]; // Size of an integer
                rng.GetBytes(randomNumber);
                int noiseValue = BitConverter.ToInt32(randomNumber, 0);
                int noise = Mod(noiseValue, NoiseMax) - NoiseMax / 2; // Example to create a simple noise

                for (int j = 0; j < size; j++)
                {
                    vectorMatrix[j, i] = noise;
                }
            }

            return vectorMatrix;
        }
        private byte[] MatrixToByteArray(Matrix matrix)
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    // Assuming the matrix stores ints, convert each element to bytes
                    bytes.AddRange(BitConverter.GetBytes(matrix[i, j]));
                }
            }

            return bytes.ToArray();
        }

        private Matrix ExtractC1(Matrix ciphertext)
        {
            int halfRows = ciphertext.Rows / 2;
            Matrix C1 = new Matrix(halfRows, ciphertext.Columns);

            for (int i = 0; i < halfRows; i++)
            {
                for (int j = 0; j < ciphertext.Columns; j++)
                {
                    C1[i, j] = ciphertext[i, j];
                }
            }

            return C1;
        }
        private Matrix ExtractC2(Matrix ciphertext)
        {
            int halfRows = ciphertext.Rows / 2;
            Matrix C2 = new Matrix(halfRows, ciphertext.Columns);

            for (int i = halfRows; i < ciphertext.Rows; i++)
            {
                for (int j = 0; j < ciphertext.Columns; j++)
                {
                    C2[i - halfRows, j] = ciphertext[i, j];
                }
            }

            return C2;
        }

        private Matrix GenerateNoiseVector(int length)
        {
            Matrix vector = new Matrix(length, 1); // Assuming a vector is a matrix with one column

            byte[] randomNumber = new byte[4]; // Size of an integer

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(randomNumber);
                int noiseValue = BitConverter.ToInt32(randomNumber, 0);
                vector[i, 0] = Mod(noiseValue, NoiseMax) - NoiseMax / 2; // Example to create a simple noise
            }

            return vector;
        }
    }
}
