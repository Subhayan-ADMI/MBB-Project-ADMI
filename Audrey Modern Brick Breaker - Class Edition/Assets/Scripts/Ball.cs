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
        int remainingBalls = balls.Length;
        //check if the ball goes beyond limit
        if (collision.CompareTag("BottomLine"))
        {
            remainingBalls--;
            Debug.Log("Ball out");
            Debug.Log("Number of balls remaining before deactivation " + remainingBalls);
            // check how many balls are in the scene 


            Debug.Log("Remaining Balls on the scene : " + remainingBalls);

            if (remainingBalls == 0) // When the final ball goes beyond the bottom line 
            {
                if (GameManager.instance.TurnsLeft == 0)
                {
                    StartCoroutine(GameManager.instance.ChangeStatewithTimeGap(GameManager.States.GameOver));
                }
                else
                {
                    GameManager.instance.IsBallOut = true;
                    StartCoroutine(GameManager.instance.ChangeStatewithTimeGap(GameManager.States.Aiming));
                }
            }

        }

        //Destroy(this.gameObject); //Destroy the ball when it goes out of bounds
        StartCoroutine(DeactivateGameObject());
    }

    IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(.15f);
        gameObject.SetActive(false);
    }

    #endregion
}
