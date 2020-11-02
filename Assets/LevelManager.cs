using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string level;
    public Bounds bound;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount <= 0)
        {
            Debug.Log("level Complete");
            StartCoroutine(LoadScene(level));
        }
        bound = new Bounds(transform.position, Vector3.zero);
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t  = transform.GetChild(i);
            bound.Encapsulate(t.position);
        }
    }

     IEnumerator LoadScene(string scenename)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scenename);
    }
}
