using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace FactorialService {
    class Program {
        static void Main (string[] args) {
            var host = new WebHostBuilder ()
                .UseKestrel ()
                .UseUrls ("http://*:5001/")
                .UseContentRoot (Directory.GetCurrentDirectory ())
                .UseStartup<Startup> ()
                .Build ();

            host.Run ();
        }
    }
}