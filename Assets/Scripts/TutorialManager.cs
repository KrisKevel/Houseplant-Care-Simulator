using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Tooltip TutorialStep;
    public Hint[] Hints;
    private int _step;
    private Hint _currentHint;

    // Start is called before the first frame update
    void Start()
    {
        _step = -1;
        NextHint();
        TutorialStep.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_step)
        {
            case 0:
                ClickThePlant();
                break;
            case 1:
                ShowWaterLevel();
                break;
            case 2:
                break;
            case 3:
                ShowLightLevel();
                break;
            case 4:
                ShowComputer();
                break;
            case 5:
                ShowPlantipedia();
                break;
            case 6:
                ShowShop();
                break;
            case 7:
                LastWords();
                break;
            default:
                StartTheGame();
                break;
        }
    }

    private void NextHint()
    {
        if (_step < Hints.Length - 1)
        {
            _step++;
            _currentHint = Hints[_step];
            TutorialStep.SetText(_currentHint.Content, _currentHint.Header);
        }
        else
        {
            StartTheGame();
        }
    }

    private void StartTheGame()
    {
        Events.StartTheGame();
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

    private void ShowWaterLevel()
    {
        // On Enter, next
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextHint();
        }
    }

    public void WaterPlant()
    {
        // Once button clicked, next
        NextHint();
    }

    private void ShowLightLevel()
    {
        // On Enter, next
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextHint();
        }
    }

    private void ShowComputer()
    {
        // Once welcome screen open, next
        GameObject welcomeScreen = GameObject.Find("WelcomeScreen");
        if (welcomeScreen != null && welcomeScreen.activeSelf)
        {
            NextHint();
        }
    }

    private void ShowPlantipedia()
    {
        // Once Plantipedia open, next
        GameObject plantipedia = GameObject.Find("Plantipedia");
        if (plantipedia != null && plantipedia.activeSelf)
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

    private void LastWords()
    {
        // Once Enter, next
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextHint();
        }
    }


    [System.Serializable]
    public class Hint
    {
        public string Header;
        public string Content;
    }
}
