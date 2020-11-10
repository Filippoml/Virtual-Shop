using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Shop.Factory
{
    public class ItemSpawner
    {
        private ItemFactory monsterFactory;
        private Random random;
        public ItemSpawner(ItemFactory pMonsterFactory)
        {
            monsterFactory = pMonsterFactory;
            random = new Random();
        }
        //A simplified version of spawn method
        public Item SpawnItem()
        {
            int rand = random.Next(1, 4);
            Item item = null;
            switch(rand)
            {
                case 1:
                    item = monsterFactory.CreateArmature();
                    break;
                case 2:
                    item = monsterFactory.CreatePotion();
                    break;
                case 3:
                    item = monsterFactory.CreateSword();
                    break;

            }
            return item;
        }
    }
}
