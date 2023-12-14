using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    float desTimer = 0f;
    float desTime = 1f;

    void Update()
    {
        if(desTimer > desTime)
        {
            Destroy(gameObject);
        }
        else
        {
            desTimer += Time.deltaTime;
        }
    }
}
