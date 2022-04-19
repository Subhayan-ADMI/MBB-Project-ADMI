using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    public float randomXPosition;
    public float randomYPosition;
    // Start is called before the first frame update
    void Start()
    {
        randomXPosition = Random.Range(-5f, 5f);
        Debug.Log("Random X value = " + randomXPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangePlayerPosition()
    {
        randomXPosition = Random.Range(-2.5f, 2.5f);
        yield return new WaitForSeconds(2f);
        transform.position += new Vector3(randomXPosition, 0, 0);
    }
}
