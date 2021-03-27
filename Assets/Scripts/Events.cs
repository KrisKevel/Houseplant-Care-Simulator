using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    // Event invoked in order to update stress level UI
    public static event Action<float> OnUpdateStressUI;
    public static void UpdateStressUI(float stress) => OnUpdateStressUI.Invoke(stress);

    // Event invoked in order to update stress levels
    public static event Action<float> OnUpdateStressLevel;
    public static void UpdateStressLevel(float stress) => OnUpdateStressLevel.Invoke(stress);

    // Event invoked in order to open menu of the game
    public static event Action OnOpenMenu;
    public static void OpenMenu() => OnOpenMenu.Invoke();

    // Event invoked in order to bring up the moisture meter panel
    public static event Action<HouseplantHealth> OnOpenMoistureMeter;
    public static void OpenMoistureMeter(HouseplantHealth houseplant) => OnOpenMoistureMeter.Invoke(houseplant);

    // Event invoked in order to bring up the dead plant panel
    public static event Action<HouseplantHealth> OnOpenDeadPanel;
    public static void OpenDeadPanel(HouseplantHealth houseplant) => OnOpenDeadPanel.Invoke(houseplant);

    // Event invoked in order to bring up the computer screen
    public static event Action OnUseComputer;
    public static void UseComputer() => OnUseComputer.Invoke();

    // Event invoked in order to open the shop window on the computer
    public static event Action OnOpenShop;
    public static void OpenShop() => OnOpenShop.Invoke();

    // Event invoked in order to open the Plantipedia window on the computer
    public static event Action OnOpenPlantipedia;
    public static void OpenPlantipedia() => OnOpenPlantipedia.Invoke();

    // Event invoked in order to open the welcome screen on the computer
    public static event Action OnOpenWelcomeScreen;
    public static void OpenWelcomeScreen() => OnOpenWelcomeScreen.Invoke();

    // Event invoked in order to buy a plant from the shop
    public static event Action<GameObject> OnBuyPlant;
    public static void BuyPlant(GameObject houseplant) => OnBuyPlant.Invoke(houseplant);

    // Event invoked in order to trigger workday start/end
    public static event Action<bool> OnToggleWork;
    public static void ToggleWork(bool toggle) => OnToggleWork.Invoke(toggle);

    // Event invoked in order to trigger workday start/end
    public static event Action<bool> OnToggleSleep;
    public static void ToggleSleep(bool toggle) => OnToggleSleep.Invoke(toggle);

    // Event invoked in order to notify player about insufficient funds
    public static event Action OnInsufficientFunds;
    public static void InsufficientFunds() => OnInsufficientFunds.Invoke();

    // Event invoked in order to bring up the game over menu
    public static event Action OnGameOver;
    public static void GameOver() => OnGameOver.Invoke();

    // Event invoked in order to bring up the win menu
    public static event Action OnWin;
    public static void Win() => OnWin.Invoke();

    // Event invoked in order to show plant care information
    public static event Action<HouseplantData> OnBringUpPlantInfo;
    public static void BringUpPlantInfo(HouseplantData data) => OnBringUpPlantInfo.Invoke(data);

    // Event invoked every hour
    public static event Action OnHourPassed;
    public static void HourPassed() => OnHourPassed.Invoke();

    // Event invoked once tutorial is over
    public static event Action OnGameStart;
    public static void StartTheGame() => OnGameStart.Invoke();

    // Event invoked once tutorial is over
    public static event Action<Vector3> OnPlacePlant;
    public static void PlacePlant(Vector3 clickPos) => OnPlacePlant.Invoke(clickPos);
}
