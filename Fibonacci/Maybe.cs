using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    //public class Maybe
    //{
    //    public static Maybe<T> Just<T>(T value)
    //    {
    //        return new Maybe<T> { Value = value, HasValue = true };
    //    }
    //    public static Maybe<T> Nothing<T>()
    //    {
    //        return new Maybe<T>(); 
    //    }
    //}

    //public class Maybe<T> : IEnumerable<T>
    //{
    //    public bool HasValue { get; set; }

    //    public T Value { get; set; }

    //    IEnumerable<T> ToValue()
    //    {
    //        if (HasValue)
    //        {
    //            yield return Value;
    //        }
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        return ToValue().GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return ToValue().GetEnumerator();
    //    }
    //}
    public class MayBe
    {
        public static Maybe<T> Just<T>(T value)
        {
            return new Maybe<T>(true, value);
        }

        public static Maybe<T> Nothing<T>()
        {
            return new Maybe<T>();
        }
    }

    public struct Maybe<T>
    {
        public readonly bool HasValue;

        public readonly T Value;

        public Maybe(bool hasValue, T value)
        {
            HasValue = hasValue;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="collectionSelector">数据选择器(获取第二个数据)</param>
        /// <param name="func">处理函数</param>
        /// <returns></returns>
        public Maybe<TResult> SelectMany<TCollection, TResult>(Func<T, Maybe<TCollection>> collectionSelector, Func<T, TCollection, TResult> func)
        {
            if (!HasValue)
            {
                return MayBe.Nothing<TResult>();
            }
            Maybe<TCollection> collection = collectionSelector(Value);
            if (!collection.HasValue)
            {
                return MayBe.Nothing<TResult>();
            }
            return MayBe.Just(func(Value, collection.Value));
        }

        public Maybe<TResult> Select<TResult>(Func<T, TResult> func)
        {
            if (!HasValue)
            {
                return MayBe.Nothing<TResult>();
            }
            return MayBe.Just(func(Value));
        }

        public override string ToString() => HasValue ? $"Just {Value}" : "Nothing";
    }

}
