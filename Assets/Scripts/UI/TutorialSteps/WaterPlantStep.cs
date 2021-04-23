using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlantStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        return FirstPlant.GetComponent<HouseplantHealth>().GetWaterLevel() >= 75;
    }
}
