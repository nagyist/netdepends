using System.Runtime.Serialization;

namespace NetDepends
{
    [DataContract]
    public class Dependency
    {
        [DataMember]
        string name;

        Branch parent;

        public Dependency(string name, Branch parent)
        {
            this.name = name;
            this.parent = parent;
        }
    }
}
