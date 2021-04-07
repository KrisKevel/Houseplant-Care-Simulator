using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

                if (hit.collider.tag == "Plant")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(BringUpPlantPanel(hit.collider.gameObject));
                    }
                    else if (Input.GetMouseButtonDown(1) && _clickable)
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

    private IEnumerator BringUpPlantPanel(GameObject plant)
    {
        print("Coroutine started");
        GameObject player = GameObject.Find("Player").gameObject;
        HouseplantHealth plantHealth = plant.GetComponent<HouseplantHealth>();

        while (Vector3.Distance(player.transform.position, plant.transform.position) > GameManager.Instance.AOE ||
            player.GetComponent<NavMeshAgent>().velocity.magnitude != 0)
        {
            yield return null;
        }

        if (plantHealth.Dead)
        {
            Events.OpenDeadPanel(plantHealth);
            yield break;
        }
        else
        {
            Events.OpenMoistureMeter(plantHealth);
            yield break;
        }
    }
}
