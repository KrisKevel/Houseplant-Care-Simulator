using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlantStatsStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject plantStats = GameObject.Find("PlantStats");
        return plantStats != null && plantStats.activeSelf;
    }
}
