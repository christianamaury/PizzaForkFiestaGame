using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryAgainTextAnimator : MonoBehaviour
{

    public static TryAgainTextAnimator Instance {get; set;}

    //Animation variable;
    public Animator anim;

    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        //Getting animation Component;
        anim = GetComponent<Animator>();

        //This would ignore the fact that the game is actually paused;
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    //Play animation for the Text;
    public void PlayTryAgainTextAnimation()
    {
        anim.SetTrigger("TryTextAnimation");
    }
}
