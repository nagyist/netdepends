using System.Runtime.Serialization;
using Mono.Cecil;

namespace NetDepends
{
    [DataContract]
    public class Leaf : Dependency
    {
        [DataMember]
        string full;

        [DataMember]
        System.Collections.ArrayList dependencies;

        [DataMember]
        System.Collections.ArrayList missing;

        System.Collections.Generic.HashSet<string> set;

        public Leaf(TypeDefinition type, Branch parent)
            : base(type.Name, parent)
        {
            this.full = type.FullName.Replace(".", "/");
            this.dependencies = new System.Collections.ArrayList();
            this.missing = new System.Collections.ArrayList();
            this.set = new System.Collections.Generic.HashSet<string>();

            foreach (var field in type.Fields)
                this.Add(field.FieldType);

            foreach (var inter in type.Interfaces)
                this.Add(inter);

            foreach (var even in type.Events)
                this.Add(even.EventType);

            foreach (var prop in type.Properties)
                this.Add(prop.PropertyType);

            foreach (var attr in type.CustomAttributes)
                this.Add(attr.Constructor.DeclaringType);

            foreach (var method in type.Methods)
            {
                foreach (var p in method.CustomAttributes)
                    this.Add(p.Constructor.DeclaringType);
                foreach (var p in method.Parameters)
                    this.Add(p.ParameterType);
                this.Add(method.ReturnType);
                if (method.Body == null)
                    continue;

                foreach (var local in method.Body.Variables)
                    this.Add(local.VariableType);

                foreach (var instruction in method.Body.Instructions)
                {
                    var methodReference = instruction.Operand as MethodReference;
                    if (methodReference != null)
                    {
                        this.Add(methodReference.DeclaringType);
                        this.Add(methodReference.ReturnType);
                    }

                    var field = instruction.Operand as FieldDefinition;
                    if (field != null)
                    {
                        this.Add(field.DeclaringType);
                    }

                    var property = instruction.Operand as PropertyDefinition;
                    if (property != null)
                    {
                        this.Add(property.DeclaringType);
                        this.Add(property.PropertyType);
                    }
                }
            }
        }

        private void Add(TypeReference type)
        {
            var a = type.ToString();
            a = a.Replace("<", ",").Replace(">", ",").Replace("`", ",").Replace("[]", "").Replace("&", "").Replace("/", "");
            foreach (var bit in a.Split(','))
            {
                int i;
                if (!System.Int32.TryParse(bit, out i))
                    this.set.Add(bit);
            }
        }

        public void Finalise(System.Collections.Generic.Dictionary<string, Leaf> leaves)
        {
            foreach (var item in this.set)
            {
                var i = item.Replace(".", "/");
                if (i == this.full)
                    continue;
                if (leaves.ContainsKey(item))
                    this.dependencies.Add(i);
                else
                    this.missing.Add(i);
            }
        }
    }
}
