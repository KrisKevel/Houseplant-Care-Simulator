using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Statistics : MonoBehaviour
{
    private float morningStress;
    private float morningFunds;
    private int morningPlants;
    private int morningHappyPlants;
    private int morningUnhappyPlants;

    public TextMeshProUGUI Stress;
    public TextMeshProUGUI StressDiff;
    public TextMeshProUGUI Funds;
    public TextMeshProUGUI FundsDiff;
    public TextMeshProUGUI Plants;
    public TextMeshProUGUI PlantsDiff;
    public TextMeshProUGUI HappyPlants;
    public TextMeshProUGUI HappyPlantsDiff;
    public TextMeshProUGUI UnhappyPlants;
    public TextMeshProUGUI UnhappyPlantsDiff;

    private void Awake()
    {
        Events.OnToggleSleep += ShowStatistics;
    }

    private void OnDestroy()
    {
        Events.OnToggleSleep -= ShowStatistics;
    }

    void Start()
    {
        morningStress = GameManager.Instance.GetStress();
        morningFunds = GameManager.Instance.GetFunds();
        HouseplantHealth[] plantsInScene = FindObjectsOfType<HouseplantHealth>();
        morningPlants = plantsInScene.Length;
        foreach (HouseplantHealth plant in plantsInScene)
        {
            if (plant.Happy) { morningHappyPlants++; }
            else { morningUnhappyPlants++; }
        }
    }

    public void ShowStatistics(bool sleeping)
    {
        if (!sleeping) { return; }

        float currentStress = GameManager.Instance.GetStress();
        float currentFunds = GameManager.Instance.GetFunds();

        HouseplantHealth[] plantsInScene = FindObjectsOfType<HouseplantHealth>();
        int currentPlants = plantsInScene.Length;
        int currentHappyPlants = 0;
        int currentUnhappyPlants = 0;
        foreach (HouseplantHealth plant in plantsInScene)
        {
            if (plant.Happy) { currentHappyPlants++; }
            else { currentUnhappyPlants++; }
        }

        Stress.text = System.Math.Round(currentStress, 1).ToString();
        double stressDiff = System.Math.Round(currentStress - morningStress, 1);
        if (stressDiff > 0)
        {
            StressDiff.color = Color.red;
            StressDiff.text = "( +" + stressDiff + " )";
        }
        else if (stressDiff == 0)
        {
            HappyPlantsDiff.color = Color.white;
            HappyPlantsDiff.text = "( " + stressDiff + " )";
        }
        else
        {
            StressDiff.color = Color.green;
            StressDiff.text = "( " + stressDiff + " )";
        }

        Funds.text = currentFunds.ToString();
        double fundsDiff = System.Math.Round(currentFunds - morningFunds, 1);
        if (fundsDiff > 0)
        {
            FundsDiff.color = Color.green;
            FundsDiff.text = "( +" + fundsDiff + " )";
        }
        else if (fundsDiff == 0)
        {
            HappyPlantsDiff.color = Color.white;
            HappyPlantsDiff.text = "( " + fundsDiff + " )";
        }
        else
        {
            FundsDiff.color = Color.red;
            FundsDiff.text = "( " + fundsDiff + " )";
        }

        Plants.text = currentPlants.ToString();
        int plantsDiff = currentPlants - morningPlants;
        if (plantsDiff > 0)
        {
            PlantsDiff.color = Color.green;
            PlantsDiff.text = "( +" + plantsDiff + " )";
        }
        else if (plantsDiff == 0)
        {
            PlantsDiff.color = Color.white;
            PlantsDiff.text = "( " + plantsDiff + " )";
        }
        else
        {
            PlantsDiff.color = Color.red;
            PlantsDiff.text = "( " + plantsDiff + " )";
        }

        HappyPlants.text = currentHappyPlants.ToString();
        int happyPlantsDiff = currentHappyPlants - morningHappyPlants;
        if (happyPlantsDiff > 0)
        {
            HappyPlantsDiff.color = Color.green;
            HappyPlantsDiff.text = "( +" + happyPlantsDiff + " )";
        }
        else if (happyPlantsDiff == 0)
        {
            HappyPlantsDiff.color = Color.white;
            HappyPlantsDiff.text = "( " + happyPlantsDiff + " )";
        }
        else
        {
            HappyPlantsDiff.color = Color.red;
            HappyPlantsDiff.text = "( " + happyPlantsDiff + " )";
        }

        UnhappyPlants.text = currentUnhappyPlants.ToString();
        int unhappyPlantsDiff = currentUnhappyPlants - morningUnhappyPlants;
        if (unhappyPlantsDiff > 0)
        {
            UnhappyPlantsDiff.color = Color.red;
            UnhappyPlantsDiff.text = "( +" + unhappyPlantsDiff + " )";
        }
        else if (unhappyPlantsDiff == 0)
        {
            UnhappyPlantsDiff.color = Color.white;
            UnhappyPlantsDiff.text = "( " + unhappyPlantsDiff + " )";
        }
        else
        {
            UnhappyPlantsDiff.color = Color.green;
            UnhappyPlantsDiff.text = "( " + unhappyPlantsDiff + " )";
        }

        morningStress = currentStress;
        morningFunds = currentFunds;
        morningPlants = currentPlants;
        morningHappyPlants = currentHappyPlants;
        morningUnhappyPlants = currentUnhappyPlants;
    }
}
