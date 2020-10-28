using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float time = 1;
    void Start()
    {
        Destroy(this.gameObject, time);
    }
}
