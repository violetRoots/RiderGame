using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    PlayerController PC;
    bool isDead;
   

    private void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        

    }

    private void Update()
    {
        isDead = PC.isDead;
        // клик в любом месте
       // if (Input.GetMouseButton(0) && isDead) LoadSkier();
    }


    public void LoadScene0()
    {
        SceneManager.LoadScene(0);
    }

    
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }


    public void CloseApplication()
    {
        Application.Quit();
    }




}
