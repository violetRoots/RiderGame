using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().enabled = false;
    }


    public void EnableButton()
    {
        GetComponent<Button>().enabled = true;
    }
}