using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Computer : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    private bool _clickable = false;

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _clickable = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < GameManager.Instance.AOE;

                if (hit.collider.name == "monitor" && Input.GetMouseButton(0) && _clickable)
                {
                    Events.UseComputer();
                }
            }
        }
    }
}
