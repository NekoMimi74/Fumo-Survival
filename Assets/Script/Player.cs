using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Camera cam;
    Transform mySpawnPoint;

    public float speed;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float CamRubber;
    
    AudioSource myAudio;
    public AudioClip fumo;
    Text timerTxt;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        mySpawnPoint = transform.Find("SpawnPoint");
        timerTxt = GameObject.Find("Time").GetComponent<Text>();

        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // // player move with accel
        // Vector3 a = Input.acceleration;
        // //print(a);

        // Physics.gravity = new Vector3(a.x, 0, a.y) * speed;

        // check for touches
        if(Input.touches.Length > 0){
            RaycastHit hitInfo;
            Ray r = cam.ScreenPointToRay(Input.touches[0].position);
            if(Physics.Raycast(r, out hitInfo)){
                Vector3 h = hitInfo.point;

                Vector3 dir = hitInfo.point - transform.position; 
                transform.position += dir.normalized * Time.deltaTime * speed;

                h.y = 1;
                transform.LookAt(h);
            }

            // Bang
            if(Input.touches.Length >= 2 && Input.touches[1].phase == TouchPhase.Began){
                GameObject g = Instantiate(bulletPrefab, mySpawnPoint.position, mySpawnPoint.rotation);
                g.GetComponent<Rigidbody>().velocity = -transform.forward * bulletSpeed;

                myAudio.PlayOneShot(fumo);
            }
        }

        // camera follow player
        Vector3 goPoint = new Vector3(
            transform.position.x, cam.transform.position.y, transform.position.z
        );
        cam.transform.position = Vector3.Lerp(cam.transform.position, goPoint, Time.deltaTime * CamRubber);

        TimeTracker();
    }

    private float time = 1;
    private int sec;
    void TimeTracker(){
        if(time < 0){
            sec++;
            time = 1;
            timerTxt.text = "" + sec + "";
        } else {
            time -= 1 * Time.deltaTime;
        }
    }
}
