using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Factory
{
    public interface ItemFactory
    {
        Item CreateSword();
        Item CreateArmature();
        Item CreatePotion();
    }
}
