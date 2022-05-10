using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private void Start()
    {
        GameManager.instance.IsBallOut = false; //When a new ball is instantiated, tell the game manager that a ball is in game
    }

    #region Collisions


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("Trigger enters : " + collision.name);
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        //check if the ball goes beyond limit
        if (collision.CompareTag("BottomLine"))
        {
            int remainingBalls = 0;
            foreach (var ball in balls)
            {
                if (ball.activeInHierarchy)
                    remainingBalls++;
            }
            Debug.Log("Ball out");
            GameManager.instance.TurnsLeft--; //We reduce the total of turns we have left
            if (GameManager.instance.TurnsLeft == 0)
                StartCoroutine(GameManager.instance.GameOver()); //When all the turns have been played, it is game over 
            else
            {
                // check how many balls are in the scene 
                

                Debug.Log("Remaining Balls on the scene : "+remainingBalls);

                if (remainingBalls <= 1) // When the final ball goes beyond the bottom line 
                {
                    StartCoroutine(GameManager.instance.ChangeStatewithTimeGap(GameManager.States.Aiming));
                    GameManager.instance.IsBallOut = true;
                }

            }

            //Destroy(this.gameObject); //Destroy the ball when it goes out of bounds
            StartCoroutine(DeactivateGameObject());
        }
    }

    IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
    }

    #endregion
}
