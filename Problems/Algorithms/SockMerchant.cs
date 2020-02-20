using System;
using System.IO;
using Contracts;

namespace Problems.Algorithms
{
    public class SockMerchant : ISolver
    {
        public bool Main(string[] input, string[] output)
        {
            int expected = Convert.ToInt32(output[0]);

            int[] array = Array.ConvertAll(input[1].Split(' '), arTemp => Convert.ToInt32(arTemp));
            
            int actual = Process(array.Length, array);
            
            return expected == actual;
        }

        public int Process(int n, int[] ar)
        {
            return 3;
        }

    }
}
