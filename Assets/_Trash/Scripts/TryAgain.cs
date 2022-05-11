using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgain : MonoBehaviour
{
    Text text;
    

    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Лох! :)";
        
    }

    void Update()
    {
       
    }
}
