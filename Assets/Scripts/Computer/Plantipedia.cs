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
        Events.OnUseComputer += LoadPlantipedia;
    }

    private void Start()
    {
        LoadPlantipedia();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenPlantipedia -= OpenPanel;
        Events.OnUseComputer -= LoadPlantipedia;
    }

    void OpenPanel()
    {
        LoadPlantipedia();
        gameObject.SetActive(true);
    }

    public void ClosePlantipedia()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }

    private void LoadPlantipedia()
    {
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (HouseplantData plant in GameManager.Instance.Plants)
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.tutorial &&
                plant.HouseplantName != "Fittonia albivenis")
            {
                continue;
            }
            GameObject tile = Instantiate(TilePrefab);
            tile.transform.SetParent(Content.transform, false);
            tile.GetComponent<PlantipediaTile>().Plant = plant;
        }
    }
}
