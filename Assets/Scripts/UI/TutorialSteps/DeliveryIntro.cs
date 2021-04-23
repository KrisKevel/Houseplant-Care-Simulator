using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryIntroStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FirstPlant.SetActive(true);
            Events.ClearDeliverySystem();
            return true;
        }

        return false;
    }
}
