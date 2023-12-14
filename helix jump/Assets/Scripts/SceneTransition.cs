using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator animator;
    float transitTimer = 0f;
    float transitionLength = 1f;
    public static int sceneIndex = 1;
    void Start()
    {
        animator = GetComponent<Animator>();
        transitTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool isTransit = animator.GetBool("Transition");
        if (isTransit)
        {
            if(transitTimer > transitionLength)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                transitTimer += Time.deltaTime;
            }
        }
    }
}
