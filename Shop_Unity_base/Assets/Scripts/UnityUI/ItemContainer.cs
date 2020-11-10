using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Model;
using View;

//This class is applied to a button that represents an Item in the View. It is a visual representation of the item
//when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to the one specified in the Item data,
//and it enables or disables the checkbox to indicate if the Item is selected.
public class ItemContainer : MonoBehaviour
{
    public ShopView shopView { get; set; }

    //link to the checkmark image (set in prefab)
    [SerializeField]
    private GameObject checkMark;

    //link to the original item (set in Initialize)
    private Item item;
    
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItem()
    //------------------------------------------------------------------------------------------------------------------------
    public Item GetItem() {
        return item;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(ShopView shopView, Item item, bool isSelected) {
        //store item
        this.shopView = shopView;

        this.item = item;

        //set checkmark visibility
        if (isSelected) {
            checkMark.SetActive(true);
        }

        //set button image
        Image image = GetComponentInChildren<Image>();
        Sprite sprite = SpriteCache.Get("item");
        if (sprite != null) {
            image.sprite = sprite;
        }


    }
}
