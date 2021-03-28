using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantActions : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private bool _clickable = false;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _clickable = Vector3.Distance(GameObject.Find("Player").transform.position, hit.collider.transform.position) < GameManager.Instance.AOE;

                if (hit.collider.tag == "Plant" && _clickable)
                {
                    if (Input.GetMouseButton(0))
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
                    else if (Input.GetMouseButtonDown(1))
                    {
                        Events.PickUpPlant(hit.collider.gameObject);
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Events.PlacePlant(hit.point);
                }
            }
        }
    }
}
