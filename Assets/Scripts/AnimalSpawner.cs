using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public int amount = 20;
    public GameObject chicken;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null)
                {
                    if (amount > 0)
                    {
                        Debug.Log("Hit " + hit.transform.gameObject.name);
                        Instantiate(chicken, hit.point, Quaternion.identity);
                        amount -= 1;
                    }
                }
        }
    }
}
