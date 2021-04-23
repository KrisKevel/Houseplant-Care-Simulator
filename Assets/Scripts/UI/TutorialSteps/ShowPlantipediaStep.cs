using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlantipediaStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject plantipedia = GameObject.Find("Plantipedia");
        GameObject plantInfo = GameObject.Find("PlantInfo");

        return plantipedia != null && plantipedia.activeSelf ||
            plantInfo != null && plantInfo.activeSelf;
    }
}
