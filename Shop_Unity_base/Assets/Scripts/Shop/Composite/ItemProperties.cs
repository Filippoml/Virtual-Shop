using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Composite
{
    public class NameProperty : ItemProperty
    {
        public NameProperty(string value) : base(value) { }
    }

    public class AttackProperty : ItemProperty
    {
        public AttackProperty(int value) : base(value) { }
    }

    public class CostProperty : ItemProperty
    {
        public CostProperty(int value) : base(value) { }
    }


    public class AmountProperty : ItemProperty
    {
        public AmountProperty(int value) : base(value) { }
    }
}
