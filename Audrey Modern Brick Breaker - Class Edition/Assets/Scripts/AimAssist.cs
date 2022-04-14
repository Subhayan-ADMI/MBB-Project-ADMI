using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour
{
    LineRenderer aimLineRendererComponent;

    Vector3 aimLineStartingPoint; //Vector 3 variable to store where the starting point of the line is

    //Called before void Start() or when unity is loading your game
    private void Awake()
    {
        aimLineRendererComponent = GetComponent<LineRenderer>(); //Gain access to the Line Renderer Component using the variable you made

    }

    public void SetStartingPointLine(Vector3 worldPoint)
    {
        //Get the value of the starting point of the line
        aimLineStartingPoint = worldPoint;

        //Set the value of the starting point of the line in the Line Renderer Component's postion index 0
        aimLineRendererComponent.SetPosition(0, aimLineStartingPoint);
    }

    public void SetEndingPointLine(Vector3 worldPoint)
    {
        //Find where the ending line should be in respect to the position of the cannon
        Vector3 pointRespectToCannon = worldPoint - aimLineStartingPoint;

        //Make sure the line is drawn from the cannon and not from the center of the screen
        Vector3 aimLineEndingPoint = transform.position + pointRespectToCannon;

        //Set the value of the ending point of the line in the Line Renderer Component's position index 1
        aimLineRendererComponent.SetPosition(1, aimLineEndingPoint);
    }

    public void SetLineVisible()
    {
        //Enable the Line Renderer Component to make it visible
        aimLineRendererComponent.enabled = true;
    }

    public void SetLineInvisible()
    {
        //Disable the Line Renderer Component to make it invisible
        aimLineRendererComponent.enabled = false;
    }
}
