using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInstructions : MonoBehaviour
{
    public static GInstructions Instance {get; set;}

    public Animator anim;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopTextAnimation()
    {
        anim.SetBool("ExitInstructions", true);
        anim.enabled = false; 

    }
}
