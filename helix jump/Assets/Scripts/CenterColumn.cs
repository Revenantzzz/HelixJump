using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CenterColumn : MonoBehaviour
{
    GameObject[] player;
    bool passed = false;
    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        if (gameObject.transform.position.y  > player[0].transform.position.y+.2 )
        {
            if(!passed)
            {
                GameManager.numOfPassedRing++;
                passed = true;
            }
        }
    }
}
