using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInput : MonoBehaviour
{

    Vector3 locationOfMouse; //Vector3 type of variable to store Vector3 data

    Vector3 startDragPosition; //To store the vector position of where we first touched the screen
    Vector3 endDragPosition; //To store the vector position of where our finger is on the screen before we lift it

    AimAssist aimAssistScript; // A variable of type Aim Assist


    public float minShootValue; //Minimum value to limit the aimline

    Rigidbody2D rb;

    public float shootForce; //The force at which the ball is going to shoot

    public GameObject ball; //Reference to the ball

    private void Awake()
    {
        aimAssistScript = GetComponent<AimAssist>(); //Gain access to the Aim Assist Script Component using the variable you made

        rb = GetComponent<Rigidbody2D>(); //Get a refernce to the rigidbody attached to the object
    }

    // Start is called as soon as the game starts and it is called ONCE
    void Start()
    {
        Debug.Log("Hello");
    }

    // Update is called every frame while your game is running
    void Update()
    {
        locationOfMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        locationOfMouse.z = 0f; //Set the z coordinate of the mouse position that we got to 0

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(locationOfMouse); //Call the start drag funtion and pass the location of mouse vector as parameter
        }

        if (Input.GetMouseButton(0))
        {
            ContinueDrag(locationOfMouse); //Call the ContinueDrag function and pass the location of mouse vector as parameter
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag(); //Call the EndDrag function
        }
    }

    void StartDrag (Vector3 positionInWorldData)
    {

        //Make the Aim Line Visible
        aimAssistScript.SetLineVisible();

        //We should record the first touch position
        startDragPosition = positionInWorldData;


        //Set the starting position of our aim line to the position of the gameobject to which this script is attached to, which is the cannon
        aimAssistScript.SetStartingPointLine(transform.position);

        GameManager.instance.gameState = GameManager.States.Aiming;

    } 

    void ContinueDrag (Vector3 positionInWorldData)
    {
        //We should keep recording where our finger is at
        endDragPosition = positionInWorldData;

        //We should calculate the displacement vector between our start point and where our finger is now
        Vector3 directionVectorOfDrag = endDragPosition - startDragPosition;

        if (directionVectorOfDrag.y >= -minShootValue) // if the y value of directionVector exceeds minimum shoot value in the downward direction
        {
            directionVectorOfDrag.y = -minShootValue; //Keep it at the minimum value;
        }

        //We should continuously update the end point of our Aim line to the displacement vector from the vector our finger dragging made to the Cannon
        aimAssistScript.SetEndingPointLine(transform.position - directionVectorOfDrag);
    }

    void EndDrag()
    {

        // Make the aim line invsible
        aimAssistScript.SetLineInvisible();

        //We should calculate the displacement vector between our start point and where our finger is now
        Vector3 directionOfDrag = endDragPosition - startDragPosition;

        //Launch the balls
        if (GameManager.instance.IsBallOut == true && GameManager.instance.gameState != GameManager.States.GameOver) //We verify that there's no ball in the scene
        LaunchBall(directionOfDrag);
    }


    void LaunchBall(Vector3 direction)
    {
        GameManager.instance.gameState = GameManager.States.Shooting; //change the state to shooting

        Debug.Log("Turnsleft : " + GameManager.instance.TurnsLeft);

        var launchedBall = Instantiate(ball, transform.position, Quaternion.identity);
        launchedBall.GetComponent<Rigidbody2D>().AddForce(-direction * shootForce, ForceMode2D.Impulse);
    }
    

}
