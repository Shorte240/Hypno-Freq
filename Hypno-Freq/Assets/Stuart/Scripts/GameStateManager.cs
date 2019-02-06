using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

enum GameState
{
    play,
    pause,
    win,
    lose,
    exit
}

public class GameStateManager : MonoBehaviour {

    public float timer = 30;
    public GameObject TimerObj;
    public Text TimeText;
    public Slider TimeBar;
    public Button menuButton;
    public Button pauseButton;
    public Sprite pauseImage;
    public Sprite resumeImage;
    GameState gameState;
    public NavMeshAgent[] agents;
    int agentsSize = 3;
    //public bool[] isGroupEntityHypnotised;
    public GameObject humanManager;
    public int aliveCount = 0;


    // Use this for initialization
    void Start () {
        timer = 30;
        TimeText.text = ((int)timer).ToString() + "s"; ;
        gameState = GameState.play;
        menuButton.onClick.AddListener(Menu);
        pauseButton.onClick.AddListener(Pause);
        humanManager = GetComponent<GameObject>();
        //pauseImage = GetComponent<Sprite>();
        //resumeImage = GetComponent<Sprite>();
        //pauseButton = GetComponent<Button>();
        //menuButton = GetComponent<Button>();
    }
	void pause()
    {
        for(int i = 0; i < agentsSize; i++)
        {
            agents[i].isStopped = true; 
        }
    }

    void unPause()
    {
        for (int i = 0; i < agentsSize; i++)
        {
            agents[i].isStopped = false;
        }
    }
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case GameState.play:
                //for (int i = 0; i < agentsSize; i++)
                //{
                //    //if(!isGroupEntityHypnotised[i])
                //    //{
                //    //    break;
                //    //}
                //    //Debug.Log("WON");

                //}

                //bool allHypnotized = true;
                //foreach (MoveBetweenPoints child in humanManager.transform)
                //{
                //    Debug.Log(child.name);
                //    if (child.hypno == false)
                //    {
                //        allHypnotized = false;
                //    }
                //}

                //if (allHypnotized)
                //{
                //    gameState = GameState.win;
                //    Debug.Log("WIN");
                //}

                //gameState = GameState.win;
                // make timer visible

                if (aliveCount == agentsSize)
                {
                    gameState = GameState.win;
                }

                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    // time's up!
                    timer = 30;

                    gameState = GameState.lose;
                }

                // update UI
                TimeText.text = ((int)timer).ToString() + "s";
                TimeBar.value = timer;
                break;
            case GameState.pause:
                // TODO: ???
                break;
            case GameState.win:
                // TODO: show win screen/animation
                Debug.Log("WIN");
                SceneManager.LoadScene("winScene");
                break;
            case GameState.lose:
                // TODO: show lose screen/animation
                SceneManager.LoadScene("lossScene");
                break;
            case GameState.exit:
                // TODO: exit game
                break;
            default:
                break;
        }
    }

    void Pause ()
    {
        
        switch (gameState)
        {
            case GameState.play:
                pause();
                gameState = GameState.pause;
                pauseButton.image.sprite = resumeImage;
                break;
            case GameState.pause:
                unPause();
                gameState = GameState.play;
                pauseButton.image.sprite = pauseImage;
                break;
            default:
                break;
        }
    }

    void Menu ()
    {
        SceneManager.LoadScene("Menu");
    }

}
