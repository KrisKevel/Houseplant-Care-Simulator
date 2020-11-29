using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Houseplant")]
public class HouseplantData : ScriptableObject
{
    public string HouseplantName;
    public GameObject HouseplantPrefab;
    [Range(0, 100)]
    public float WaterRequirement;
    [Range(0, 50)]
    public float WaterReqDiff;
    [Range(0, 100)]
    public float LightRequirement;
    [Range(0, 50)]
    public float LightReqDiff;
}
