using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static event Action<float> OnUpdateStressUI;
    public static void UpdateStressUI(float stress) => OnUpdateStressUI.Invoke(stress);

    public static event Action<float> OnUpdateStressLevel;
    public static void UpdateStressLevel(float stress) => OnUpdateStressLevel.Invoke(stress);

    public static event Action OnOpenMenu;
    public static void OpenMenu() => OnOpenMenu.Invoke();

    public static event Action<HouseplantHealth> OnOpenMoistureMeter;
    public static void OpenMoistureMeter(HouseplantHealth houseplant) => OnOpenMoistureMeter.Invoke(houseplant);

    public static event Action<HouseplantHealth> OnOpenDeadPanel;
    public static void OpenDeadPanel(HouseplantHealth houseplant) => OnOpenDeadPanel.Invoke(houseplant);
}
