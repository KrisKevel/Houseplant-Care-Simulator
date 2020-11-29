﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool _dead = false;

    void Start()
    {
        _currentTimeH = TimeManager.Instance.GetCurrentTimeHours();
        _lastTimeHealthRemoved = TimeManager.Instance.GetCurrentTimeHours();

        _currentWaterLevel = Houseplant.WaterRequirement;
        _currentLightLevel = Houseplant.LightRequirement;

        _minWaterLevel = Houseplant.WaterRequirement - Houseplant.WaterReqDiff;
        _maxWaterLevel = Houseplant.WaterRequirement + Houseplant.WaterReqDiff;
        _minLightLevel = Houseplant.LightRequirement - Houseplant.LightReqDiff;
        _maxLightLevel = Houseplant.LightRequirement + Houseplant.LightReqDiff;
    }

    void Update() 
    {
        if (!_dead)
        {
            if (_currentTimeH != TimeManager.Instance.GetCurrentTimeHours())
            {
                //Increase current water level
                if (Input.GetMouseButton(0))
                    IncreaseWaterLevel();

                //Decrease the current water level
                print("Current water level: " + _currentWaterLevel);
                DecreaseWaterLevel();

                _currentTimeH = TimeManager.Instance.GetCurrentTimeHours();

                //If the plant is unhappy, increase stress, otherwise decrease
                if (_currentWaterLevel < _minWaterLevel || _currentWaterLevel > _maxWaterLevel)
                {
                    RemoveHealth();
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
            print("Health: " + Health);
            if (Health < 0)
            {
                _dead = true;
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
        if (Health + Houseplant.HealthRegenRate > 100)
        {
            Health = 100f;
        }
        else
        {
            Health += Houseplant.HealthRegenRate;
        }
    }


    private void DecreaseWaterLevel()
    {
        if (_currentWaterLevel - Houseplant.WaterConsumption < 0)
        {
            _currentWaterLevel = 0;
        }
        else
        {
            _currentWaterLevel -= Houseplant.WaterConsumption;
        }
    }

    private void IncreaseWaterLevel()
    {
        if (_currentWaterLevel + 20 > 100)
        {
            _currentWaterLevel = 100;
        }
        else
        {
            _currentWaterLevel += 20;
        }
    }

}