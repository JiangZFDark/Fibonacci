using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            #region fibonacci
            Console.WriteLine("Fibonacci:");
            var fibo = new Fibo().GenerateFibonacci(5);
            foreach (var num in fibo)
            {
                Console.WriteLine(num);
            }
            #endregion

            #region Compose
            Console.WriteLine("Compose:");
            Func<int, double> log10 = x => Math.Log10(x);
            Func<double, string> toString = x => x.ToString();
            var log10ToString = log10.Compose(toString);
            Console.WriteLine(log10ToString(100));
            #endregion

            #region Curry
            Console.WriteLine("Curry:");
            var add = Method.AddTwoNumber();
            var addCurry = add.Curry();
            var addResult = addCurry(1)(2);
            var addThree = Method.AddThreeNumber();
            var addThreeCurry = addThree.Curry();
            var addThreeResult = addThreeCurry(1)(2)(3);
            #endregion

            #region SelectManyDealWithStruct
            Console.WriteLine("SelectManyDealWithStruct:");
            Maybe<int> data1 = MayBe.Just<int>(5);
            Maybe<int> data2 = MayBe.Nothing<int>();
            Maybe<int> data3 = MayBe.Just<int>(3);
            Maybe<int> data4 = MayBe.Just<int>(4);
            var result1 = (from dataA in data1 from dataB in data2 select dataA + dataB);
            var result2 = (from dataA in data1 from dataC in data3 select dataA + dataC);
            var result3 = (from dataA in data1 from dataD in data4 from dataC in data3 select dataA + dataD + dataC);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine(result3);
            #endregion

            #region 笛卡尔计算
            Console.WriteLine("笛卡尔计算:");
            int[] array1 = { 1, 2, 3, 4, 5 };
            int[] array2 = { 5, 4, 3, 2, 1 };
            var result4 = array1.SelectMany(a1 => array2, (a1, a2) => $"{a1}+{a2}={a1 + a2}");
            foreach (var ret in result4)
            {
                Console.WriteLine(ret);
            }
            #endregion

            #region 两数求和问题
            Console.WriteLine("两数求和问题:");
            int[] nums1 = new int[] { 5, 12, 6, 3, 9, 2, 1, 7 };
            var list1 = Method.TwoSum(nums1, 13);
            list1.ForEach(l =>
            {
                Console.WriteLine(string.Join(",", l.ToArray()));
            });
            #endregion

            #region 三数求和问题
            Console.WriteLine("三数求和问题:");
            int[] nums2 = new int[] { 5, 12, 6, 3, 9, 2, 1, 7 };
            var list2 = Method.ThreeSum(nums2, 13);
            list2.ForEach(l =>
            {
                Console.WriteLine(string.Join(",", l.ToArray()));
            });
            #endregion

            Console.ReadKey();
        }
    }
}
