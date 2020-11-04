using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public float amplitudeY = 0.9f;
    public float omegaY = 2f;
    public float offset = 0.2f;
    
    float index;
    public void Update()
    {
        index += Time.deltaTime;
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
        transform.localPosition = new Vector3(transform.position.x, y+offset, transform.position.z);
    }
}
