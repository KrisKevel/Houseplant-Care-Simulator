using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlantActions : MonoBehaviour
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
                if (hit.collider.tag == "Plant")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(BringUpPlantPanel(hit.collider.gameObject));
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        Events.RightClickPlant(ray);
                        StartCoroutine(PickUpPlant(hit.collider.gameObject));
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    Events.RightClickPlant(ray);
                    StartCoroutine(PlacePlant(hit.point));
                }
            }
        }
    }

    private IEnumerator BringUpPlantPanel(GameObject plant)
    {
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

    private IEnumerator PickUpPlant(GameObject plant)
    {
        GameObject player = GameObject.Find("Player").gameObject;

        while (Vector3.Distance(player.transform.position, plant.transform.position) > GameManager.Instance.AOE ||
            player.GetComponent<NavMeshAgent>().velocity.magnitude != 0)
        {
            yield return null;
        }

        Events.PickUpPlant(plant);
        yield break;
    }

    private IEnumerator PlacePlant(Vector3 point)
    {
        GameObject player = GameObject.Find("Player").gameObject;

        while (Vector3.Distance(player.transform.position, point) > GameManager.Instance.AOE ||
            player.GetComponent<NavMeshAgent>().velocity.magnitude != 0)
        {
            yield return null;
        }

        Events.PlacePlant(point);
        yield break;
    }
}
