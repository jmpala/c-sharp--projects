
// var lines = File.ReadAllLines("Text.txt");
// foreach (var line in lines)
// { 
//     Console.WriteLine(line);
// }

// Task -> foundation of async C#
// Is not a Thread

// File.ReadAllLinesAsync("Text1.txt")
//     .ContinueWith(t =>
//     {
//         if (t.IsFaulted)
//         {
//             Console.Error.WriteLine(t.Exception);
//             return;
//         }
//         
//         var lines = t.Result;
//         // Task us completed
//         foreach (var line in lines)
//         { 
//             Console.WriteLine(line);
//         }
//     });

static async Task ReadFile()
{
    var lines = await File.ReadAllLinesAsync("Text.txt");
    foreach (var line in lines)
    { 
        Console.WriteLine(line);
    }
}

await ReadFile();

static async Task<int> GetDataFromNetworkAsync()
{
    // Simulate network call
    await Task.Delay(150);
    var result = 42;
    return result;
}

var res = await GetDataFromNetworkAsync();
Console.WriteLine(res);

Func<Task<int>> getDataFromNetworkViaLambda = async () =>
{
    await Task.Delay(150);
    var result = 42;

    return result;
};

Console.WriteLine("I am here!");