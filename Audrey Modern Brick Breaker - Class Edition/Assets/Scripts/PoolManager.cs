using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject ballPrefab; // The prefab of the element we want to instantiate 
    public int ballAmount; // The total number of ball we want
    public List<GameObject> bullets; // A list that will store the instances of the ball
    public static PoolManager Instance { get; set; }
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        // Preload bullets.
        bullets = new List<GameObject>(ballAmount);
        for (int i = 0; i < ballAmount; i++) {
            GameObject prefabInstance = Instantiate (ballPrefab);
            // prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive (false);

            bullets.Add(prefabInstance); // Add the instantiated ball to the list
        
        }
    }
    
    public GameObject GetBullet () {
        foreach (GameObject bullet in bullets) {
            if (!bullet.activeInHierarchy) {
                bullet.SetActive (true);        
                return bullet;
            }
        }
        GameObject prefabInstance = Instantiate (ballPrefab);  
        bullets.Add (prefabInstance);
        return prefabInstance;
    }
}
