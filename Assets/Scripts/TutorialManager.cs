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
        _step = 0;
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
                WaterPlant();
                break;
            case 3:
                ShowLightLevel();
                break;
            case 4:
                ShowComputer();
                break;
            case 5:
                ShowPlantepedia();
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
        if (true)
        {
            NextHint();
        }
    }

    private void ShowWaterLevel()
    {
        // On click, next
        if (true)
        {
            NextHint();
        }
    }

    private void WaterPlant()
    {
        // Once button clicked, next
        if (true)
        {
            NextHint();
        }
    }

    private void ShowLightLevel()
    {
        // On click, next
        if (true)
        {
            NextHint();
        }
    }

    private void ShowComputer()
    {
        // Once welcome screen open, next
        if (true)
        {
            NextHint();
        }
    }

    private void ShowPlantepedia()
    {
        // Once Plantepedia open, next
        if (true)
        {
            NextHint();
        }
    }

    private void ShowShop()
    {
        // Once shop open, next
        if (true)
        {
            NextHint();
        }
    }

    private void LastWords()
    {
        // Once clicked, next
        if (true)
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
