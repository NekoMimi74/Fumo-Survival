using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("Spawn", spawnTime);
        if(timer <= 0){
            Spawn();
            timer = spawnTime;
        } else{
            timer -= Time.deltaTime;
        }
    }

    //Spawn enemy
    public void Spawn(){
        GameObject g = Instantiate(enemy);
        g.transform.position = transform.position;
    }
}
