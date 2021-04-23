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
            Arrow.SetObject(_currentStep.Hint.ObjectToPointAt, _currentStep.Hint.RotateArrow);
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
        Events.ToggleSleep(false);
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
            { 4, new SpaceStep() },
            { 5, new DeliveryIntroStep() },
            { 6, new PickUpPlantStep() },
            { 7, new PlacePlantStep() },
            { 8, new ShowComputerStep() },
            { 9, new ShopClosedStep() },
            { 10, new ShowPlantipediaStep() },
            { 11, new ShowPlantInfoStep() },
            { 12, new LeaveComputerStep() },
            { 13, new ShowPlantStatsStep() },
            { 14, new SpaceStep() },
            { 15, new WaterPlantStep() },
            { 16, new ClosePlantStatsStep() },
            { 17, new SpaceStep() },
        };
    }

    [System.Serializable]
    public class Hint
    {
        public int Ordinal;
        public string Header;
        public string Content;
        public GameObject ObjectToPointAt;
        public bool RotateArrow;
    }
}
