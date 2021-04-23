using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDescStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject confirmation = GameObject.Find("Confirmation");
        return confirmation != null && confirmation.activeSelf;
    }
}
