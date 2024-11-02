using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PreLoader : MonoBehaviour
{
    //Reference of the Canvas Group;
    private CanvasGroup fadedGroup;

    //Load time..,
    private float loadtTime;

    //Minimum time before showing Logo;
    private float minimumLogoTime = 3.0f; 

    // Start is called before the first frame update
    void Start()
    {
        //Grabbing Canvas Component from the Game Object;
        fadedGroup = FindObjectOfType<CanvasGroup>();

        //We'd start with a white screen first;
        fadedGroup.alpha = 1;

        //If Time.time is less than the requirement waiting time;
        //loadTime is equal to it;
        if (Time.time < minimumLogoTime)
        {
            loadtTime = minimumLogoTime;
        }
        else
        {
            loadtTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        fadingEffect();
    }

    public void fadingEffect()
    {
        //Fade in Effect;
        if (Time.time < minimumLogoTime)
        {
            fadedGroup.alpha = Time.time - minimumLogoTime;
        }

        if (Time.time > minimumLogoTime && loadtTime != 0)
        {
            fadedGroup.alpha = Time.time - minimumLogoTime;
            if (fadedGroup.alpha >= 1)
            {
                //Loading Main Menu Game Scene;
                SceneManager.LoadScene(1);
            }
        }
    }
}
