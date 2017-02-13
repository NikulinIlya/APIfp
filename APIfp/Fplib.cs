using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APIfp
{
    abstract class Expression<TArg, TResult>
    {
        public static Expression<TArg, TResult> Value(TResult constant)  
        {
            return new ValueExpression<TArg, TResult>(constant);
        }

        public static IfThenElseExpression<TArg, TResult> If(Func<TArg, bool> cond)
        {
            return new IfThenElseExpression<TArg, TResult>(cond);
        }

        


    }

    class ValueExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        TResult val;
        public ValueExpression(TResult value) { val = value; }
        public TResult Evaluate(TArg arg) {
            return this.val;
        }
    }

    class IfThenElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public IfThenElseExpression(Func<TArg, bool> cond) { }

        public ThenElseExpression<TArg, TResult> Then(Func<TArg, bool> thenner)
        {
            return new ThenElseExpression<TArg, TResult>(this, thenner);
        }

    }

    class ThenElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public ThenElseExpression(IfThenElseExpression<TArg, TResult> iffer, Func<TArg, bool> elser) { }

        public ElseExpression<TArg,TResult> Else(Func<TArg, TResult> elser)
        {
            return new ElseExpression<TArg, TResult>(this, elser);
        }

    }

    class ElseExpression<TArg, TResult> : Expression<TArg, TResult>
    {
        public ElseExpression(ThenElseExpression<TArg, TResult> ifthenner, Func<TArg, TResult> elser) { }
        /*public Evaluate(TArg arg)
        {
            var condValue = this.cond(arg);
            if (condValue) {
                return this.thenner();
            } else {
                return this.elser();
            }
        }*/
        
    }




    public static class Fplib
    {

        public static Func<T, V> Thenn<T, U, V>(this Func<U, V> f, Func<T, U> g) //compose
        {
            return x => f(g(x));
        }

        public static Func<U, V> Recurse<U, V>( Func<U, V> f) //public static Func<int, int> Recurse(Func<int, int> f)
        {
            return x => f(x);
        }

        public static IEnumerable<int> Zip(List<int> list1, List<int> list2)
        {
            //var vls = new List<int>();
            var vls = list1.Zip(list2, (first, second) => first + second);
            return vls;
        }

        public static IEnumerable<int> JoinValues(List<int> list1, List<int> list2)
        {
            var vls = list1.Concat(list2);
            return vls;
        }
    }

    public static class Mapp
    {
        //public static IEnumerable<int> Map(IEnumerable<int> s, Func<int, int> f)
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> list, Func<T, U> res_f)
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
            return Map(res, res_f);
        }

        public static IEnumerable<T> Any2<T>(this IEnumerable<T> list, Func<T, bool> filter, Func<T, T> res_f)
        {
            var res = Map(list, res_f);
            res = from n in res
                      where (filter(n))
                      select n;
            
            foreach (var item in res)
                yield return item;
        }

        //public static IEnumerable<int> FlatMap(List<List<int>> list, Func<int, int> f)
        public static IEnumerable<U> FlatMap<T,U>(List<List<T>> list, Func<T, U> f)
        {
            IEnumerable<T> vls = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                vls = vls.Concat(list[i]);
            }
            return Map(vls, f); //expression
        }
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
            ArrayList al1 = new ArrayList();
            //ArrayList vls1 = new ArrayList(); // IEnumerable
            vls.Add(1);
            vls.Add(2);
            vls.Add(3);
            vls.Add(4);
            vls.Add(1);
            vls.Add(2);
            //vls3.Add(vls);

            vls1 = Mapp.Any(vls, x=> x>=2, x => x + x);
            foreach (var item in vls1)
                Console.Write(item + " ");
            Console.WriteLine();
            vls1 = Mapp.Any2(vls, x => x >= 2, x => x + x);
            foreach (var item in vls1)
                Console.Write(item + " ");
            Console.WriteLine();
            /*vls1 = Mapp.Map2(vls, x => x + x);
            foreach (var item in vls1)
                Console.Write(item+" ");
            Console.WriteLine();
            vls = vls1.ToList();
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
