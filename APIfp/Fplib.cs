using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace APIfp
{


    public class CDClib<T>
    {
        List<T> list;
        public Thread Thrd;
        public CDClib() { }
        public CDClib(List<T> l)
        {
            list = l;
        }

        public List<T> ToList()
        {
            return list;
        }

        /*public IEnumerable<U> Map2<U>(IEnumerable<T> list, Func<T, U> res_f)
        {
            foreach (var item in list)
                yield return res_f(item);
        }*/

        public IEnumerable<U> Map<U>(Func<T, U> res_f)
        {
            foreach (var item in list)
                yield return res_f(item);
        }

        public IEnumerable<T> All(List<Func<T, T>> list_f)
        {
            var res = list.Select(list_f[0]);
            for (int i=1; i<list_f.Count; i++)
            {
                res = res.Select(list_f[i]);
            }
            foreach (var item in res)
                yield return item;
        }
                //List<T> list1;
        /*foreach (var item in res)
             vls.Add(item);*/
        //return res;
        /*//vls.Clear();
        foreach (var item in vls)
            vls.Add(list_f[i](item));*/


        public IEnumerable<T> Any(Func<T, bool> filter, List<Func<T, T>> list_f)
        {
            var res = All(list_f);
            res = from n in list
                      where (filter(n))
                      select n;
            foreach (var item in res)
                yield return item;
        }

        /*public IEnumerable<T> Any2(Func<T, bool> filter, Func<T, T> res_f) //IEnumerable<T> list, 
        {
            var res = Map1(list, res_f);
            res = from n in res
                  where (filter(n))
                  select n;

            foreach (var item in res)
                yield return item;
        }*/

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

        public static Func<T, U> Recurse<U>(Func<T, U> f)
        {
            return x => f(x);
        }
    }

    class CDClibN<T> : CDClib<T>
    {
        List<List<T>> list;
        public CDClibN(List<List<T>> a)
        {
            list = a;
        }

        /*public List<List<T>> ToList()
        {
            return list;
        }*/

        public IEnumerable<U> FlatMap<U>(Func<T, U> res_f) //List<List<T>> list, 
        {
            IEnumerable<T> vls = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                vls = vls.Concat(list[i]);
            }
            foreach (var item in vls)
                yield return res_f(item); //expression
        }

        public List<List<T>> Zip()
        {
            List<List<T>> listRes = new List<List<T>>();
            int n = list[0].Count();
            for (int i = 1; i < list.Count(); i++)
            {
                if (list[i].Count() < n) n = list[i].Count();
            }

            for (int i = 0; i < n; i++)
            {
                List<T> listM = new List<T>();
                for (int j = 0; j < list.Count(); j++)
                {
                    listM.Add(list[j][i]);
                }
                listRes.Add(listM);
            }
            return listRes;
        }

        public List<List<T>> Unzip()
        {
            List<List<T>> listRes = new List<List<T>>();
            int n = list[0].Count();
            for (int i = 0; i < n; i++)
            {
                List<T> listM = new List<T>();
                for (int j = 0; j < list.Count(); j++)
                {
                    listM.Add(list[j][i]);
                }
                listRes.Add(listM);
            }
            return listRes;
        }


        }

    /*class CDClibTu<T>
    {
        //List<Tuple<List<T>>> list1 = new List<Tuple<List<T>>>();
        //List<List<Tuple<object>>> list = new List<List<Tuple<object>>>();
        List<Tuple<object>> list1 = new List<Tuple<object>>();
        List<Tuple<object, object>> list2 = new List<Tuple<object, object>>();
        List<Tuple<object, object, object>> list3 = new List<Tuple<object, object, object>>();
        List<Tuple<object, object, object, object>> list4 = new List<Tuple<object, object, object, object>>();
        List<Tuple<object, object, object, object, object>> list5 = new List<Tuple<object, object, object, object, object>>();
        List<Tuple<object, object, object, object, object, object>> list6 = new List<Tuple<object, object, object, object, object, object>>();
        List<Tuple<object, object, object, object, object, object, object>> list7 = new List<Tuple<object, object, object, object, object, object, object>>();
        List<Tuple<object, object, object, object, object, object, object, object>> list8 = new List<Tuple<object, object, object, object, object, object, object, object>>();
        public CDClibTu(ArrayList listArgs)
        {

            var t = new Object();
            switch (listArgs.Count)
            {
                case 1:
                    //t = new Tuple<object>(listArgs[0]);
                    list1.Add(new Tuple<object>(listArgs[0]));
                    break;
                case 2:
                    //t = new Tuple<object, object>(listArgs[0], listArgs[1]);
                    list2.Add(new Tuple<object, object>(listArgs[0], listArgs[1]));
                    break;
                case 3:
                    list3.Add(new Tuple<object, object, object>(listArgs[0], listArgs[1], listArgs[2]));
                    break;
                case 4:
                    list4.Add(new Tuple<object, object, object, object>(listArgs[0], listArgs[1], listArgs[2], listArgs[3]));
                    break;
                case 5:
                    list5.Add(new Tuple<object, object, object, object, object>(listArgs[0], listArgs[1], listArgs[2], listArgs[3], listArgs[4]));
                    break;
                case 6:
                    list6.Add(new Tuple<object, object, object, object, object, object>(listArgs[0], listArgs[1], listArgs[2], listArgs[3], listArgs[4], listArgs[5]));
                    break;
                case 7:
                    list7.Add(new Tuple<object, object, object, object, object, object, object>(listArgs[0], listArgs[1], listArgs[2], listArgs[3], listArgs[4], listArgs[5], listArgs[6]));
                    break;
                case 8:
                    list8.Add(new Tuple<object, object, object, object, object, object, object, object>(listArgs[0], listArgs[1], listArgs[2], listArgs[3], listArgs[4], listArgs[5], listArgs[6], listArgs[7]));
                    break;
                default:
                    break;
            }
        }

        public List<Tuple<object, object>> Zip()
        {
            List<Tuple<object, object>> listRes = new List<Tuple<object, object>>();
            return listRes;
        }

        public List<Tuple<object, object>> Unzip()
        {
            List<Tuple<object, object>> listRes = new List<Tuple<object, object>>();
            return listRes;
        }
    }*/
    

    /*public static class Map
    {

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

        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T2, T3> f, Func<T1, T2> g)
        {
            return x => f(g(x));
        }
    }*/

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

        /*public static IfThen<R> If<R>(IFunctionType<Boolean> cond)
        {
            return new IfThen<R>(cond);
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
            //var vls = new List<int>();
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

            /*vls1 = Map.Any(vls, x=> x>=2, x => x + x);
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

            var f1 = new Func<int, int>(x => x / x);
            var g1 = new Func<int, int>(x =>  x+x);
            var h = f1.Compose(g1);
            var vs = new List<int>();
            IEnumerable<int> vs1 = new List<int>();
            vs.Add(2);
            vs.Add(2);
            vs.Add(3);
            vs1 = Map.Map1(vs, h);
            foreach (var item in vs1)
                Console.Write(item + " ");
            Console.WriteLine();*/
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
