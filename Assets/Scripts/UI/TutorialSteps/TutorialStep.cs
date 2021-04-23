using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep
{
    public TutorialManager.Hint Hint;
    public GameObject FirstPlant;

    public abstract bool CheckIfCompleted();
}
