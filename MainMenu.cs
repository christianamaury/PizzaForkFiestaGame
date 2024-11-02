using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To access Scene Manager methods;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance {get; set;}

    //Buttons & UI Game Objects;
    public GameObject pizzaLogoObject;
    public GameObject forkVectorObject;
    public GameObject infoMenuGameObject;
    public GameObject infoMenuButtonGameObject;
   
    public GameObject playGameButtonGameObject;
    public GameObject quitGameButtonGameObject;
    public GameObject noAdsButtonGameObject;
    public GameObject leaderboardButtonGameObject;
    public GameObject landingPageGameObject;
    public GameObject restorePurchasesGameObject;
    public GameObject noPurchasesMessagesGameObject;

    public void Awake()
    {
        Instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        //By Default these GameObjects are set to False;
        infoMenuGameObject.SetActive(false);
        //infoButtonGameObject.SetActive(false);
        //restorePurchasesGameObject.SetActive(false);
        noPurchasesMessagesGameObject.SetActive(false);


    }
     
    public void displayInfoMenu()
    {
        //Playing UI Click Sound;
        FindObjectOfType<AudioManager>().Play("UIClick");

        //Display info Game Object Panel for the user;
        infoMenuGameObject.SetActive(true);

        //Turning off these buttons..
        forkVectorObject.SetActive(false);
        pizzaLogoObject.SetActive(false);
        playGameButtonGameObject.SetActive(false);
        quitGameButtonGameObject.SetActive(false);
        landingPageGameObject.SetActive(false);
        leaderboardButtonGameObject.SetActive(false);
        noAdsButtonGameObject.SetActive(false);
        infoMenuButtonGameObject.SetActive(false);
 

    }

    public void startGame()
    {
        //Playing UI Sound below for the click on the Buttons;
        FindObjectOfType<AudioManager>().Play("UIClick");

        //Loading Main Level Scene: Time.timeScale in order to start playing the game;
        SceneManager.LoadScene(2);
        Time.timeScale = 1;

    }

    public void backToMainMenu()
    {
        //Putting UI sound here;
        FindObjectOfType<AudioManager>().Play("UIClick");

        //.Turning off the info panel
        infoMenuGameObject.SetActive(false);
        pizzaLogoObject.SetActive(true);
        forkVectorObject.SetActive(true);
        infoMenuButtonGameObject.SetActive(true);
        playGameButtonGameObject.SetActive(true);
        quitGameButtonGameObject.SetActive(true);
        leaderboardButtonGameObject.SetActive(true);
        noAdsButtonGameObject.SetActive(true);
        landingPageGameObject.SetActive(true);
  
    }

    public void ourSocialMedia()
    {
        //Playing UI sound;
        FindObjectOfType<AudioManager>().Play("UIClick");

        Application.OpenURL("https://www.tiktok.com/@sweetestent");
    }

    public void landingPage()
    {
        //Link for our Landing Page to collect user Email Address information;
        //Users need to fully sign up through our MailChimp app.

        //Link goes below here:

    }

    public void checkOurGames()
    {
        //Play UI Sound here;
        FindObjectOfType<AudioManager>().Play("UIClick");
      
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            //Take the user to our Mobile Catalogue;
            Application.OpenURL("https://apps.apple.com/us/developer/christian-a-castro/id1427156495");
        }
    }

    public void quitGame()
    {
        //Playing UI Sound below for the click on the Buttons;
        FindObjectOfType<AudioManager>().Play("UIClick");

        Application.Quit();
    }
}
