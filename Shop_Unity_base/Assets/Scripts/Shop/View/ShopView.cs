namespace View
{
    using System.Collections.Generic;
    using System.Drawing;
    using UnityEngine;
    using UnityEngine.UI;

    using Model;
    using Controller;
    using GXPEngine.Shop.Composite;
    using GXPEngine.Shop.Observer;
    using System;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ShopController()
    //------------------------------------------------------------------------------------------------------------------------        
    public class ShopView : MonoBehaviour, Subject, Observer
    {
        [SerializeField]
        private LayoutGroup itemLayoutGroup;

        [SerializeField]
        private GameObject itemPrefab;

        [SerializeField]
        private Button buyButton;

        [SerializeField]
        private Button sellButton;

        [SerializeField]
        private Button changeCurrencyButton;

        [SerializeField]
        private Text moneyText;


        public ShopModel shopModel { get; set; }
        private ShopController shopController;

        public event Update onUpdate;

        private bool _currencyEuro;



        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Initialize()
        //------------------------------------------------------------------------------------------------------------------------        
        //this method is used to initialize the view, as we can't use a constructor (monobehaviour)
        public void Initialize(ShopModel shopModel, ShopController shopController)
        {
            this.shopModel = shopModel;
            this.shopController = new ShopController(shopModel);

            RepopulateItemIconView();
            //InitializeButtons();

            Subscribe(this);

            _currencyEuro = true;
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  RepopulateItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //clears the icon gridview and repopulates it with new icons (updates the visible icons)
        public void RepopulateItemIconView() {

            Debug.Log("es");
            ClearIconView();
            PopulateItemIconView();

            moneyText.text = "Money" + shopModel.GetPlayerMoneyString();
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  PopulateItems()
        //------------------------------------------------------------------------------------------------------------------------        
        //adds one icon for each item in the shop
        private void PopulateItemIconView() {
            foreach (Item item in shopModel.GetItems()) {
                AddItemToView(item);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ClearIconView()
        //------------------------------------------------------------------------------------------------------------------------        
        //remove all existing icons in the gridview
        private void ClearIconView() {
            Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in allIcons) {
                if (child != itemLayoutGroup.transform) {
                    Destroy(child.gameObject);
                }
             }
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AddItemToView()
        //------------------------------------------------------------------------------------------------------------------------        
        //Adds a new icon. An icon is a prefab Button with some additional scripts to link it to the store Item
        private void AddItemToView(Item item) {


            GameObject newItemIcon = GameObject.Instantiate(itemPrefab);

            newItemIcon.GetComponentsInChildren<Text>()[0].text = item.GetPropertyValue<string>(typeof(NameProperty));
            newItemIcon.GetComponentsInChildren<Text>()[1].text = "Amout: " + item.GetPropertyValue<int>(typeof(AmountProperty)).ToString();

            if (_currencyEuro)
            {
                newItemIcon.GetComponentsInChildren<Text>()[2].text = "Cost: " + item.GetPropertyValue<int>(typeof(CostProperty)).ToString() + "€";
            }
            else
            {
                newItemIcon.GetComponentsInChildren<Text>()[2].text = "Cost: " + item.GetPropertyValue<int>(typeof(CostProperty)).ToString() + "$";
            }

            newItemIcon.transform.SetParent(itemLayoutGroup.transform);
            newItemIcon.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

            ItemContainer itemContainer = newItemIcon.GetComponent<ItemContainer>();
            Debug.Assert(itemContainer != null);
            bool isSelected = (item == shopModel.GetSelectedItem());
            itemContainer.Initialize(this, item, isSelected);

        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  InitializeButtons()
        //------------------------------------------------------------------------------------------------------------------------        
        //This method adds a listener to the 'Buy' and 'Sell' button. They are forwarded to the controller to the shop.
        private void InitializeButtons() {
            buyButton.onClick.AddListener(
                delegate {
                    shopModel.Buy();
                }
            );
            sellButton.onClick.AddListener(
                delegate {
                    shopModel.Sell();
                }
            );
            changeCurrencyButton.onClick.AddListener(
                delegate {
                    NotifyObservers();
                }
            );
        }

        public void Subscribe(Observer subject)
        {
            onUpdate += subject.EventCalled;
        }


        public void NotifyObservers()
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