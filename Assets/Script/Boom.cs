using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boom : MonoBehaviour
{
    public float shakeThreshHold;
    public GameObject kaBoom;
    Image fumo;
    // Start is called before the first frame update
    void Start()
    {
        fumo = GameObject.Find("Image").GetComponent<Image>();
        fumo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        accel();
    }

    void accel(){
        if(Input.acceleration.magnitude > shakeThreshHold){
            //print("BOOM!!");
            fumo.enabled = true;
            Time.timeScale = 0;
            GameObject g = Instantiate(kaBoom);
            Destroy(gameObject);
        }
    }
}
