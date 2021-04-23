using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Tooltip Tooltip;
    public Hint[] Hints;
    public Arrow Arrow;
    public GameObject FirstPlant;
    
    private int _step;
    private TutorialStep _currentStep;
    private Dictionary<int, TutorialStep> _stepMap;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameIsGoing = false;

        InitializeSteps();

        _step = -1;
        NextHint();

        Tooltip.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentStep.CheckIfCompleted())
        {
            NextHint();
        }
    }

    public void NextHint()
    {
        if (_step < Hints.Length - 1)
        {
            _step++;
            _currentStep = _stepMap[_step];
            if (_step == 12)
            {
                Events.EnableMoistureMeter();
            }
            Arrow.SetObject(_currentStep.Hint.ObjectToPointAt, _step == 6);
            Tooltip.SetText(_currentStep.Hint.Content, _currentStep.Hint.Header);

            if (_currentStep.Hint.ObjectToPointAt != null &&
            _currentStep.Hint.ObjectToPointAt.activeInHierarchy)
            {
                Arrow.ShowArrow();
            }
            else
            {
                Arrow.HideArrow();
            }
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
        Tooltip.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void InitializeSteps()
    {
        InitializeStepMap();

        foreach (Hint hint in Hints)
        {
            _stepMap[hint.Ordinal].Hint = hint;
            _stepMap[hint.Ordinal].FirstPlant = FirstPlant;
        }
    }

    private void InitializeStepMap()
    {
        _stepMap = new Dictionary<int, TutorialStep>
        {
            { 0, new ShowComputerStep() },
            { 1, new ShopIntroStep() },
            { 2, new ShopDescStep() },
            { 3, new LeaveComputerStep() },
            { 4, new DeliveryIntroStep() },
            { 5, new PickUpPlantStep() },
            { 6, new PlacePlantStep() },
            { 7, new ShowComputerStep() },
            { 8, new ShopClosedStep() },
            { 9, new ShowPlantipediaStep() },
            { 10, new ShowPlantInfoStep() },
            { 11, new LeaveComputerStep() },
            { 12, new ShowPlantStatsStep() },
            { 13, new SpaceStep() },
            { 14, new WaterPlantStep() },
            { 15, new ClosePlantStatsStep() },
            { 16, new SpaceStep() },
        };
    }

    [System.Serializable]
    public class Hint
    {
        public int Ordinal;
        public string Header;
        public string Content;
        public GameObject ObjectToPointAt;
    }
}
