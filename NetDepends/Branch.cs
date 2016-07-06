using System.Runtime.Serialization;

namespace NetDepends
{
    [DataContract]
    public class Branch : Dependency
    {
        [DataMember]
        System.Collections.ArrayList children;

        public Branch(string name, Branch parent)
            : base(name, parent)
        {
            this.children = new System.Collections.ArrayList();
        }

        public void AddChild(Dependency child)
        {
            children.Add(child);
        }
    }
}
