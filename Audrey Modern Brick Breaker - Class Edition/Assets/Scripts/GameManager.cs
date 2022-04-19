using System.Collections; //To be able to use IENumerators/Coroutines
using UnityEngine;
using TMPro; //To be able to use text mesh pro

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }


    public enum States
    {
        Preparation,
        Aiming,
        Shooting,
        Victory,
        GameOver
    }

    public States gameState;

    int totalNumberOfBricks;

    public bool IsBallOut = true;

    public int TurnsLeft; //The number of turns we have

    public TextMeshProUGUI GameOverTxt; // Reference to the game over text game object

    #region Timer Variables
    public bool TimerIsRunning = false;
    public float timeRemaining;
    #endregion


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update

    void Start()
    {
        GameOverTxt.gameObject.SetActive(false);
        totalNumberOfBricks = GameObject.FindGameObjectsWithTag("Bricks").Length; // Get the total number of bricks present in the scene

        Debug.Log("Number of bricks : " + totalNumberOfBricks);
        //gameState = States.Aiming; //Change the starting state to Aim State
        
        StartCoroutine(StartGame());


    }

    void Update()
    {
        if (TimerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // reduce the value of the timer at each frame
                Debug.Log("Time before start : " + timeRemaining);
            }

            else // Once the timer hits zero, change state and turn the timer of
            {
                Debug.Log("Time's up !");
                timeRemaining = 0;
                TimerIsRunning = false;
                StartCoroutine(ChangeStatewithTimeGap(States.Aiming));
            }
        }
    }



    IEnumerator StartGame()
    {
        Debug.Log("Can't shoot yet");
        yield return new WaitForSeconds(3f);
        StartCoroutine(ChangeStatewithTimeGap(States.Aiming));
        Debug.Log("Can shoot now");

    }


    public void DestroyBrick()
    {
        totalNumberOfBricks--; //reduce the number of bricks by one
        Debug.Log("Number of bricks on the scene = " + totalNumberOfBricks);

        if (totalNumberOfBricks == 0) // If all the bricks have been destroyed 
        {
            gameState = States.Victory;
            StartCoroutine(ChangeStatewithTimeGap(States.Victory));
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.2f);
       switch (gameState)
        {
            case States.Preparation:
                break;
            case States.Victory:
                Debug.Log("All bricks have been destroyed");
                GameOverTxt.text = "All bricks have been destroyed"; // Changing the text that will be displayed

                break;
            default:
                gameState = States.GameOver;
                GameOverTxt.text = "No more turns! GAME OVER !"; //Changing the text that will be displayed
                Debug.Log("Game over"); //Show a game over message
                break;

        }

        GameOverTxt.gameObject.SetActive(true); // When the game is over we show a text

        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;

        //Go to the gameover scene or show a game over panel
    }

    public IEnumerator ChangeStatewithTimeGap(States stateToChange)
    {
        
        yield return new WaitForSeconds(0.1f); //Wait for 0.1 seconds
        gameState = stateToChange; //change state to the state defined in the call of the function
    }
}
