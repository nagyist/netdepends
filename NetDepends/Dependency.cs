using System.Runtime.Serialization;

namespace NetDepends
{
    [DataContract]
    public class Dependency
    {
        [DataMember]
        string name;

        Dependency parent;

        public Dependency(string name, Dependency parent)
        {
            this.name = name;
            this.parent = parent;
        }
    }
}
