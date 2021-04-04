using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI FundsText;
    public TextMeshProUGUI Funds;
    public GridLayoutGroup Content;
    public GameObject TilePrefab;

    void Awake()
    {
        Events.OnOpenShop += OpenPanel;
        Events.OnBuyPlant += UpdateFunds;
        Events.OnUseComputer += UpdateFunds;
        Events.OnInsufficientFunds += NotifyOfInsufficientFunds;
        Events.OnUseComputer += LoadShop;
    }

    private void Start()
    {
        LoadShop();
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenShop -= OpenPanel;
        Events.OnInsufficientFunds -= NotifyOfInsufficientFunds;
        Events.OnBuyPlant -= UpdateFunds;
        Events.OnUseComputer -= UpdateFunds;
        Events.OnUseComputer -= LoadShop;
    }

    void OpenPanel()
    {
        ChangeTextColor(new Color(255, 255, 255));
        UpdateFunds();
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        Events.OpenWelcomeScreen();
    }

    public void UpdateFunds()
    {
        Funds.text = PlayerPrefs.GetFloat("Funds").ToString();
    }

    public void UpdateFunds(GameObject obj)
    {
        UpdateFunds();
    }

    void NotifyOfInsufficientFunds()
    {
        ChangeTextColor(new Color(255, 0, 0));
    }

    private void ChangeTextColor(Color color)
    {
        FundsText.color = color;
        Funds.color = color;
    }

    private void LoadShop()
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
            tile.GetComponent<ShopTile>().Plant = plant;
        }
    }
}
