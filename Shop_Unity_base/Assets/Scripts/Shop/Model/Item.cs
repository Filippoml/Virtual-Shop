
using GXPEngine.Shop.Composite;
using System;
using System.Collections.Generic;

namespace Model
{
    //This class holds data for an Item. Currently it has a name, an iconName and an amount.
    public class Item : PropertiesContainer
    {

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Item()
        //------------------------------------------------------------------------------------------------------------------------
        public Item() : base ()
        {

        }

        public void decreaseStock()
        {
            GetProperty(typeof(AmountProperty)).valueInt--;
        }

        public void increaseStock()
        {
            GetProperty(typeof(AmountProperty)).valueInt++;
        }
    }
}
