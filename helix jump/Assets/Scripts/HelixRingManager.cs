using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class HelixRingManager : MonoBehaviour
{
    public GameObject[] easyRings;
    public GameObject[] mediumEasyRings;
    public GameObject[] mediumRings;
    public GameObject[] mediumHardRings;
    public GameObject[] hardRings;

    public GameObject[] lastRings;
    int[] rotateList = { 0, 45, 90, 135, 180, 225, 270, 315 };
    float yPos = 0;

    private int numberOfRing;
    [SerializeField]
    private float ringDistance = 5f;
    [SerializeField]
    private float rotateSpeed = 5f;

    private int levelPer10 = 0;

    private int numOfE = 0;
    private int numOfME = 0;
    private int numOfM = 0;
    private int numOfMH = 0;
    private int numOfH = 0;

    private void Start()
    {
        levelPer10 = (GameManager.currentLevelIndex / 10);
        numberOfRing = 20 + (levelPer10)*10;
        DifficultySet(GameManager.currentLevelIndex);
        LevelDifficultSpawn(GameManager.currentLevelIndex);
        spawnRings(lastRings, 0);
    }
    private void ResetDifficultNum()
    {
        numOfE = 0;
        numOfME = 0;
        numOfM = 0;
        numOfMH = 0;
        numOfH = 0;
    }

    private void DifficultySet(int currentLevel)
    {
        ResetDifficultNum();
        if(currentLevel == 1)
        {
            numOfE = numberOfRing;
        }
        else
        {
            switch((currentLevel/10)) 
            {
                case 0:
                    {
                        numOfME = 6 + currentLevel;
                        numOfE = numberOfRing = numOfME;
                        break;
                    }
                case 1:
                    {
                        numOfE = 5;
                        numOfM = 1 + currentLevel%10;
                        numOfME = numberOfRing - numOfM - 5;
                        break;
                    }
                case 2: 
                    {
                        numOfE = 5;
                        numOfME = 15;
                        numOfMH = 1 + currentLevel%10;
                        numOfM = numberOfRing - numOfMH - 20;
                        break;
                    }
                case 3:
                    {
                        numOfE = 5;
                        numOfME = 10;
                        numOfMH = 11+ currentLevel%10;
                        numOfH = 1 + currentLevel%10;
                        numOfM = numberOfRing - numOfH - numOfMH - 15;
                        break;
                    }
                case 4:
                    {
                        numOfE = 5;
                        numOfME = 10;
                        numOfM = 10;
                        numOfH = 4 + currentLevel%10;
                        numOfMH = numberOfRing - numOfMH - 25;
                        break;
                    }
            }
        }
    }

    private void LevelDifficultSpawn(int currentLevel)
    {
        for(int i = 0; i <= numOfE-1; i++) 
        {
            spawnRings(easyRings, Random.Range(0, easyRings.Length));
        }
        for(int i =0; i <= numOfME-6; i++)
        {
            spawnRings(mediumEasyRings, Random.Range(0, mediumEasyRings.Length ));
        }
        for (int i = 0; i < numOfM; i++)
        {
            spawnRings(mediumRings, Random.Range(0, mediumRings.Length));
        }
        for (int i = 0; i < numOfMH; i++)
        {
            spawnRings(mediumHardRings, Random.Range(0, mediumHardRings.Length));
        }
        for (int i = 0; i < numOfH; i++)
        {
            spawnRings(hardRings, Random.Range(0, hardRings.Length));
        }
        if(currentLevel > 1)
        {
            for (int i = 0; i < 5; i++)
            {
                spawnRings(mediumEasyRings, Random.Range(0, mediumEasyRings.Length));
            }
        }
        
    }
    private void spawnRings(GameObject[] Rings, int index)
    {
        GameObject newRing = Instantiate(Rings[index], new Vector3(transform.position.x, yPos, transform.position.z),
            Quaternion.Euler(new Vector3(0, rotateList[Random.Range(0, rotateList.Length - 1)], 0)), transform);
        yPos -= ringDistance;
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        float rotateAxis = context.ReadValue<float>();
        transform.Rotate(transform.position.x, -rotateAxis*rotateSpeed*Time.deltaTime, transform.position.z);
    }
}
