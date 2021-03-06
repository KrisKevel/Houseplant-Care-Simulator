﻿using UnityEngine;
using UnityEngine.EventSystems;

public class HouseplantHealth : MonoBehaviour
{
    public float Health = 100f;
    public HouseplantData Houseplant;
    public float StressRemovedOnCare = 0.002f;
    public bool Happy;
    public float InitialLight = 0f;
    public float InitialWater = 0f;

    private float _currentWaterLevel;
    private float _minWaterLevel;
    private float _maxWaterLevel;

    private float _currentLightLevel;
    private float _minLightLevel;
    private float _maxLightLevel;

    [HideInInspector]
    public bool Dead = false;

    private void Awake()
    {
        Events.OnHourPassed += HealthCheck;
    }

    void Start()
    {
        Happy = true;
        UpdateLightLevel();

        if (InitialWater == 0)
        {
            _currentWaterLevel = Houseplant.WaterRequirement;
        }
        else
        {
            _currentWaterLevel = InitialWater;
        }

        _minWaterLevel = Houseplant.WaterRequirement - Houseplant.WaterReqDiff;
        _maxWaterLevel = Houseplant.WaterRequirement + Houseplant.WaterReqDiff;
        _minLightLevel = Houseplant.LightRequirement - Houseplant.LightReqDiff;
        _maxLightLevel = Houseplant.LightRequirement + Houseplant.LightReqDiff;
    }

    private void OnDestroy()
    {
        Events.OnHourPassed -= HealthCheck;
    }

    private void HealthCheck()
    {
        if (!Dead)
        {
            //Decrease the current water level
            DecreaseWaterLevel();

            //If the plant is unhappy (watering), increase stress, otherwise decrease
            bool wateringRequirementsMet = (_currentWaterLevel > _minWaterLevel) && (_currentWaterLevel < _maxWaterLevel);
            if (wateringRequirementsMet)
            {
                RestoreHealth();
                UpdateStress(-Houseplant.StressRemoved);
            }
            else
            {
                RemoveHealth();
            }

            UpdateLightLevel();
            //If the plant is unhappy (light level), increase stress, otherwise decrease
            bool lightRequirementsMet = (_currentLightLevel > _minLightLevel) && (_currentLightLevel < _maxLightLevel);
            if (lightRequirementsMet)
            {
                RestoreHealth();
                UpdateStress(-Houseplant.StressRemoved);
            }
            else
            {
                RemoveHealth();
            }

            Happy = wateringRequirementsMet && lightRequirementsMet;
        }
    }


    private void RemoveHealth()
    {
        Health -= Houseplant.DamageRate;
        if (Health < 0)
        {
            SimulatePlantDeath();
        }
        else
        {
            UpdateStress(Houseplant.StressAdded);
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

    public void IncreaseWaterLevel(float diff)
    {
        GameManager.Instance.UpdateStress(-StressRemovedOnCare);
        _currentWaterLevel = Mathf.Clamp(_currentWaterLevel + diff, 0, 100);
    }

    public float GetWaterLevel()
    {
        return _currentWaterLevel;
    }

    public float GetLightLevel()
    {
        UpdateLightLevel();
        return _currentLightLevel;
    }

    public void UpdateLightLevel()
    {
        Placement placement = gameObject.GetComponent<PickUp>().GetCurrentPlacement();
        if (placement == null)
        {
            if (InitialLight != 0)
            {
                _currentLightLevel = InitialLight;
            }
            else
            {
                _currentLightLevel = GameManager.Instance.CarpetLight;
            }
        }
        else
        {
            _currentLightLevel = placement.Light;
        }
    }

    private void UpdateStress(float addedStress)
    {
        GameManager.Instance.UpdateStress(addedStress);
    }

    public void TossThePlant()
    {
        Destroy(gameObject);
    }

    public void SimulatePlantDeath()
    {
        Dead = true;
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer.gameObject.tag == "Leaves")
            {
                renderer.enabled = false;
            }
        }
        UpdateStress(Houseplant.StressAddedOnDeath);
    }
}
