using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace Fibonacci
{
    public static class Method
    {
        public static Func<T1,T3> Compose<T1,T2,T3>(this Func<T1,T2> f1, Func<T2,T3> f2)
        {
            return x => f2(f1(x));
        }

        public static Func<T1, Func<T2, TResult>> Curry<T1, T2, TResult>(this Func<T1, T2, TResult> func)
        {
            return x => y => func(x, y);
        }

        public static Func<int, int, int> AddTwoNumber()
        {
            return (x, y) => x + y;
        }

        public static Func<int, int, int, int> AddThreeNumber()
        {
            return (x, y, z) => x + y + z;
        }

        public static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func)
        {
            return x => y => z => func(x, y, z);
        }

        public static List<List<int>> TwoSum(int[] nums, int target)
        {
            Hashtable table = new Hashtable();
            List<List<int>> resultList = new List<List<int>>();
            for(int i = 0; i < nums.Length; i++)
            {
                int other = target - nums[i];
                if (table.ContainsKey(other))
                {
                    List<int> list = new List<int>();
                    list.Add((int)table[other]);
                    list.Add(i);
                    resultList.Add(list);
                }
                table.Add(nums[i], i);
            }
            return resultList;
        }

        public static List<List<int>> ThreeSum(int[] nums, int target)
        {
            List<List<int>> resultList = new List<List<int>>();
            Array.Sort(nums);
            for(int i = 0; i < nums.Length; i++)
            {
                int d = target - nums[i];
                for(int j = i + 1, k = nums.Length - 1; j < nums.Length; j++)
                {
                    while (j < k && (nums[j] + nums[k]) > d)
                    {
                        k--;
                    }
                    if (j == k)
                    {
                        break;
                    }
                    if (nums[j] + nums[k] == d)
                    {
                        List<int> list = new List<int>();
                        list.Add(nums[i]);
                        list.Add(nums[j]);
                        list.Add(nums[k]);
                        resultList.Add(list);
                    }
                }
            }
            return resultList;
        }
    }
}
