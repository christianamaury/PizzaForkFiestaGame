using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkScript : MonoBehaviour
{
    private ForkScript Instance {get; set;}

    [SerializeField]
    private Vector2 throwForce;
    private float yAxisThreshold = 5.26f;
    public bool isActive = true;
    public bool forkFrozenSafetyCheck = false;

    private Rigidbody2D rb;
    private BoxCollider2D forkCollider;

    private bool startThrowingFork = false;
 
    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        forkCollider = GetComponent<BoxCollider2D>();

        //..TESTING; Set the Collision detection mode to Continuous;
        //rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            
    }
     
    // Update is called once per frame
    private void Update()
    {
        #if UNITY_IOS || UNITY_IPHONE
        if (Input.touchCount > 0 && isActive)
        {
            //Get the first touch on the screen;
            Touch touch = Input.GetTouch(0);

            //Check if the touch phase is began (equivalent to a mouse button down);
            if (touch.phase == TouchPhase.Began)
            {
                startThrowingFork = true;
            }
        }
        #endif

        #if UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64 || UNITY_STANDALONE
        //Checking the mouse click in order to throw the Fork;
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            //rb.AddForce(throwForce, ForceMode2D.Impulse);
            //rb.gravityScale = 1;

            ////Calling the Decrementing method for the UI Fork image;
            //GameController.Instance.GameUI.DecrementDiplayForkCount();

            startThrowingFork = true;
 
        }

        #endif
    }

    private void FixedUpdate()
    {
        if (startThrowingFork)
        {
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;

            //Calling the Decrementing method for the UI Fork image;
            GameController.Instance.GameUI.DecrementDiplayForkCount();

            startThrowingFork = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //..if is not active; We don't want to detect collisions if the Fork object is not active
        if (!isActive)
            return;

        //Setting value back to False;
        isActive = false;

        //TESTING: This.transform.position.y;
        if (collision.collider.tag == "PizzaLog")
        {
            //Adding special sound where the fork hits..
            AudioManager.Instance.Play("ForkHitSound");

            //Once we have noticed the collision, play particles effect;
            GetComponent<ParticleSystem>().Play();

            //Displaying current Score;
            GameM.Instance.savingScore();

            //..Stopping the movement of the Fork;
            rb.velocity = new Vector2(0, 0);
            //TESTING
            rb.gravityScale = 0;

            // ..To be equal to the parent GameObject of the Fork Game Object;
            this.transform.SetParent(collision.collider.transform);
            //Reseting angular velocilty as well;
            //rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;

            //Freezing all contraints:
            rb.constraints = RigidbodyConstraints2D.FreezePosition;

            //TESTING;
            StartCoroutine(SafetyCheck());

            //Setting new offset for x values for the forkCollider
            //We're not messing the y coordinates;
            forkCollider.offset = new Vector2(forkCollider.offset.x, -0.4f);
            forkCollider.size = new Vector2(forkCollider.size.x, 1.2f);

            //..It was a success hit, method;
            GameController.Instance.OnSuccessfulForkHit();
        }

        else if (collision.collider.tag == "ForkObject")
        {
            //We're not touching the Y axis; 
            rb.velocity = new Vector2(rb.velocity.x, -2);
            //TESTING
            rb.gravityScale = 1;

            //..Start the GameOverSequence down here;
            GameController.Instance.StartGameOverSequence(false);
        }

    }

    private IEnumerator SafetyCheck()
    {
        yield return new WaitForSeconds(0.2f);

        if (rb.bodyType == RigidbodyType2D.Kinematic && Mathf.Abs(rb.velocity.magnitude) < 0.01f)
        {
            forkFrozenSafetyCheck = true;
            Debug.Log("Fork did set in place properly");
        }

        else
        {
            //Handling Failure;
            GameController.Instance.OnSuccessfulForkHit();
        }
    }
}
