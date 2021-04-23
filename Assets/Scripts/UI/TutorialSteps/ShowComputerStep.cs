using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowComputerStep : TutorialStep
{
    public override bool CheckIfCompleted()
    {
        GameObject monitor = GameObject.Find("WelcomeText");
        return monitor != null && monitor.activeSelf;
    }
}
