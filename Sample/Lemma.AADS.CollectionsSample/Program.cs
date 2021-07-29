using System;

namespace Lemma.AADS.CollectionsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new int[] { 1, 2, 3, 4, 5 };
            var list = new Structures.MyVector<int>();

            foreach (var i in items)
                list.Add(i);

            foreach (var e in list)
            {
                Console.WriteLine(e);   
            }
            
        }
    }
}
