using System.Collections; //To be able to use IENumerators/Coroutines
using UnityEngine;
using UnityEngine.SceneManagement;
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

    int remainingBricks;

    public int totalNumberOfBricks;


    public bool IsBallOut = true;

    public int TurnsLeft; //The number of turns we have

    public TextMeshProUGUI GameOverTxt; // Reference to the game over text game object

    public TextMeshProUGUI AnnouncerTxt; // Reference to the Announcer text game object


    #region Timer Variables
    public bool TimerIsRunning = false;
    public float timeRemaining;
    #endregion

    bool isGameOver = false; // A Boolean to check if game is over 

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
        remainingBricks = totalNumberOfBricks;

        Debug.Log("Number of bricks : " + totalNumberOfBricks);

        UIManager.Instance.UpdateScore(remainingBricks);
        //gameState = States.Aiming; //Change the starting state to Aim State



        StartCoroutine(StartGame());

    }


    IEnumerator StartGame()
    {
        Debug.Log("Can't shoot yet");
        AnnouncerTxt.text = "3...";
        yield return new WaitForSeconds(.5f);
        AnnouncerTxt.text = "2...";
        yield return new WaitForSeconds(.5f);
        AnnouncerTxt.text = "1...";
        yield return new WaitForSeconds(.5f);
        AnnouncerTxt.text = "GO";
        yield return new WaitForSeconds(.2f);
        AnnouncerTxt.gameObject.SetActive(false);


        StartCoroutine(ChangeStatewithTimeGap(States.Aiming));
        Debug.Log("Can shoot now");

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

            else // Once the timer hits zero, change state and turn the timer off
            {
                Debug.Log("Time's up !");
                timeRemaining = 0;
                TimerIsRunning = false;
                StartCoroutine(ChangeStatewithTimeGap(States.Aiming));
            }
        }

        if (!isGameOver && gameState == States.GameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOver());
        }
    }



    public void DestroyBrick()
    {
        remainingBricks--; //reduce the number of bricks by one
        UIManager.Instance.UpdateScore(remainingBricks); // Call the UI Manager and update the score

        if (remainingBricks == 0) // If all the bricks have been destroyed 
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
                SceneManager.LoadScene("GameOver");
                break;

        }

        GameOverTxt.gameObject.SetActive(true); // When the game is over we show a text

        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.15f);
        Time.timeScale = 1;

        //Go to the gameover scene or show a game over panel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // this will load the next scene on the build index
    }

    public IEnumerator ChangeStatewithTimeGap(States stateToChange)
    {

        yield return new WaitForSeconds(0.1f); //Wait for 0.1 seconds
        gameState = stateToChange; //change state to the state defined in the call of the function
    }

    public void ResetBalls()
    {
        if (gameState == States.Shooting && TurnsLeft > 0)
        {
            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var ball in balls)
            {
                ball.SetActive(false);
            }
            StartCoroutine(ChangeStatewithTimeGap(States.Aiming));
            IsBallOut = true;

        }
    }
}
