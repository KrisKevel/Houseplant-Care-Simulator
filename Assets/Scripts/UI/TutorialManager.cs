﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Tooltip TutorialStep;
    public Hint[] Hints;
    public Arrow Arrow;
    public GameObject FirstPlant;
    private int _step;
    private Hint _currentHint;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameIsGoing = false;
        _step = -1;
        NextHint();
        TutorialStep.gameObject.SetActive(true);
        Events.OnBringUpPlantInfo += OpenPlantPage;
        Events.OnBuyPlant += PurchasePlant;
    }

    private void OnDestroy()
    {
        Events.OnBringUpPlantInfo -= OpenPlantPage;
        Events.OnBuyPlant -= PurchasePlant;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_step)
        {
            case 0:
                ShowComputer();
                break;
            case 1:
                ShowShop();
                break;
            case 2:
            case 14:
                break;
            case 3:
            case 11:
                LeaveShop();
                break;
            case 4:
                ShowPlant();
                break;
            case 5:
                CheckDestination();
                break;
            case 6:
                CheckLightLevel();
                break;
            case 7:
                ShowComputer();
                break;
            case 8:
                ShowClosed();
                break;
            case 9:
                ShowPlantipedia();
                break;
            case 10:
                OpenPlantInfoPage();
                break;
            case 12:
                ClickThePlant();
                break;
            case 13:
            case 16:
                SpaceButton();
                break;
            case 15:
                CloseStats();
                break;
            default:
                StartTheGame();
                break;
        }

        if (_currentHint.ObjectToPointAt != null &&
            _currentHint.ObjectToPointAt.activeInHierarchy)
        {
            Arrow.ShowArrow();
        }
        else
        {
            Arrow.HideArrow();
        }
    }

    public void NextHint()
    {
        if (_step < Hints.Length - 1)
        {
            _step++;
            _currentHint = Hints[_step];
            if (_step == 12)
            {
                Events.EnableMoistureMeter();
            }
            Arrow.SetObject(_currentHint.ObjectToPointAt, _step == 6);
            TutorialStep.SetText(_currentHint.Content, _currentHint.Header);
        }
        else
        {
            StartTheGame();
        }
    }

    public void StartTheGame()
    {
        Arrow.gameObject.SetActive(false);
        FirstPlant.SetActive(true);
        Events.EnableMoistureMeter();
        GameManager.Instance.StartGame();
        TutorialStep.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void ClickThePlant()
    {
        // If plant status panel open, next
        GameObject plantStats = GameObject.Find("PlantStats");
        if (plantStats != null && plantStats.activeSelf)
        {
            NextHint();
        }
    }

    private void CloseStats()
    {
        // If plant status panel open, next
        GameObject plantStats = GameObject.Find("PlantStats");
        if (plantStats == null || !plantStats.activeSelf)
        {
            NextHint();
        }
    }

    private void SpaceButton()
    {
        // On Enter, next
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextHint();
        }
    }

    public void WaterPlant()
    {
        // Once watering button clicked, next
        if (_step == 14)
        {
            NextHint();
        }
    }

    public void PurchasePlant(GameObject plant)
    {
        if (_step == 2)
        {
            NextHint();
        }
    }
    public void OpenPlantPage(HouseplantData houseplant)
    {
        if (_step == 10)
        {
            NextHint();
        }
    }

    public void OpenPlantInfoPage()
    {
        GameObject plantInfo = GameObject.Find("PlantInfo");
        if (plantInfo != null && plantInfo.activeSelf)
        {
            NextHint();
        }
    }

    private void ShowComputer()
    {
        // Once welcome screen open, next
        GameObject monitor = GameObject.Find("WelcomeText");
        if (monitor != null && monitor.activeSelf)
        {
            NextHint();
        }
    }

    private void ShowPlantipedia()
    {
        // Once Plantipedia open, next
        GameObject plantipedia = GameObject.Find("Plantipedia");
        GameObject plantInfo = GameObject.Find("PlantInfo");
        if (plantipedia != null && plantipedia.activeSelf ||
            plantInfo != null && plantInfo.activeSelf)
        {
            NextHint();
        }
    }

    private void ShowPlant()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FirstPlant.SetActive(true);
            NextHint();
        }
    }

    private void CheckDestination()
    {
        if (FindObjectOfType<Destination>().carrying)
        {
            NextHint();
        }
    }

    private void CheckLightLevel()
    {
        if (FirstPlant.gameObject.GetComponent<HouseplantHealth>().GetLightLevel() == 68)
        {
            NextHint();
        }
    }

    private void ShowClosed()
    {
        GameObject shop = GameObject.Find("Shop");
        if (shop == null || !shop.activeSelf)
        {
            NextHint();
        }
    }

    private void ShowShop()
    {
        // Once shop open, next
        GameObject shop = GameObject.Find("Shop");
        if (shop != null && shop.activeSelf)
        {
            NextHint();
        }
    }

    private void LeaveShop()
    {
        GameObject monitor = GameObject.Find("Log out");
        if (monitor == null || !monitor.activeSelf)
        {
            NextHint();
        }
    }


    [System.Serializable]
    public class Hint
    {
        public string Header;
        public string Content;
        public GameObject ObjectToPointAt;
    }
}
