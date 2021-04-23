using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Steps that are completed by pressing space
public class SpaceStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
