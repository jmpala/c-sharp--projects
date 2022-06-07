namespace DelegatesAndLambdas;

// Looks like strategy pattern, but without interfaces
static class Program
{
    public static void Main(string[] args)
    {
        MathOp f = Add;
        f = Substract;

        Console.WriteLine(f(10, 8));
        
        CalculateAndPrint(1, 1, Add);
        
        // clearer and smaller code
        // Anonymous function, the name is chosen by the compiler
        CalculateAndPrint(1, 1, (x, y) => x + y);
        
        CalculateAndPrint("A","B", (x,y) => x + y);
        CalculateAndPrint(true, true, (x, y) => x && y);
    }

    // Generics
    delegate T Combine<T>(T a, T b);
    
    // Defines a function type
    delegate int MathOp(int x, int y);

    // Functions that requires a delegate
    static void CalculateAndPrint<T>(T x, T y, Combine<T> f)
    {
        var result = f(x, y);
        Console.WriteLine(result);
    }
    
    static int Add(int x, int y)
    {
        return x + y;
    }

    static int Substract(int a, int b)
    {
        return a - b;
    }
}