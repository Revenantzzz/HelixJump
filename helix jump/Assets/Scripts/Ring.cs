using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    [SerializeField]
    GameObject centerCl;

    GameObject ringSFX;
    AudioSource audioManager;

    float radius = 200f;
    float force = 500f;
    float destroyTime = .2f;

    float destroyTiming = 0f;
    int index = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        ringSFX = GameObject.FindGameObjectWithTag("SFX");
        Debug.Log(ringSFX.name);
        audioManager = ringSFX.GetComponent<AudioSource>();
        index = 0;
    }
    private void FixedUpdate()
    {
        if ((gameObject.transform.position.y > player.transform.position.y +.2f) && index == 0)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(force,centerCl.transform.position - new Vector3(0,0.3f,0),radius);
            audioManager.Play();
         
            index++;
        }
        if(index != 0)
        {
            destroyTiming += Time.deltaTime;
            if (destroyTiming > destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
