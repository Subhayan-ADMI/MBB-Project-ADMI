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

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        //check the tag of the element it collided with
        if (collision.collider.CompareTag("Bricks"))
        {
            //What we want to do when we collide with a brick
            //Debug.Log("Collided with a brick :" + collision.collider.name);

            //Destroy the brick the ball collided with
            Destroy(collision.collider.gameObject);


            GameManager.instance.DestroyBrick(); //Call the GameManager and reduce the number of bricks
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enters : " + collision.name);

        //check if the ball goes beyond limit
        if (collision.CompareTag("BottomLine"))
        {
            Debug.Log("Ball out");
            GameManager.instance.TurnsLeft--; //We reduce the total of turns we have left
            if (GameManager.instance.TurnsLeft == 0)
                GameManager.instance.GameOver(); //When all the turns have been played, it is game over 
            else
            {


                GameManager.instance.IsBallOut = true; //Tell the game manger that the ball went below the bottom line

                GameManager.instance.gameState = GameManager.States.Aiming;

            }

            Destroy(this.gameObject); //Destroy the ball when it goes out of bounds
        }
    }

    #endregion
}
