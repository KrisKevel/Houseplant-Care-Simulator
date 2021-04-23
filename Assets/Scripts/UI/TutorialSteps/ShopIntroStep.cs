using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopIntroStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject shop = GameObject.Find("Shop");
        return shop != null && shop.activeSelf;
    }
}
