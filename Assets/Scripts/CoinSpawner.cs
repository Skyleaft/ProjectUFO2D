using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject Coin;

    float randX,randY;
    float randScale;
    Vector2 whereToSpawn;
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-78f, 76);
            randY = Random.Range(-75, 72);
            randScale = Random.Range(0.7f, 4f);
            whereToSpawn = new Vector2(randX, randY);

            GameObject newObject = Instantiate(Coin, whereToSpawn, Quaternion.identity) as GameObject;
            newObject.transform.localScale = new Vector2(randScale, randScale); 
            
        }
    }
}
