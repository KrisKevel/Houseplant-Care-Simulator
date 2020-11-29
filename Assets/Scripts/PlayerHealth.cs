using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Stress = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Events.UpdateStressUI(Stress);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
