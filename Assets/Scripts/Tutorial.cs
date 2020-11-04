using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tutorial : MonoBehaviour
{
    public Vector3[] ArrowPositions;
    public string[] instructions;
    public GameObject instructionHolder;
    public TextMeshProUGUI instructionsText;
    public GameObject arrow;
    public int index = 0;

    public static Tutorial instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void NextStep()
    {
        if (ArrowPositions.Length > index)
        {
            arrow.transform.position = ArrowPositions[index];           
        }
        else
        {
            arrow.SetActive(false);
        }
        if (instructions.Length > index)
        {
            instructionsText.text = instructions[index];
        }
        else
        {
            instructionHolder.SetActive(false);
        }
        index++;
    }

}
