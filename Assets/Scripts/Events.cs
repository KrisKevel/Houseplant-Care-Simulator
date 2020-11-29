using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static event Action<float> OnUpdateStressUI;
    public static void UpdateStressUI(float stress) => OnUpdateStressUI.Invoke(stress);
}
