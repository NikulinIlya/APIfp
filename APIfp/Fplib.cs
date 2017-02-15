using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APIfp
{


    public class CDClib<T>
    {
        List<T> list;

        public CDClib(List<T> l)
        {
            list = l;
        }

        public List<T> ToList()
        {
            return list;
        }

        public IEnumerable<U> Map1<U>(IEnumerable<T> list, Func<T, U> res_f)
        {
            foreach (var item in list)
                yield return res_f(item);
        }

        public IEnumerable<T> Any(IEnumerable<T> list, Func<T, bool> filter, Func<T, T> res_f)
        {
            var res = from n in list
                      where (filter(n))
                      select n;
            return Map1(res, res_f);
        }

        public IEnumerable<T> Any2(IEnumerable<T> list, Func<T, bool> filter, Func<T, T> res_f)
        {
            var res = Map1(list, res_f);
            res = from n in res
                  where (filter(n))
                  select n;

            foreach (var item in res)
                yield return item;
        }

        public IEnumerable<T> JoinValues(List<T> listAdd)
        {
            return list.Concat(listAdd);
        }

        public Func<T, V> OThen<U, V>(Func<U, V> f, Func<T, U> g) //compose
        {
            return x => f(g(x));
        }

        public T Fold(Func<T, T, T> func)
        {
            bool containsValue = false;
            T firstValue = default(T);
            bool firstValueSet = false;
            IEnumerator<T> iEnum = list.GetEnumerator();
            while (iEnum.MoveNext())
            {
                containsValue = true;
                T tempValue = iEnum.Current;
                if (firstValueSet) tempValue = func(firstValue, tempValue);
                firstValueSet = true;
                firstValue = tempValue;
            }
            if (containsValue)
                return firstValue;
            return default(T);
        }
    }

    class CDClibN<T>
    {
        List<List<T>> list;
        public CDClibN(List<List<T>> a)
        {
            list = a;
        }

        public List<List<T>> ToList()
        {
            return list;
        }
    }

    /*public static Func<T, V> Thenn<T, U, V>(this Func<U, V> f, Func<T, U> g) //compose
    {
        return x => f(g(x));
    }

    public static Func<U, V> Recurse<U, V>( Func<U, V> f)
    {
        return x => f(x);
    }

    public static IEnumerable<int> Zip(List<int> list1, List<int> list2)//WRONG!
    {
        //var vls = new List<int>(); 
        var vls = list1.Zip(list2, (first, second) => first + second);
        return vls;
    }
    //public static IEnumerable<int> JoinValues(List<int> list1, List<int> list2)
    public static IEnumerable<T> JoinValues<T>(List<T> list1, List<T> list2)
    {
        //var vls = list1.Concat(list2);
        return list1.Concat(list2);
    }*/


    public static class Map
    {
        //public static IEnumerable<int> Map(IEnumerable<int> s, Func<int, int> f)
        public static IEnumerable<U> Map1<T, U>(this IEnumerable<T> list, Func<T, U> res_f)
        {
            foreach (var item in list)
                yield return res_f(item);
        }

        public static IEnumerable<U> Map2<T, U>(this IEnumerable<T> list, Func<T, U> res_f)
        {
            var res = list.Select(res_f);
            return res;
        }

        public static IEnumerable<T> Any<T>(this IEnumerable<T> list, Func<T, bool> filter, Func<T, T> res_f)
        {
            var res = from n in list
                      where (filter(n))
                      select n;
            return Map1(res, res_f);
        }

        public static IEnumerable<T> Any2<T>(this IEnumerable<T> list, Func<T, bool> filter, Func<T, T> res_f)
        {
            var res = Map1(list, res_f);
            res = from n in res
                      where (filter(n))
                      select n;
            
            foreach (var item in res)
                yield return item;
        }

        public static T Fold<T>(this IEnumerable<T> list, Func<T, T, T> func)
        {
            bool containsValue = false;
            T firstValue = default(T);
            bool firstValueSet = false;
            IEnumerator<T> iEnum = list.GetEnumerator();
            while (iEnum.MoveNext())
            {
                containsValue = true;
                T tempValue = iEnum.Current;
                if (firstValueSet)
                    tempValue = func(firstValue, tempValue);
                firstValueSet = true;
                firstValue = tempValue;
            }
            if (containsValue)
                return firstValue;
            return default(T);


        }

        /*public static Func<T3, T1> my_chain<T1, T2, T3>(Func<T2, T1> f1, Func<T3, T2> f2)
        {
            return (x => f2(f1(x)));
        }*/

        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T2, T3> f, Func<T1, T2> g)
        {
            return x => f(g(x));
        }
        
        //public static IEnumerable<int> FlatMap(List<List<int>> list, Func<int, int> f)
        /*public static IEnumerable<U> FlatMap<T,U>(List<List<T>> list, Func<T, U> f)
        {
            IEnumerable<T> vls = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                vls = vls.Concat(list[i]);
            }
            return Map1(vls, f); //expression
        }*/
    }

    //if
    class Expression<T>
    {
        public static dynamic Value(List<T> arg1, params List<T>[] lists)
        {
            if (lists == null || lists.Length == 0)
            {
                return new CDClib<T>(arg1);
            }
            else
            {
                List<List<T>> list = new List<List<T>>();
                list.Add(arg1);
                foreach (var l in lists)
                    list.Add(l);
                return new CDClibN<T>(list);
            }
        }

        /*public static IfThen<T> If(Func<dynamic, Boolean> cond)
        {
            return new IfThen<T>(cond);
        }*/
    }


    public static class tes
    {
        public static void tesvoid()
        {
            //ArrayList vls = new ArrayList();
            var vls = new List<int>();
            var vls2 = new List<int>();
            var vls3 = new List<List<int>>();
            IEnumerable<int> vls1 = new List<int>();
            //IEnumerable<int> vls2 = new List<int>();
            //ArrayList al1 = new ArrayList();
            //ArrayList vls1 = new ArrayList(); // IEnumerable
            vls.Add(1);
            vls.Add(2);
            vls.Add(3);
            vls.Add(4);
            vls.Add(1);
            vls.Add(2);
            //vls3.Add(vls);

            vls1 = Map.Any(vls, x=> x>=2, x => x + x);
            foreach (var item in vls1)
                Console.Write(item + " ");
            Console.WriteLine();
            vls1 = Map.Any2(vls, x => x >= 2, x => x + x);
            foreach (var item in vls1)
                Console.Write(item + " ");
            Console.WriteLine();
            vls1 = Map.Map1(vls, x => x + x);
            foreach (var item in vls1)
                Console.Write(item+" ");
            Console.WriteLine();

            var f1 = new Func<int, double>(x => x / x);
            var g1 = new Func<double, double>(x =>  x);
            var h = f1.Compose(g1);
            var vs = new List<double>();
            IEnumerable<double> vs1 = new List<double>();
            vs.Add(2.5);
            vs.Add(2.4);
            vs.Add(3);
            vs1 = Map.Map1(vs, h);
            foreach (var item in vs1)
                Console.Write(item + " ");
            Console.WriteLine();
            //vls1 = Map.Map2(vls, Map.compose(x => x + x, y => y * y));
            //f = Map.Thenn(x => x + x, x => x * x);
            /*vls = vls1.ToList();
            vls3.Add(vls);
            
            //vls1 = vls1.Map(x => x*3);
            foreach (var item in vls3)
                foreach (var item1 in item)
                    Console.Write(item1 + " ");
            Console.WriteLine();

            vls1 = Mapp.FlatMap(vls3, x => x + x);
            foreach (var item in vls1)
                Console.Write(item + " ");
            Console.WriteLine();
            vls = vls1.ToList();
            vls3.Add(vls);
            foreach (var item in vls3)
                foreach (var item1 in item)
                    Console.Write(item1 + " ");
            Console.WriteLine();*/
        }
    }
}
