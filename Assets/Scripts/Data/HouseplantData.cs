using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Houseplant")]
public class HouseplantData : ScriptableObject
{
    public string HouseplantName;
    public GameObject HouseplantPrefab;
    public Sprite HouseplantPicture;
    public float Price;


    [Range(0, 100)]
    public float WaterRequirement;
    [Range(0, 50)]
    public float WaterReqDiff;
    // Amount of water consumed per in-game hour
    [Range(0, 1)]
    public float WaterConsumption;

    [Range(0, 100)]
    public float LightRequirement;
    [Range(0, 50)]
    public float LightReqDiff;
    // Amount of damage taken per in-game hour when the plant is not happy
    [Range(0, 10)]
    public float DamageRate;

    // Decrease in stress when plant is happy
    [Range(0, 1)]
    public float StressRemoved;
    [Range(0, 20)]
    public float StressRemovedOnDelivery;

    // Increase in stress when plant is unhappy
    [Range(0, 1)]
    public float StressAdded;
    [Range(0, 10)]
    public float StressAddedOnDeath;

    // Amount of health restored per in-game hour when the care requirements are met
    [Range(0, 20)]
    public float HealthRegenRate;


    // Plant care information
    public string GeneralCareInfo;
    public string WaterRequirementText;
    public string LightRequirementText;

    public int DaysForDelivery;
}
