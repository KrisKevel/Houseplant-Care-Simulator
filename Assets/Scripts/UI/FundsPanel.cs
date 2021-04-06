using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FundsPanel : MonoBehaviour
{
    private TextMeshProUGUI _funds;
    
    private void Awake()
    {
        Events.OnUpdateFunds += UpdateFunds;
    }

    void Start()
    {
        _funds = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnDestroy()
    {
        Events.OnUpdateFunds -= UpdateFunds;
    }

    private void UpdateFunds(int funds)
    {
        _funds.text = funds.ToString();
    }
}
