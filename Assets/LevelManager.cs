using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string level;
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
    }

     IEnumerator LoadScene(string scenename)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(scenename);
    }
}
