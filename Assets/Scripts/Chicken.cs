using System.Collections;
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

    public Vector3 target;

    void Start()
    {
        state = State.Idle;
    }

    void Update()
    {
        switch (state) {
            case State.Idle:
                break;
            case State.Walking:
                if(agent.destination == null)
                {
                    state = State.Idle;
                }
                if(Vector3.Distance(transform.position,agent.destination) < destinationOffset)
                {
                    state = State.Exploding;
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

    IEnumerator ExplodeChicken()
    {
        yield return new WaitForSeconds(0.5f);
        Explode(transform.position, explosionRadius, explosionDamage);
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        exploded = true;
        Destroy(this.gameObject);
    }

    public void SetDestination (Vector3 target)
    {
        agent.destination = target;
        state = State.Walking;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == targetLayer)
        {
            state = State.Exploding;
            agent.destination = transform.position;
        }
    }
}