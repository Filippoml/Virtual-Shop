using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.Shop.Composite;
using Model;

namespace GXPEngine.Shop.Factory
{
    class ItemFactoryLevel2 : ItemFactory
    {
        public Item CreateArmature()
        {
            Item item = new Item
            {
                Properties = new List<ItemProperty>{
                new NameProperty("Armature 2"),
                new AttackProperty(20),
                new CostProperty(20),
                new AmountProperty(20)
                }
            };
            return item;
        }

        public Item CreatePotion()
        {
            Item item = new Item
            {
                Properties = new List<ItemProperty>{
                new NameProperty("Potion 2"),
                new AttackProperty(20),
                new CostProperty(20),
                new AmountProperty(20)
                }
            };
            return item;
        }

        public Item CreateSword()
        {
            Item item = new Item
            {
                Properties = new List<ItemProperty>{
                new NameProperty("Sword 2"),
                new AttackProperty(20),
                new CostProperty(20),
                new AmountProperty(20)
                }
            };
            return item;
        }
    }
}
