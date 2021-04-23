using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlantStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        return GameObject.Find("Destination").GetComponent<Destination>().carrying;
    }
}
