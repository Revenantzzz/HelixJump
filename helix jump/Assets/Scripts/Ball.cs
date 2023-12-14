using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private GameObject splitPref;
    [SerializeField]
    private float bounceForce = 10f;
    
    static public bool hitDanger = false;
 
    static public bool hitFinish = false;

    AudioSource audioManager;

    private void Start()
    {
        audioManager = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        hitDanger = false;
        hitFinish = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play();

        string materialName = collision.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Danger (Instance)")
        {
            GameManager.gameOver = true;
        }
        else
        {
            if (materialName == "LastRing (Instance)")
            {
                GameManager.levelComplete = true;
            }
            else
            {
                    rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
            }
        }
        if (!collision.gameObject.CompareTag("Split"))
        {
            Vector3 splitPos = new Vector3(transform.position.x, collision.transform.position.y + 0.19f, transform.position.z);

            GameObject newSplit = Instantiate(splitPref, splitPos, Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)));
            newSplit.transform.parent = collision.transform;
        }

    }
}

