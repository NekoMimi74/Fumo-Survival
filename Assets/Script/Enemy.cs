using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Components
    Rigidbody myBod;
    NavMeshAgent myAgent;
    Transform playerTran;

    public float speed;
    public int hp;
    AudioSource herAudio;
    public AudioClip defeat;

    // Text gameOver;
    Image fumo;
    // Start is called before the first frame update
    void Start()
    {
        myBod = GetComponent<Rigidbody>();
        myAgent = GetComponent<NavMeshAgent>();
        herAudio = GetComponent<AudioSource>();

        playerTran = GameObject.Find("Player").transform;
        fumo = GameObject.Find("Image").GetComponent<Image>();
        fumo.enabled = false;
        // gameOver = GameObject.Find("GameOver").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.destination = playerTran.position;

        //movement
        Vector3 f = myAgent.steeringTarget - transform.position;
        f.y = 0;
        myBod.AddForce(f.normalized * Time.deltaTime * speed);

        //rotation
        Vector3 v = playerTran.position - transform.position;
        v.y = 0;
        transform.forward = v;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet"){
            if(hp <= 0){
                herAudio.PlayOneShot(defeat);
                Destroy(gameObject);
            }
            else {hp--;}
        }
        else if(other.tag == "Player"){
            // gameOver.text = "<b>She's Mind!</b>";
            fumo.enabled = true;
            Time.timeScale = 0;
        }
    }
}
