using System;
using System.Diagnostics;

MeasureTime( () => CountToNearlyInfinity());

Console.WriteLine($"The result is { MeasureTimeFunc(() => CalculateSomeResult()) }");

// Action
static void MeasureTime(Action a)
{
    var watch = Stopwatch.StartNew();
    a();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
}


// Func<>
static int MeasureTimeFunc(Func<int> f)
{
    var watch = Stopwatch.StartNew();
    var res = f();
    watch.Stop();
    Console.WriteLine(watch.Elapsed);
    return res;
}

static void CountToNearlyInfinity()
{
    for (var i = 0; i < 1000000000; i++) ;
}


static int CalculateSomeResult()
{
    for (var i = 0; i < 1000000000; i++) ;
    return 42;
}