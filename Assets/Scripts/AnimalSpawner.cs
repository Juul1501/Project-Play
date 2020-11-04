using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Mode {Spawning, Moving};
public class AnimalSpawner : MonoBehaviour
{
    public LineRenderer line;
    public int amount = 20;
    public GameObject chicken;

    public TextMeshProUGUI text;
    public Mode mode;
    GameObject curChicken;
    public LevelManager levelManager;

    public Transform[] placeRegions;
    private void Start()
    {
        text.text = amount.ToString();
        NextStep();

    }
    void Update()
    {
        DrawLines();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                switch (mode)
                {
                    case Mode.Spawning:
                        if (hit.transform != null &&!CheckIfInsideFarm(hit.point))
                        {
                            if (amount > 0)
                            {
                                Debug.Log("Hit " + hit.transform.gameObject.name);
                                curChicken = Instantiate(chicken, hit.point, Quaternion.identity);
                                amount -= 1;
                                text.text = amount.ToString();
                                mode = Mode.Moving;
                                NextStep();
                            }
                        }
                        break;
                    case Mode.Moving:
                        curChicken.GetComponent<Chicken>().SetDestination(hit.point);
                        mode = Mode.Spawning;
                        NextStep();
                        break;
                }
            }
        }
    }
    void NextStep()
    {
        if (Tutorial.instance != null)
        {
            Tutorial.instance.NextStep();
        }
    }
    void DrawLines()
    {
        line.positionCount = 5;

        for (int i = 0; i < 5; i++)
        { 
            if(i <4)
            line.SetPosition(i, placeRegions[i].position);

            if(i+1 == 5)
            {
                line.SetPosition(i, placeRegions[0].position);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(placeRegions[0].position, placeRegions[1].position);
        Gizmos.DrawLine(placeRegions[1].position, placeRegions[2].position);
        Gizmos.DrawLine(placeRegions[2].position, placeRegions[3].position);
        Gizmos.DrawLine(placeRegions[0].position, placeRegions[3].position);
    }
    public bool CheckIfInsideFarm(Vector3 point)
    {
        if (point.x < placeRegions[2].position.x && point.x > placeRegions[0].position.x && point.z < placeRegions[2].position.z && point.z > placeRegions[0].position.z)
        {
            return true;
        }
        else
            return false;
    }
}
