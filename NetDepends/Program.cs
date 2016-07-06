using System;
using Mono.Cecil;

namespace NetDepends
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(@"NetDepends - prepare input for itdepends
usage:
    NetDepends <name of json file for output> <list of assemblies to process>");
                return;
            }

            var dependencies = new Dependencies();
            for (int i = 1; i < args.Length; ++i)
                dependencies.ProcessAssembly(args[i]);
            dependencies.Write(args[0]);
        }
    }
}
