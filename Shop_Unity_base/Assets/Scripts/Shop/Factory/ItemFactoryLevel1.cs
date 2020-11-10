using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Shop.Composite;
using Model;

namespace GXPEngine.Shop.Factory
{
    class ItemFactoryLevel1 : ItemFactory
    {
        public Item CreateArmature()
        {
            Item item = new Item();
            item.Properties = new List<ItemProperty>{

                new NameProperty("Armature 1"),
                new AttackProperty(10),
                new CostProperty(10),
                new AmountProperty(10)
            };           
            return item;
        }

        public Item CreatePotion()
        {
            Item item = new Item();
            item.Properties = new List<ItemProperty>{

                new NameProperty("Potion 1"),
                new AttackProperty(10),
                new CostProperty(10),
                new AmountProperty(10)
            };
            return item;
        }

        public Item CreateSword()
        {
            Item item = new Item();
            item.Properties = new List<ItemProperty>{

                new NameProperty("Sword 1"),
                new AttackProperty(10),
                new CostProperty(10),
                new AmountProperty(10)
            };
            return item;
        }
    }
}
