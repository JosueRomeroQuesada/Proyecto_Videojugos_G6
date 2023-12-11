using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WelcomeController : MonoBehaviour
{
    
    public void Play()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Back()
    {
        //StateManager.Instance.setName(textbox.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void state()
    {
        
        LevelManager.Instance.FirstScene();
    }
    void Start()
    {
    }
        
    }

