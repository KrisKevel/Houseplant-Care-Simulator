using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlantInfoStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject plantInfo = GameObject.Find("PlantInfo");

        return plantInfo != null && plantInfo.activeSelf;
    }
}
