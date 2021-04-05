using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public Grid DeliveryGrid;
    private List<KeyVal<HouseplantData, int>> _plantsToBeDelivered = new List<KeyVal<HouseplantData, int>>();
    private List<KeyVal<HouseplantData, int>> _delivered = new List<KeyVal<HouseplantData, int>>();

    private List<Vector3> possiblePositions;

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
        if (GameManager.Instance.CurrentState == GameManager.GameState.game)
        {
            HouseplantData plantData = houseplant.gameObject.GetComponent<HouseplantHealth>().Houseplant;
            _plantsToBeDelivered.Add(new KeyVal<HouseplantData, int>(plantData, plantData.DaysForDelivery));
            Events.DeliveryUpdate(_plantsToBeDelivered);
        }
    }

    void Deliver(bool sleeping)
    {
        if (!sleeping)
        {
            possiblePositions = DeliveryGrid.GetPossiblePositions();
            foreach (KeyVal<HouseplantData, int> pair in _plantsToBeDelivered)
            {
                pair.value -= 1;
                if (pair.value <= 0)
                {
                    if (PlacePlant(pair.key.HouseplantPrefab))
                    {
                        GameManager.Instance.UpdateStress(-pair.key.StressRemovedOnDelivery);
                        _delivered.Add(pair);
                    }
                    else
                    {
                        // Notify player that his carpet is full??
                    }
                }
            }

            foreach (KeyVal<HouseplantData, int> pair in _delivered)
            {
                _plantsToBeDelivered.Remove(pair);
            }
            _delivered.Clear();
            Events.DeliveryUpdate(_plantsToBeDelivered);
        }
    }

    bool PlacePlant(GameObject plant)
    {
        if (possiblePositions.Count == 0) return false;

        foreach (Vector3 position in possiblePositions)
        {
            if (CheckIfFree(position))
            {
                Instantiate(plant).transform.position = position;
                possiblePositions.Remove(position);
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

public class KeyVal<Key, Val>
{
    public Key key { get; set; }
    public Val value { get; set; }

    public KeyVal() { }

    public KeyVal(Key key, Val val)
    {
        this.key = key;
        this.value = val;
    }
}
