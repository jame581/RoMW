using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Skript ovlada napovedu zobrazenou ve hre
/// </summary>
public class TutorialController : MonoBehaviour
{

    public GameObject tutorial;
    public Image[] tuts;
    public Button btnNext;

    private int index;

    public void Start()
    {
        tutorial.SetActive(false);
        index = 0;
        for (int i = 0; i < tuts.Length; i++)
        {
            tuts[i].enabled = false;
        }
        btnNext.GetComponentInChildren<Text>().text = "NEXT TIP";
    }

    public bool NextState()
    {
        tutorial.SetActive(true);
        if (index >= tuts.Length)
        {
            HideHint();
            return true;
        }
        else
        {
            tuts[index].enabled = true;            
            if (index == (tuts.Length - 1))
            {
                tuts[index].enabled = true;
                btnNext.GetComponentInChildren<Text>().text = "PLAY";
            }
            index++;
            return false;
        }        
    }

    public void HideHint()
    {
        tutorial.SetActive(false);
    }
}
