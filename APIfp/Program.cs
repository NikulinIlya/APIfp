using APIfp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIfp
{
    public static class Functions
    {
        

        

        


    }
    class Program
    {
        //public delegate int Func(int n);

        /*public int ValueInt(int n)
        {
            return n;
        }

        public List<int> ValueList(List<int> n)
        {
            var vls = new List<int>();
            return n;
        }*/

        /*public List<int> Map(List<int> n, Func func1)
        {
            var vls = new IEnumerable<T>();
            vls = n.Select(x => func1(x));
            return vls;
        }*/

        

        static void Main(string[] args)
        {
            //var vls = new List<int>();
            var vls = new List<string>();
            //IEnumerable<int> vls1 = new List<int>();
            IEnumerable<string> vls1 = new List<string>();
            vls.Add("1");
            vls.Add("abc");
            var vls2 = new List<int>();
            vls2.Add(3);
            vls2.Add(1);
            vls2.Add(2);
            for (int i = 0; i < vls.Count; i++)
            {
                //Console.WriteLine(vls[i]);
            }
            //Console.WriteLine(); //static
            tes.tesvoid();
            Console.WriteLine();
            //////////////////////////////////////////////////////////////////
            ArrayList al = new ArrayList();  //стр 936
            ArrayList al1 = new ArrayList();
            int ic = al.Count;
            al.Add('a');
            al.Add(1);
            //al.Add(al1);
            al1.Add(2); al1.Add("n"); al1.Add('b');

            //foreach (var с in al)
                //Console.Write(с + " ");
            //Console.WriteLine("\n");

            //tes to = new APIfp.tes(); //not static
            //to.tesvoid();

            //vls1 = Mapp.Map(vls, x => x + x);
            //foreach (var item in vls1)
            //Console.WriteLine(item);

            //Console.WriteLine(Functions.Map(vls,x => x+1));
            //Console.WriteLine();
            /*vls1 = Functions.Zip(vls, vls2);
            foreach (var item in vls1)
                Console.WriteLine(item);
            Console.WriteLine();*/

            //vls1 = Functions.JoinValues(vls, vls2);
            //foreach (var item in vls1)
                //Console.WriteLine(item);
            //Console.WriteLine();
            Console.ReadKey();
        }
    }
}
