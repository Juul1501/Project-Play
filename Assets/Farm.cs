using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public int health = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddDamage(int amount)
    {
        health = health - amount;
        if(health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
