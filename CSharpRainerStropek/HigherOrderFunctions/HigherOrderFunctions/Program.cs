using System;

Func<int, int> calculator = CreateCalculator();
Console.WriteLine(calculator(1)); // Call happens outside scope of CreateCalculator()

// Factory Pattern
// This function returns a delegate
Func<int, int> CreateCalculator()
{
    var factor = 42; // The factor gets promoted to the heap (closure)
    return n => n * factor; // The lambda method captures factor
}

// Behind the scenes C# makes the following
BestFriends CreateCalculatorInternal()
{
    return new BestFriends
    {
        factor = 42
    };
}

class BestFriends
{
    public int factor;
    public int Calculator(int n) => n * factor;
}