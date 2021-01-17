using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesSystem : MonoBehaviour
{
    public Vector3 DeliveryPosition; 

    void Start()
    {
        Events.OnBuyPlant += BuyPlant;
    }

    private void OnDestroy()
    {
        Events.OnBuyPlant -= BuyPlant;
    }

    void BuyPlant(GameObject houseplant)
    {
        Instantiate(houseplant).transform.position = DeliveryPosition;
    }
}
