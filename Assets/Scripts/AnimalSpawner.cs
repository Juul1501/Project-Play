using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Mode {Spawning, Moving};
public class AnimalSpawner : MonoBehaviour
{
    public int amount = 20;
    public GameObject chicken;

    public GameObject arrow;
    public TextMeshProUGUI text;
    public Mode mode;
    GameObject curChicken;
    public LevelManager levelManager;
    private void Start()
    {
        text.text = amount.ToString();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                switch (mode)
                {
                    case Mode.Spawning:
                        if (hit.transform != null && !levelManager.bound.Contains(hit.point))
                        {
                            if (amount > 0)
                            {
                                Debug.Log("Hit " + hit.transform.gameObject.name);
                                curChicken = Instantiate(chicken, hit.point, Quaternion.identity);
                                amount -= 1;
                                if (arrow != null)
                                    arrow.SetActive(false);
                                text.text = amount.ToString();
                                mode = Mode.Moving;
                            }
                        }
                        break;
                    case Mode.Moving:
                        curChicken.GetComponent<Chicken>().SetDestination(hit.point);
                        mode = Mode.Spawning;
                        break;
                }
            }
        }
    }
}
