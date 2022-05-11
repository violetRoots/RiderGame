using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float playerDistance;
    public int StarsCount;
    public int CubeCount;
    Text DistanceCount;
    Text starCountText;
    Text cubeCountText;
    GameObject Player;
    public GameObject buttons;
    bool winSound;

    public void Start()
    {
        Player = GameObject.Find("Player");
        DistanceCount = GameObject.Find("DistanceCount").GetComponent<Text>();
        starCountText = GameObject.Find("StarsCount").GetComponent<Text>();
        cubeCountText = GameObject.Find("CubeCount").GetComponent<Text>();
        StarsCount = 0;
        CubeCount = 0;
        buttons.SetActive(false);
    }

    public void Update()
    {
        playerDistance = Vector3.Distance( new Vector3(0, 0, 0), Player.transform.position);
        DistanceCount.text = Mathf.RoundToInt(playerDistance) + " m";
        starCountText.text = StarsCount + "/10";
        cubeCountText.text = CubeCount + "/3";
        if (StarsCount >= 10 && CubeCount >=3) Win();

    }

    public float DisCount(float d)
    {
        d = Vector3.Distance(new Vector3(0, 0, 0), Player.transform.position);
        return d;
    }
    public void Win()
    {
       if (!winSound) GetComponent<SFX>().PlaySound(0);
       winSound = true;

    }
    public void Replay()
    {
        buttons.SetActive(true);
    }
    public void DeadText()
    {
        GameObject.Find("TryAgain").GetComponent<Text>().enabled = true;

        Text TryAgain = GameObject.Find("TryAgain").GetComponent<Text>();
         int deadswitch = Random.Range(1, 2 + 1);
   
        switch (deadswitch)
        {
            case 1:
                TryAgain.text = "Лох :)";
                break;
            case 2:
                TryAgain.text = "Ха-Ха! :)";
                break;
            
        }
    }
}
