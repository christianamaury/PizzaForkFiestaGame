using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    //Eventually add more purchase type items for the user;

    public enum PurchaseType { removeAds }
    public PurchaseType purchaseType;

    public void ClickOnPurchaseButton()
    {
        //Playing UI sound;
        FindObjectOfType<AudioManager>().Play("UIClick");

        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                IAPurchase.Instance.BuyRemoveAllAds();
                break;
        }
    }

}
