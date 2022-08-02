using Models;

namespace BoxesProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int, string> t = new BinarySearchTree<int, string>();
            t.AddNode(4, "four");
            t.AddNode(5, "five");
            t.AddNode(1, "one");
            t.AddNode(10, "ten");
            t.AddNode(20, "twenty");
            Console.WriteLine(t);

        }
    }
}