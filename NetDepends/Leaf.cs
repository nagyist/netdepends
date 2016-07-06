using System.Runtime.Serialization;

namespace NetDepends
{
    [DataContract]
    public class Leaf : Dependency
    {
        [DataMember]
        string full;

        [DataMember]
        System.Collections.ArrayList dependencies;

        public Leaf(string full, string name, Dependency parent)
            : base(name, parent)
        {
            this.full = full;
            this.dependencies = new System.Collections.ArrayList();
        }

        public void AddDependency(string dependency)
        {
            dependencies.Add(dependency);
        }
    }
}
