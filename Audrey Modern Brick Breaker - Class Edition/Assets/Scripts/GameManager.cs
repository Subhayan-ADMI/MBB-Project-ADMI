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
        gameState = States.Aiming; //Change the starting state to Aim State
    }


    public void DestroyBrick()
    {
        totalNumberOfBricks--; //reduce the number of bricks by one
        Debug.Log("Number of bricks on the scene = " + totalNumberOfBricks);

        if (totalNumberOfBricks == 0) // If all the bricks have been destroyed 
        {
            gameState = States.Victory;
            //StartCoroutine(ChangeStatewithTimeGap(States.Victory));
            GameOver();
        }
    }

    public void GameOver()
    {
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

        //Go to the gameover scene or show a game over panel

    }

    IEnumerator ChangeStatewithTimeGap(States stateToChange)
    {
        yield return new WaitForSeconds(0.1f); //Wait for 0.1 seconds
        gameState = stateToChange; //change state to the state defined in the call of the function
    }
}
