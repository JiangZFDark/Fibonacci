using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Fibo
    {
        public IEnumerable<int> GenerateFibonacci(int n)
        {
            if (n <= 1)
            {
                yield return 1;
            }
            int a = 1, b = 0;
            for(int i = 1; i <= n; ++i)
            {
                int t = b;
                b = a;
                a += t;
                yield return a;
            }
        }
    }
}
