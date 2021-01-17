using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoistureMeter : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.tag == "Plant" && Input.GetMouseButton(0))
                {
                    HouseplantHealth plantHealth = hit.collider.gameObject.GetComponent<HouseplantHealth>();
                    if (plantHealth.Dead)
                    {
                        Events.OpenDeadPanel(plantHealth);
                    } 
                    else
                    {
                        Events.OpenMoistureMeter(plantHealth);
                    }
                }
            }
        }
    }
}
