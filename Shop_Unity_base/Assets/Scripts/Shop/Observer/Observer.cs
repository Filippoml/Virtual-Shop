using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Observer
{
    public interface Observer
    {
        void EventCalled(object sender, EventArgs e);
    }
}
