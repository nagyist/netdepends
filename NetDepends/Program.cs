using System;
using System.Runtime.Serialization.Json;

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

            var root = new Branch("", null);
            for (int i = 1; i < args.Length; ++i)
                ProcessAssembly(root, args[i]);

            var settings = new DataContractJsonSerializerSettings();
            settings.KnownTypes = new System.Type[] { typeof(Branch), typeof(Leaf) };
            settings.EmitTypeInformation = System.Runtime.Serialization.EmitTypeInformation.Never;
            var serializer = new DataContractJsonSerializer(typeof(Dependency), settings);
            var writer = System.IO.File.CreateText(args[0]);
            serializer.WriteObject(writer.BaseStream, root);
            writer.Close();
        }

        static void ProcessAssembly(Branch root, string assembly)
        {

        }
    }
}
