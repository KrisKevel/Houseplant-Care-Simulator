using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlantStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        return FirstPlant.GetComponent<HouseplantHealth>().GetLightLevel() == 68;
    }
}
