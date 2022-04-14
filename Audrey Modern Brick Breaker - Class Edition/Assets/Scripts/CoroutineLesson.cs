using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineLesson : MonoBehaviour
{
    SpriteRenderer squareSprite;

    // Start is called before the first frame update
    void Start()
    {
        squareSprite = GetComponent<SpriteRenderer>();
        //FadeOriginal();
        //StartCoroutine(FadeCoroutine());
        StartCoroutine(FadeCoroutinewithTimeControl());

        //StartCoroutine(PrintStuff());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FadeOriginal()
    {
        Color squareColor = squareSprite.material.color;

        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            squareColor.a = alpha;
            squareSprite.material.color = squareColor;
        }
    }

    IEnumerator FadeCoroutine()
    {
        //An IENumerator is a data type that makes a function into a coroutine
        //A coroutine is a function that executes across multiple frames....basically...a function that can be paused

        Color squareColor = squareSprite.material.color;

        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            squareColor.a = alpha;
            squareSprite.material.color = squareColor;
            yield return null; //Wait for the next frame to execute the rest
        }

    }

    IEnumerator FadeCoroutinewithTimeControl()
    {
        //An IENumerator is a data type that makes a function into a coroutine
        //A coroutine is a function that executes across multiple frames....basically...a function that can be paused

        Color squareColor = squareSprite.material.color;

        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            squareColor.a = alpha;
            squareSprite.material.color = squareColor;
            yield return new WaitForSeconds(0.5f); //Wait for 1 second in real time
        }

    }

    IEnumerator PrintStuff()
    {
        yield return new WaitForSeconds(5f); //Wait for 1 second in real time

        Debug.Log("Hello I am Printing");
    }
}
