
using System.Text;

namespace TypeSystem;

static class Program
{
    public static void Main(string[] args)
    {

        // Boxing
        object o = 42;
        object o2 = 42;
        
        if (o == o2) // Reference compared
        {
            Console.WriteLine("Same");
        }
        
        // equals looks into the box
        // [42] == 42
        if (o.Equals(42))
        {
            Console.WriteLine("Same");
        }
        
        if (o is 42) // True
        {
            Console.WriteLine("Same");
        }


        // Unboxing
        var n = (int)o;

        
        // Static
        int num = 42;
        // num = "FooBar"; // Error
        
        
        // Dynamic
        dynamic dynam = 42;
        dynam = "FooBar";
        
        
        
        // Struct or Class
        void DoSomething()
        {
            var dt = new DateTime();
            var dt2 = dt; // copied

            var sb = new StringBuilder();
            var sb2 = sb;
        }
    }
}