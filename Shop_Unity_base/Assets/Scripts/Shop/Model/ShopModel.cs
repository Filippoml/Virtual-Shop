namespace Model
{
    using GXPEngine;
    using GXPEngine.Shop.Composite;
    using GXPEngine.Shop.Factory;
    using GXPEngine.Shop.Observer;
    using System;
    using System.Collections.Generic;
    using View;

    //This class holds the model of our Shop. It contains an ItemList. In its current setup, view and controller need to get
    //data via polling. Advisable is, to set up an event system for better integration with View and Controller.
    public class ShopModel : Observer
    {
        const int MaxMessageQueueCount = 4; //it caches the last four messages
        private List<string> messages = new List<string>();

        private List<Item> itemList = new List<Item>(); //items in the store
        private int selectedItemIndex = 0; //selected item index

        private List<Item> inventoryList = new List<Item>(); //items in the store

        private int playerMoney;

        private bool _currencyEuro;

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopModel()
        //------------------------------------------------------------------------------------------------------------------------        
        public ShopModel()
        {
            populateInventory(16); //currently, it has 16 items

            playerMoney = 50; //set player money to 50

            _currencyEuro = true;

      
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  PopulateInventory()
        //------------------------------------------------------------------------------------------------------------------------        


       
        private void populateInventory(int itemCount)
        {
            ItemSpawner itemSpawner = new ItemSpawner(new ItemFactoryLevel1());
            //ItemSpawner itemSpawner = new ItemSpawner(new ItemFactoryLevel2());

            

            for (int index = 0; index < itemCount; index++)
            {
                itemList.Add(itemSpawner.SpawnItem());
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItem()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the selected item
        public Item GetSelectedItem()
        {
            if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Count)
            {
                return itemList[selectedItemIndex];
            }
            else
            {
                return null;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItem()
        //------------------------------------------------------------------------------------------------------------------------
        //attempts to select the given item, fails silently
        public void SelectItem(Item item)
        {
            if (item != null)
            {
                int index = itemList.IndexOf(item);
                if (index >= 0)
                {
                    selectedItemIndex = index;
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  SelectItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        //attempts to select the item, specified by 'index', fails silently
        public void SelectItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
            {
                selectedItemIndex = index;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetSelectedItemIndex()
        //------------------------------------------------------------------------------------------------------------------------
        //returns the index of the current selected item
        public int GetSelectedItemIndex()
        {
            return selectedItemIndex;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns a list with all current items in the shop.
        public List<Item> GetItems()
        {
            return new List<Item>(itemList); //returns a copy of the list, so the original is kept intact, 
                                             //however this is shallow copy of the original list, so changes in 
                                             //the original list will likely influence the copy, apply 
                                             //creational patterns like prototype to fix this. 
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemCount()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the number of items
        public int GetItemCount()
        {
            return itemList.Count;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetItemByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        //tries to get an item, specified by index. returns null if unsuccessful
        public Item GetItemByIndex(int index)
        {
            if (index >= 0 && index < itemList.Count)
            {
                return itemList[index];
            }
            else
            {
                return null;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetMessage()
        //------------------------------------------------------------------------------------------------------------------------        
        //returns the cached list of messages
        public string[] GetMessages()
        {
            return messages.ToArray();
        }

        public void DeleteMessage()
        {
            messages.RemoveAt(0);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AddMessage()
        //------------------------------------------------------------------------------------------------------------------------
        //adds a message to the cache, cleaning it up if the limit is exceeded
        public void AddMessage(string message)
        {
            messages.Add(message);
            while (messages.Count > MaxMessageQueueCount)
            {
                messages.RemoveAt(0);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Buy()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet
        public void Buy()
        {
            Item itemSelect = GetSelectedItem();

            int itemAmount = itemSelect.GetPropertyValue<int>(typeof(AmountProperty));
            int itemCost = itemSelect.GetPropertyValue<int>(typeof(CostProperty));

            if (itemAmount > 0 && playerMoney >= itemSelect.GetPropertyValue<int>(typeof(CostProperty)))
            {
                decreaseMoney(itemCost);              
                itemSelect.decreaseStock();
                inventoryList.Add(itemSelect);
                AddMessage("Successful purchase!");
            }
            else
            {
                if (playerMoney < itemCost)
                {
                    AddMessage("You don't have enough money!");
                }
                else if (itemAmount < 1)
                {
                    AddMessage("Out of stock!");
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Sell()
        //------------------------------------------------------------------------------------------------------------------------        
        //not implemented yet
        public void Sell()
        {
            Item itemSelect = GetSelectedItem();

            if (inventoryList.Contains(itemSelect))
            {
                int itemCost = itemSelect.GetPropertyValue<int>(typeof(CostProperty));
                increaseMoney(itemCost);
                itemSelect.increaseStock();
                inventoryList.Remove(itemSelect);
                AddMessage("Sold!");
            }
            else
            {
                AddMessage("You don't have this item!");
            }
        }


        public int GetPlayerMoney()
        {
            return playerMoney;
        }

        public string GetPlayerMoneyString()
        {
            if(_currencyEuro)
            {
                return playerMoney + "€";
            }
            else
            {
                return playerMoney + "$";
            }
        }

        private void increaseMoney(int value)
        {
            playerMoney += value;
        }

        private void decreaseMoney(int value)
        {
            playerMoney -= value;
        }

        
    

        public void EventCalled(object sender, EventArgs e)
        {
            _currencyEuro = !_currencyEuro;
        }
    }
}
