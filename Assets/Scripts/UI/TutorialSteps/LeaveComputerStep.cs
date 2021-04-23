using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveComputerStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject monitor = GameObject.Find("Log out");
        return monitor == null || !monitor.activeSelf;
    }
}
