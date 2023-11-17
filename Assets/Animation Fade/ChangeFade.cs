using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class ChangeFade : MonoBehaviour
{
    public Animator animationfade;
    private int timeLoading;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
          ChangeLevel(1);
        }
    }
    public void ChangeLevel(int level) 
    {
        animationfade.SetTrigger("FadeOut");
        timeLoading = level;
    }
   /* public void OnFadeComplete() 
    {
        SceneManager.LoadScene(timeLoading);
    }*/
}
