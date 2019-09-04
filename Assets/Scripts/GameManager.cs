using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CamMotion camMotion;
    public PlayerController playerController;
    public MapManager map;
    public GameMode mode;

    public AudioSource music;

    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Text gameOverLabel;

    public static int EXIT = -1;
    public static int READY = 0;
    public static int PLAYING = 1;
    public static int PAUSE = 2;
    public static int GAME_OVER = 3;

    [NonSerialized]
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        mode.GameOver += OnGameOver;
        state = READY;
        camMotion.enabled = false;
        playerController.enabled = false;
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        music.enabled = MusicSwitcher.bgMusicLevel > 0;
        music.Pause();
    }


    public void Launch()
    {
        state = PLAYING;
        camMotion.enabled = true;
        playerController.enabled = true;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        music.Play();
    }

    public void Pause()
    {
        state = PAUSE;
        camMotion.enabled = false;
        playerController.enabled = false;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        music.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == READY)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
                Launch();
#endif
            if (Input.touchCount > 0)
                Launch();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == PLAYING)
                Pause();
            else
                Exit();
        }
    }

    public void OnGameOver(object sender, GameOverEventArgs e)
    {
        state = GAME_OVER;
        if (e.result == GameOverEventArgs.WIN)
        {
            gameOverLabel.text = "YOU WIN! ;D";
            camMotion.enabled = false;
            mode.enabled = false;
        }
        else if (e.result == GameOverEventArgs.LOOSE)
        {
            gameOverLabel.text = "YOU LOOSE! : (";
            camMotion.enabled = false;
            mode.enabled = false;
            playerController.enabled = false;
        }
        gameOverScreen.SetActive(true);
    }

    public void Exit()
    {
        state = EXIT;
        SceneManager.LoadScene("MainScreen");
    }
}


