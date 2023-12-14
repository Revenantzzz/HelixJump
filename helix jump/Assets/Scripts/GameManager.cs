using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    GameObject levelCompletePanel;
    [SerializeField]
    Canvas crossfade;

    Animator animator;

    static public bool gameOver = false;
    static public bool levelComplete = false;

    static public int currentLevelIndex = 1;
    static public int nextLevelIndex;
    static public int numOfPassedRing = 0;
    static public int Score = 0;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject pauseButton;
    private string nextLevelText;

    [SerializeField]
    AudioSource audioGameOver;
    [SerializeField]
    AudioSource audioWin;

    int index;

    private void Awake()
    {
        Score = 0;
    }
    void Start()
    {
        pauseButton.SetActive(true);
        currentLevelText.text = "Level "+ currentLevelIndex.ToString();

        Time.timeScale = 1f;
        gameOver = false;
        levelComplete = false;

        currentLevelIndex = PlayerPrefs.GetInt("UnlockedLevel", 0);
        nextLevelIndex = currentLevelIndex + 1;
        nextLevelText = "Level "+ nextLevelIndex.ToString();

        bestScoreText.text = "Best: " + PlayerPrefs.GetInt("bestScore", 0);
        if(PlayerPrefs.GetInt("UnlockedLevel",1) < currentLevelIndex)

        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevelIndex);
        } 

        animator = crossfade.GetComponent<Animator>();
        SceneTransition.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        animator.SetBool("Transition", false);

        audioGameOver.ignoreListenerPause = true;
        audioWin.ignoreListenerPause = true;

        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Score = numOfPassedRing * 10;
        scoreText.text = Score.ToString();
        if(Score > PlayerPrefs.GetInt("bestScore",0)) 
        {
            PlayerPrefs.SetInt("bestScore", Score);
            bestScoreText.text = "Best: " + Score.ToString();
        }
        if(gameOver && index == 0)
        {
            audioGameOver.Play();
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            index++;
        }
        if(levelComplete && index == 0)
        {
            audioWin.Play();
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            levelCompletePanel.SetActive(true);
            index++;
        }
    }
    
    public void OnTap(InputAction.CallbackContext context)
    {
        if(gameOver)
        {
            SceneManager.LoadScene(0);
        }
        if(levelComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            currentLevelIndex = nextLevelIndex;
            currentLevelText.text = nextLevelText;
        }
    }

    public void OnStartGame()
    {
        SceneTransition.sceneIndex = 1;
        animator.SetBool("Transition", true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
