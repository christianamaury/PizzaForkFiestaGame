using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement; 


public class GameM : MonoBehaviour
{
    public static GameM Instance {get; set;}

    //Class GameUI is basically GameM
    //[SerializeField]
    //..Restart Button;

    //Restart Level GameObject;
    public GameObject restartPanel;

    [Header("Fork Count Display")]
    [SerializeField]
    public GameObject forkPanel;

    [SerializeField]
    private GameObject iconFork;

    [SerializeField]
    private Color usedForkIconColor;

    //Tracking the last Fork Throw Icon;
    public int forkIconIndexToChange = 0;

    //Fork Score Value for the Player; 
    public int score = 0;

    public TextMeshProUGUI gameTextInstructions;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
 
    private List<GameObject> spawnedIconForks = new List<GameObject>();


    //..Show Restart Button Method; Aquí

    //Variable for Ads purposes;
    private int adsNumber;
    private int adsRandomNumber;

    //Game Instructions: Boolean Check
    private bool gameInstructionsBool = false; 


    private void Awake()
    {
        Instance = this;

        //Disabling Game Instructions for now;
        gameTextInstructions.enabled = true;
    }

    private void Start()
    {
        adsNumber = 2;
        adsRandomNumber = Random.Range(1, 3);

        //Assigning the score variable; 
        scoreText.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        //Disabling Game Instructions for now;
        gameTextInstructions.enabled = true;

        //Disabling animator Component;
        //GInstructions.Instance.anim.enabled = false;

        gameInstructionsBool = true;

        //Turning off Panel by Default;
        restartPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameInstructionsBool)
        {
            StartCoroutine(DisableGameInstructions(1.1f));
            gameInstructionsBool = false;

        }

        else { return;  }
    }

    private IEnumerator DisableGameInstructions(float seconds)
    {
        //If.., true;
        if (gameInstructionsBool)
        {
            GInstructions.Instance.anim.enabled = true;
           
            yield return new WaitForSeconds(seconds);
     
            //Enabling.. TextMesh Text Layer;
            gameTextInstructions.enabled = true;

            yield return new WaitForSeconds(seconds);

            //Disabling TextMesh Text Layer;
            gameTextInstructions.enabled = false;

            //Disabling TextMesh Text Animation..,
            GInstructions.Instance.StopTextAnimation();

            //Settin..g if statement Check variable to false;
            gameInstructionsBool = false;

        }

    }


    //..SetInitialDisplay Fork Count;
    public void SetInitialDisplayedForkCount (int count)
    {
        //Initially set to 1,
        //If set to 0 = 8 icons on the Scene
        for(int i = 1; i <= count; i++)
        {
            GameObject spawnedIconFork = Instantiate(iconFork, forkPanel.transform);

            //Adding it to the List;
            spawnedIconForks.Add(spawnedIconFork);
        }

        if (count == 0)
        {
            foreach (GameObject iconForks in spawnedIconForks)
            {
                if (iconForks != null)
                {

                    Destroy(iconForks);
                    forkIconIndexToChange = 0;
                    count = 0;
                    //spawnedIconForks.Clear();
                    DecrementDisplayForkCountState();

                }
            }

            spawnedIconForks.Clear();
        }
    }

    //..Decrementing fork icons that are displayed, 21:55
    public void DecrementDiplayForkCount()
    {
        //..Getting Child References;
        forkPanel.transform.GetChild(forkIconIndexToChange++).GetComponent<Image>().color = usedForkIconColor;

    }

    public void DecrementDisplayForkCountState()
    {
        foreach (Transform child in forkPanel.transform)
        {
            child.GetComponent<Image>().color = Color.white;
        }

        forkPanel.transform.GetChild(forkIconIndexToChange--).GetComponent<Image>().color = usedForkIconColor;
   
    }

    public void randomInterestialAds()
    {
        if (adsNumber == adsRandomNumber)
        {
            //Showing Ads to the Player;
            AdsManager.Instance.showingInterstitialAds();
        }

        else { return; }
    }

    public void showingRestartPanel()
    {
        //Disabling main score Text from the scene.
        scoreText.gameObject.SetActive(false);

        //Calling Randomizer Ads Function;
        randomInterestialAds();

        //Showing Game Menu;
        restartPanel.SetActive(true);

        //Playing Try Again Text Animation;
        TryAgainTextAnimator.Instance.PlayTryAgainTextAnimation();

        //Pausing Game;
        Time.timeScale = 0;

    }

    public void loadingMainScene()
    {
        //Playing UI sound;
        FindObjectOfType<AudioManager>().Play("UIClick");

        //Loading Main 1st Scene which is Main Menu;
        SceneManager.LoadScene(1);

    }

    public void restartingLevel()
    {
        FindObjectOfType<AudioManager>().Play("UIClick");

        //Disable Game Menu;
        restartPanel.SetActive(false);

        //Un-pausing game;
        Time.timeScale = 1;

        //Re-loading Current Scene;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void savingScore()
    {
        score = score + 1;
        scoreText.text = score.ToString();
       
        if (score > PlayerPrefs.GetInt("BestScore", 0))
        {
            //Saving new Score;
            PlayerPrefs.SetInt("BestScore", score);

            //Saving Bestscore into the Leaderboard, CloudServices below here:
            CloudOnceServices.Instance.SubmitScoreToLeaderboard(score);

            //Assigning new saved score to the Best Score Text;
            bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        }
    }

    public void ourGames()
    {
        FindObjectOfType<AudioManager>().Play("UIClick");

        //Showing our Games Catalogue;
        MainMenu.Instance.checkOurGames();
    }

    public void quitGame()
    {
        //Quitting Game Configuration; 
        MainMenu.Instance.quitGame();
    }



}
