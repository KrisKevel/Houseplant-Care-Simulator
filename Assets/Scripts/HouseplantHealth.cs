using UnityEngine;
using UnityEngine.EventSystems;

public class HouseplantHealth : MonoBehaviour
{
    public float Health = 100f;
    public HouseplantData Houseplant;

    private float _currentWaterLevel;
    private float _minWaterLevel;
    private float _maxWaterLevel;

    private float _currentLightLevel;
    private float _minLightLevel;
    private float _maxLightLevel;

    private int _lastTimeHealthRemoved;
    private int _currentTimeH;

    private LightMeasurer _lightMeasurer;

    [HideInInspector]
    public bool Dead = false;

    void Start()
    {
        _currentTimeH = TimeManager.Instance.GetCurrentTimeHours();
        _lastTimeHealthRemoved = TimeManager.Instance.GetCurrentTimeHours();

        _lightMeasurer = gameObject.GetComponentInChildren<LightMeasurer>();
        _currentLightLevel = _lightMeasurer.GetLuminocity();

        _currentWaterLevel = Houseplant.WaterRequirement;

        _minWaterLevel = Houseplant.WaterRequirement - Houseplant.WaterReqDiff;
        _maxWaterLevel = Houseplant.WaterRequirement + Houseplant.WaterReqDiff;
        _minLightLevel = Houseplant.LightRequirement - Houseplant.LightReqDiff;
        _maxLightLevel = Houseplant.LightRequirement + Houseplant.LightReqDiff;
    }

    void Update() 
    {
        if (!Dead)
        {
            // If an hour has passed, update stats
            if (_currentTimeH != TimeManager.Instance.GetCurrentTimeHours())
            {

                //Decrease the current water level
                DecreaseWaterLevel();

                _currentTimeH = TimeManager.Instance.GetCurrentTimeHours();

                //If the plant is unhappy (watering), increase stress, otherwise decrease
                if (_currentWaterLevel < _minWaterLevel || _currentWaterLevel > _maxWaterLevel)
                {
                    RemoveHealth();
                }
                else
                {
                    RestoreHealth();
                    Events.UpdateStressLevel(-Houseplant.StressRemoved);
                }


                //If the plant is unhappy (light level), increase stress, otherwise decrease
                if (_currentLightLevel < _minLightLevel || _currentLightLevel > _maxLightLevel)
                {
                    RemoveHealth();
                    print("WRONG LIGHT!!!!");
                }
                else
                {
                    RestoreHealth();
                    Events.UpdateStressLevel(-Houseplant.StressRemoved);
                }
            }
        }
    }


    private void RemoveHealth()
    {
        if (_lastTimeHealthRemoved != _currentTimeH)
        {
            Health -= Houseplant.DamageRate;
            if (Health < 0)
            {
                Dead = true;
                Events.UpdateStressLevel(Houseplant.StressAddedOnDeath);
            }
            else
            {
                Events.UpdateStressLevel(Houseplant.StressAdded);
                _lastTimeHealthRemoved = _currentTimeH;
            }
        }
    }

    private void RestoreHealth()
    {
        Health = Mathf.Clamp(Health + Houseplant.HealthRegenRate, 0, 100);
    }


    private void DecreaseWaterLevel()
    {
        _currentWaterLevel = Mathf.Clamp(_currentWaterLevel - Houseplant.WaterConsumption, 0, 100);
    }

    public void IncreaseWaterLevel()
    {
        _currentWaterLevel = Mathf.Clamp(_currentWaterLevel + 0.01f, 0, 100);
    }

    public float GetWaterLevel()
    {
        return _currentWaterLevel;
    }

    public void TossThePlant()
    {
        Destroy(gameObject);
    }
}
