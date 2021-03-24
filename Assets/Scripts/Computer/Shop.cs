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
    }

    private void Start()
    {
        foreach (HouseplantData plant in GameManager.Instance.Plants)
        {
            GameObject tile = Instantiate(TilePrefab);
            tile.transform.SetParent(Content.transform, false);
            tile.GetComponent<ShopTile>().Plant = plant;
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnOpenShop -= OpenPanel;
        Events.OnInsufficientFunds -= NotifyOfInsufficientFunds;
        Events.OnBuyPlant -= UpdateFunds;
        Events.OnUseComputer -= UpdateFunds;
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
}
