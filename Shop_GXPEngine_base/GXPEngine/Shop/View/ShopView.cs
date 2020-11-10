namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using GXPEngine;
    using GXPEngine.Core;

    using Model;
    using Controller;
    using System;
    using GXPEngine.Shop.Observer;
    using GXPEngine.Shop.Composite;

    //This Class draws the icons for the items in the store
    public class ShopView : Canvas, Subject, Observer
    {
        const int Columns = 4;
        const int Spacing = 80;
        const int Margin = 18;

        private bool _currencyEuro;

        private ShopModel shop;
        private ShopController shopController;

        //the icon cache is built in here, that violates the S.R. principle.
        private Dictionary<string, Texture2D> iconCache;

        private List<Canvas> canvasItems;
        public event Update onUpdate;


        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopView()
        //------------------------------------------------------------------------------------------------------------------------        
        public ShopView(ShopModel shop, ShopController shopController) : base(340, 340)
        {
            this.shop = shop;
            this.shopController = shopController;

            iconCache = new Dictionary<string, Texture2D>();

            x = (game.width - width) / 2;
            y = (game.height - height) / 2;

            canvasItems = new List<Canvas>();

            _currencyEuro = true;
            Subscribe(this);



        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Step()
        //------------------------------------------------------------------------------------------------------------------------        
        public void Step()
        {
            HandleNavigation();
            DrawItems();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  HandleNavigation()
        //------------------------------------------------------------------------------------------------------------------------        
        private void HandleNavigation()
        {
            if (Input.GetKeyDown(Key.LEFT))
            {
                MoveSelection(-1, 0);
            }
            if (Input.GetKeyDown(Key.RIGHT))
            {
                MoveSelection(1, 0);
            }
            if (Input.GetKeyDown(Key.UP))
            {
                MoveSelection(0, -1);
            }
            if (Input.GetKeyDown(Key.DOWN))
            {
                MoveSelection(0, 1);
            }

            if (Input.GetKeyDown(Key.SPACE))
            {
                shop.Buy();
            }
            if (Input.GetKeyDown(Key.BACKSPACE))
            {

                shop.Sell();
            }

            if(Input.GetKeyDown(Key.C))
            {
                NotifyObservers();
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  MoveSelection()
        //------------------------------------------------------------------------------------------------------------------------        
        private void MoveSelection(int moveX, int moveY)
        {
            int itemIndex = shop.GetSelectedItemIndex();
            int currentSelectionX = GetColumnByIndex(itemIndex);
            int currentSelectionY = GetRowByIndex(itemIndex);
            int requestedSelectionX = currentSelectionX + moveX;
            int requestedSelectionY = currentSelectionY + moveY;

            if (requestedSelectionX >= 0 && requestedSelectionX < Columns) //check horizontal boundaries
            {
                int newItemIndex = GetIndexFromGridPosition(requestedSelectionX, requestedSelectionY);
                if (newItemIndex >= 0 && newItemIndex <= shop.GetItemCount()) //check vertical boundaries
                {
                    Item item = shop.GetItemByIndex(newItemIndex);
                    shopController.SelectItem(item);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetColumnByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        private int GetIndexFromGridPosition(int column, int row)
        {
            return row * Columns + column;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetColumnByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        private int GetColumnByIndex(int index)
        {
            return index % Columns;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetRowByIndex()
        //------------------------------------------------------------------------------------------------------------------------        
        private int GetRowByIndex(int index)
        {
            return index / Columns; //rounds down
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  DrawBackground()
        //------------------------------------------------------------------------------------------------------------------------        
        private void DrawBackground()
        {
            //graphics.Clear(Color.Red);
            //graphics.Clear(Color.White);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  DrawItems()
        //------------------------------------------------------------------------------------------------------------------------        
        private void DrawItems()
        {
            List<Item> items = shop.GetItems();

            for (int index = 0; index < items.Count; index++)
            {
                Item item = items[index];
                int iconX = GetColumnByIndex(index) * Spacing + Margin;
                int iconY = GetRowByIndex(index) * Spacing + Margin;
                if (item == shop.GetSelectedItem())
                {
                    DrawItem(item, iconX, iconY, true);
                }
                else
                {
                    DrawItem(item, iconX, iconY, false);
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  DrawItem()
        //------------------------------------------------------------------------------------------------------------------------        
        private void DrawItem(Item item, int iconX, int iconY, bool selected)
        {
            string iconName = "item";
            if (selected)
            {
                iconName = "itemSelected";
            }
            Texture2D iconTexture = GetCachedTexture(iconName);


            
            graphics.DrawImage(iconTexture.bitmap, iconX, iconY);
            graphics.DrawString(item.GetPropertyValue<string>(typeof(NameProperty)), SystemFonts.CaptionFont, Brushes.Black, iconX + 3, iconY + 8);
            if (_currencyEuro)
            {
                graphics.DrawString(item.GetPropertyValue<int>(typeof(CostProperty)).ToString() + "€", SystemFonts.CaptionFont, Brushes.Black, iconX + 21, iconY + 23);
            }
            else
            {
                graphics.DrawString(item.GetPropertyValue<int>(typeof(CostProperty)).ToString() + "$", SystemFonts.CaptionFont, Brushes.Black, iconX + 21, iconY + 23);
            }
            graphics.DrawString(item.GetPropertyValue<int>(typeof(AmountProperty)).ToString(), SystemFonts.CaptionFont, Brushes.Black, iconX + 22, iconY + 38);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  GetCachedTexture()
        //------------------------------------------------------------------------------------------------------------------------        
        private Texture2D GetCachedTexture(string filename)
        {
            if (!iconCache.ContainsKey(filename))
            {
                iconCache.Add(filename, new Texture2D("media/" + filename + ".png"));
            }
            return iconCache[filename];
        }

        public void Subscribe(Observer subject)
        {
            onUpdate += subject.EventCalled;
        }


        private void NotifyObservers()
        {
            if (onUpdate != null)   //Ensures that if there are no handle                      //the event won't be raised
            {
                onUpdate(this, EventArgs.Empty);    //We can also replace
                                                    //EventArgs.Empty with our own message
            }
        }

        public void EventCalled(object sender, EventArgs e)
        {
            _currencyEuro = !_currencyEuro;
        }


    }
}