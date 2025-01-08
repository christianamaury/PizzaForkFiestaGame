using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Require GameUI which is GameM;
[RequireComponent(typeof(GameM))]
public class GameController : MonoBehaviour
{
    public static GameController Instance {get; set;}

    //Initially set to 0;
    //[SerializeField]
    private int forkCount = 8;

    [Header("Fork Spawning")]
    [SerializeField]
    private Vector2 forkSpawnPosition;

    [SerializeField]
    public GameObject forkObject;

    public GameM GameUI {get; set;}

    //In order to save Spawn Objects;
    private List<GameObject> spawnedForks = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        GameUI = GetComponent<GameM>();
    }


    // Start is called before the first frame update
    void Start()
    {
        GameUI.SetInitialDisplayedForkCount(forkCount);

        //..Respawning the 1st Fork;
        ForkSpawn();
    }

    public void OnSuccessfulForkHit()
    {
        if (forkCount > 0)
        {
            ForkSpawn();
        }

        else
        {
            StartGameOverSequence(true);
        }
    }

    public void ForkSpawn()
    {
        forkCount--;

        if (forkObject != null)
        {
            Debug.Log("ForObject is not null. Testing Instantiate");

            //TESTING; Assigning Instantiate GameObject;
            GameObject spawnedFork = Instantiate(forkObject, forkSpawnPosition, Quaternion.identity);

            //TESTING; Track the Spawn Objects on the List;
            spawnedForks.Add(spawnedFork);

            //Decrement just after successful spawn of the GameObject; TESTING;
            //forkCount--;

        }

        else
        {
            Debug.Log("Fork Object is not assigned or either assigned properly");
        }
    }

    public void StartGameOverSequence (bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    public IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if(win)
        { 
            foreach (GameObject forks in spawnedForks)
            {
                Rigidbody2D rb = forks.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 1;

                //Removing the Constraints; TESTING*
                rb.constraints = RigidbodyConstraints2D.None;
            }
 
            //Wait one second before destroying the Forks;
            yield return new WaitForSeconds(1f);
            foreach (GameObject forks in spawnedForks)
            {
                if (forks !=null)
                {
                    //Destroying the Forks;
                    Destroy(forks);
                }

            }
             
            //Clearing the List after all Destroy Forks;
            spawnedForks.Clear();

            //Resetting forkCount back to 7;
            forkCount = 8;
          
            foreach (Transform child in GameM.Instance.forkPanel.transform)
            {
                Destroy(child.gameObject);
            }
 
            GameM.Instance.forkIconIndexToChange = 0;
 
            //Assigning the Value Again; 
            GameM.Instance.SetInitialDisplayedForkCount(forkCount);
 
            //Respawning Object Again;
            if (forkCount > 0)
            {
                //Respawning Fork Objects Again;
                ForkSpawn();
            }
        
        }
        else
        {
            //..Show the Restart Button Menu; here
            GameM.Instance.showingRestartPanel();
        }
    }
 
}
