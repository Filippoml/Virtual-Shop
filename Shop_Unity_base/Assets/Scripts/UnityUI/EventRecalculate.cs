using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using View;

namespace Assets.Scripts.UnityUI
{
    class EventRecalculate : MonoBehaviour
    {
        [SerializeField]
        private ShopView shopView;

        private EventSystem eventSystem;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool noUIcontrolsInUse = EventSystem.current.IsPointerOverGameObject();

                if (noUIcontrolsInUse)
                {
                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        ItemContainer itemContainer = EventSystem.current.currentSelectedGameObject.GetComponent<ItemContainer>();
                        if (itemContainer != null)
                        {
                            shopView.shopModel.SelectItem(EventSystem.current.currentSelectedGameObject.GetComponent<ItemContainer>().GetItem());
                        }
                        else
                        {
                            switch (EventSystem.current.currentSelectedGameObject.name)
                            {
                                case "BuyButton":
                                    shopView.shopModel.Buy();
                                    break;

                                case "SellButton":
                                    shopView.shopModel.Sell();

                                    break;

                                case "ChangeCurrencyButton":
                                    shopView.NotifyObservers();

                                    break;

                            }
                        }
                    }
                    shopView.RepopulateItemIconView();
                }
            }
        }

        public void test()
        {
            Debug.Log("testa");
        }

 
    }
}
