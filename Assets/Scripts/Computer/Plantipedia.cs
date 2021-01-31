using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plantipedia : MonoBehaviour
{
    public GridLayoutGroup Content;
    public GameObject TilePrefab;

    void Awake()
    {
        Events.OnOpenPlantipedia += OpenPanel;
    }

    private void Start()
    {
        foreach (HouseplantData plant in GameManager.Instance.Plants)
        {
            GameObject tile = Instantiate(TilePrefab);
            tile.transform.SetParent(Content.transform, false);
            tile.GetComponent<PlantipediaTile>().Plant = plant;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenPlantipedia -= OpenPanel;
    }

    void OpenPanel()
    {
        gameObject.SetActive(true);
    }

    public void ClosePlantipedia()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }
}
