using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TreeEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum State { Idle, Walking, Exploding };

public class Chicken : MonoBehaviour
{
    public float destinationOffset;
    public State state;
    public NavMeshAgent agent;
    public LayerMask targetLayer;

    public float explosionRadius;
    public int explosionDamage;
    bool exploded = false;
    Vector3 destination;
    public GameObject explosionParticle;

    Transform curTarget;

    void Start()
    {
        curTarget = GetClosestBuilding(transform.position, 100);
        destination = curTarget.position;
        agent.SetDestination(destination);
        state = State.Walking;
    }

    void Update()
    {
        switch (state) {
            case State.Idle:
                break;
            case State.Walking:

                if (Vector3.Distance(transform.position, destination) < destinationOffset) 
                {
                    agent.destination = transform.position;
                    state = State.Exploding;
                }
                if(curTarget == null)
                {
                    curTarget = GetClosestBuilding(transform.position, 100);
                    destination = curTarget.position;
                    agent.SetDestination(destination);
                }
            break;
            case State.Exploding:
                if (!exploded)
                {
                    transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), 0.01f);
                    StartCoroutine(ExplodeChicken());
                }
                if (exploded)
                {
                    state = State.Idle;
                }       
            break;
        }
    }

    void Explode(Vector3 center, float radius,int damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, targetLayer);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<Farm>().AddDamage(damage);
        }
    }

    Transform GetClosestBuilding(Vector3 center, float radius)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, targetLayer);
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

    IEnumerator ExplodeChicken()
    {
        yield return new WaitForSeconds(0.5f);
        Explode(transform.position, explosionRadius, explosionDamage);
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        exploded = true;
        Destroy(this.gameObject);
    }
}