// Simple webserver
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            app.Run(async context =>
            {
                // bad code
                // Thread.Sleep(1000);
                // Task.Delay(1000).Wait();

                // Good code
                await Task.Delay(10000);
                
                await context.Response.WriteAsync("Hello World!");
            });
        });
    })
    .Build()
    .Run();