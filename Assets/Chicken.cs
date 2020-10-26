using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Chicken : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask layer;

    bool exploded = false;
    Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        Transform t = GetClosestBuilding(transform.position,10);
        destination = t.position;
        agent.SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Explode(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layer);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<Farm>().AddDamage(500);
        }
    }
    Transform GetClosestBuilding(Vector3 center, float radius)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layer);
        foreach (var h in hitColliders)
        {
            float dist = Vector3.Distance(h.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = h.transform;
                minDist = dist;
            }
        }

        return tMin;
    }
}
