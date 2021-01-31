using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public Grid DeliveryGrid;
    private Dictionary<HouseplantData, int> _plantsToBeDelivered = new Dictionary<HouseplantData, int>();
    private List<HouseplantData> _delivered = new List<HouseplantData>();

    void Start()
    {
        Events.OnBuyPlant += BuyPlant;
        Events.OnToggleSleep += Deliver;
    }

    private void OnDestroy()
    {
        Events.OnBuyPlant -= BuyPlant;
        Events.OnToggleSleep -= Deliver;
    }

    void BuyPlant(GameObject houseplant)
    {
        HouseplantData plantData = houseplant.gameObject.GetComponent<HouseplantHealth>().Houseplant;
        _plantsToBeDelivered[plantData] = plantData.DaysForDelivery;
    }

    void Deliver(bool sleeping)
    {
        if (!sleeping)
        {
            List<HouseplantData> keys = new List<HouseplantData>(_plantsToBeDelivered.Keys);
            foreach (HouseplantData plant in keys)
            {
                _plantsToBeDelivered[plant] -= 1;
                if (_plantsToBeDelivered[plant] == 0)
                {
                    if (PlacePlant(plant.HouseplantPrefab))
                    {
                        Events.UpdateStressLevel(-plant.StressRemovedOnDelivery);
                        _delivered.Add(plant);
                    }
                    else
                    {
                        // Notify player that his carpet is full??
                    }
                }
            }

            foreach (HouseplantData plant in _delivered)
            {
                _plantsToBeDelivered.Remove(plant);
            }
            _delivered.Clear();
        }
    }

    bool PlacePlant(GameObject plant)
    {
        foreach (Vector3 position in DeliveryGrid.GetPossiblePositions())
        {
            if(CheckIfFree(position))
            {
                Instantiate(plant).transform.position = position;
                return true;
            }
        }

        return false;
    }

    bool CheckIfFree(Vector3 point)
    {
        if (point == null)
        {
            return false;
        }
        Collider[] intersecting = Physics.OverlapSphere(point, 0.01f);
        return intersecting.Length == 0;
    }
}
