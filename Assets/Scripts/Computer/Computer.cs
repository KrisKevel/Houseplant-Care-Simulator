using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Computer : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.name == "monitor" && Input.GetMouseButton(0))
                {
                    StartCoroutine(BringUpMonitor());
                }
            }
        }
    }

    private IEnumerator BringUpMonitor()
    {
        GameObject player = GameObject.Find("Player").gameObject;

        while (Vector3.Distance(player.transform.position, transform.position) > GameManager.Instance.AOE ||
            player.GetComponent<NavMeshAgent>().velocity.magnitude != 0)
        {
            yield return null;
        }

        Events.UseComputer();
        yield break;
    }
}
