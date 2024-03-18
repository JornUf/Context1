using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public PlayerController _playerController;

    [SerializeField] private CinemachineVirtualCamera menucam;
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private GameObject leaderboardmenu;
    [SerializeField] private GameObject explainmenu;
    [SerializeField] private GameObject timer;
    [SerializeField] private TMP_InputField _inputField;

    [SerializeField] private leaderboard _leaderboard;

    [SerializeField] private float timeToPlay = 600;
    private float timePlayed = 0;
    private bool gameStarted = false;
    private string name;

    public void startGame()
    {
        if (_inputField.text != "")
        {
            _leaderboard.InGameBoard();
            gameStarted = true;
            name = _inputField.text;
            timer.SetActive(true);
            _playerController.backtogame();
            mainmenu.SetActive(false);
            menucam.Priority = 1;
        }
    }

    public void exitGame()
    {
        _leaderboard.stopInGameBoard();
        float time = timer.GetComponent<Timer>().besttime;
        if (time == 0)
        {
            time = 600;
        }
        _leaderboard.AddHighScoreEntry(time, name);
        timer.GetComponent<Timer>().besttime = 0;
        timer.GetComponent<Timer>().QuitedGame();
        backToMenu();
    }

    public void backToMenu()
    {
        gameStarted = false;
        _playerController.returntomenu = false;
        timer.SetActive(false);
        _leaderboard.quitleaderboard();
        leaderboardmenu.SetActive(false);
        explainmenu.SetActive(false);
        mainmenu.SetActive(true);
        _inputField.text = "";
        _playerController.canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        menucam.Priority = 3;
    }

    public void toExplain()
    {
        mainmenu.SetActive(false);
        explainmenu.SetActive(true);
    }

    public void toLeaderboard()
    {
        mainmenu.SetActive(false);
        leaderboardmenu.SetActive(true);
        _leaderboard.load();
    }

    private void Update()
    {
        if (gameStarted)
        {
            timePlayed += Time.deltaTime;
            if (timePlayed >= timeToPlay)
            {
                timePlayed = 0;
                _playerController.returntomenu = true;
            }
        }
    }
}
