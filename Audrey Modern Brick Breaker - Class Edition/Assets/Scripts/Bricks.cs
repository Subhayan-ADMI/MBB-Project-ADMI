using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bricks : MonoBehaviour
{
    [SerializeField] int hits = 1;

    TextMeshProUGUI hitsTxt;

    private void Start()
    {
        hitsTxt = GetComponentInChildren<TextMeshProUGUI>();
        hitsTxt.text = hits.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            //What we want to do when we collide with the ball

            hits--; // we reduce the number of hits (lives)
            hitsTxt.text = hits.ToString(); // Update the UI with the number of hits remaining 

            //Destroy the brick the ball collided with
            

            if (hits == 0)
            {

                GameManager.instance.DestroyBrick(); //Call the GameManager and reduce the number of bricks
                Destroy(gameObject); // When there are no more hits left, destroy the brick
            }
        }
    }
}
