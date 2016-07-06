using System.Runtime.Serialization.Json;
using Mono.Cecil;

namespace NetDepends
{
    class Dependencies
    {
        Branch root;

        System.Collections.Generic.Dictionary<string, Branch> branches;

        System.Collections.Generic.Dictionary<string, Leaf> leaves;

        public Dependencies()
        {
            root = new Branch("", null);
            branches = new System.Collections.Generic.Dictionary<string, Branch>();
            branches[""] = root;
            leaves = new System.Collections.Generic.Dictionary<string, Leaf>();
        }

        public void Write(string filename)
        {
            foreach (var leaf in this.leaves.Keys)
                this.leaves[leaf].Finalise(this.leaves);

            var settings = new DataContractJsonSerializerSettings();
            settings.KnownTypes = new System.Type[] { typeof(Branch), typeof(Leaf) };
            settings.EmitTypeInformation = System.Runtime.Serialization.EmitTypeInformation.Never;
            var serializer = new DataContractJsonSerializer(typeof(Dependency), settings);
            var writer = System.IO.File.CreateText(filename);
            serializer.WriteObject(writer.BaseStream, root);
            writer.Close();
        }

        public void ProcessAssembly(string assembly)
        {
            ModuleDefinition module = ModuleDefinition.ReadModule(assembly);
            foreach (TypeDefinition type in module.Types)
            {
                if (type.FullName.Contains("`"))
                    continue;

                if (type.FullName.Contains("<"))
                    continue;

                if (!type.IsClass && !type.IsInterface)
                    continue;

                this.Add(type);
            }
        }

        private void Add(TypeDefinition type)
        {
            var parent = GetPath(type.Namespace);
            var leaf = new Leaf(type, parent);
            parent.AddChild(leaf);
            leaves[type.FullName] = leaf;
        }

        private Branch GetPath(string path)
        {
            if (branches.ContainsKey(path))
                return branches[path];
            var name = path;
            var parent = root;
            if (path.Contains("."))
            {
                var i = path.LastIndexOf('.');
                parent = GetPath(path.Substring(0, i));
                name = path.Substring(i + 1);
            }
            var branch = new Branch(name, parent);
            parent.AddChild(branch);
            branches[path] = branch;
            return branch;
        }
    }
}
