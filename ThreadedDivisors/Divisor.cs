using System;
using System.Collections.Generic;
using System.Numerics;

namespace ThreadedDivisors
{
    class Divisor
    {
        public static List<int> Compute(int n)
        {
            List<int> result = new List<int>();

            for(int i = 1, j = (int)Math.Sqrt(n); i <= j;  i++)
            {
                if(n % i == 0)
                {
                    result.Add(i);
                    int pair = n / i;
                    if(i != pair)
                    {
                        result.Add(pair);
                    }
                }
            }

            return result;
        }
    }
}
