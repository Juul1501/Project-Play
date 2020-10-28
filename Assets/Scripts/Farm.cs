using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public int health = 500;
    public GameObject smokeParticle;
    public GameObject destroyParticle;

    private int starthealth;

    void Start()
    {
        starthealth = health;
    }
    public void AddDamage(int amount)
    {
        health = health - amount;
        if (health <= starthealth / 2)
        {
            smokeParticle.SetActive(true);
        }
        if(health <= 0)
        {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
            Instantiate(destroyParticle,transform.position,Quaternion.identity);
        }
    }
}
