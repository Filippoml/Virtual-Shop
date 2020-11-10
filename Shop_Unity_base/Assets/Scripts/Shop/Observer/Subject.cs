using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Observer
{
    public delegate void Update(object sender, EventArgs e);

    public interface Subject
    {
        event Update onUpdate;
        void Subscribe(Observer subject);
    }
}
