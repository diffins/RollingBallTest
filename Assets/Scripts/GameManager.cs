using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private EntitySpawner _spawner;
    private Ball _ball;
    private TextMeshProUGUI _distanceUI;
    private TextMeshProUGUI _scoresUI;
    private GameObject _pausePanel;
    private GameObject _losePanel;
    private TextMeshProUGUI _currentResults;
    private TextMeshProUGUI _bestResults;

    private float _speed = 8f;
    private float _distance;
    private int _scores;
    private int _scoresFromJumps;
    private bool _isPuase = false;

    public float Speed => _speed;

    private float _prevTime;

    private void Awake()
    {
        CreateSingleton();

        _spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EntitySpawner>();
        _ball = GameObject.FindGameObjectWithTag("Player").GetComponent<Ball>();
        _distanceUI = GameObject.FindGameObjectWithTag("Distance").GetComponent<TextMeshProUGUI>();
        _scoresUI = GameObject.FindGameObjectWithTag("Scores").GetComponent<TextMeshProUGUI>();
        _pausePanel = GameObject.FindGameObjectWithTag("Pause");
        _losePanel = GameObject.FindGameObjectWithTag("Lose");
        _currentResults = GameObject.FindGameObjectWithTag("Current").GetComponent<TextMeshProUGUI>();
        _bestResults = GameObject.FindGameObjectWithTag("Best").GetComponent<TextMeshProUGUI>();
        _pausePanel.SetActive(false);
        _losePanel.SetActive(false);
    }

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {   
        CalculateDistance();
        UpdateSpeed();

        _scores = (int)Math.Floor(_distance) + _scoresFromJumps;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _distanceUI.text = "Distance: " + Math.Round(_distance, 2);
        _scoresUI.text = "Scores: " + _scores;
    }

    private void UpdateSpeed()
    {
        if(_speed * _speed < _distance)
        {
            _speed++;
            _spawner.SetSpeedToEnitys(_speed);
            _ball.SetSpeed(_speed / 1.5f);
        }
    }

    private void CalculateDistance()
    {
        _distance += (Time.time - _prevTime) * (_speed / 3);
        _prevTime = Time.time;
      
    }


    public void GameOver()
    {
        Time.timeScale = 0;

        _losePanel.SetActive(true);
        _currentResults.text = "Distance: " + Math.Round(_distance, 2) + "   Scores: " + _scores;

        if(PlayerPrefs.HasKey("bestDistance"))
            _bestResults.text = "Distance: " + PlayerPrefs.GetFloat("bestDistance") + "   Scores: " + PlayerPrefs.GetInt("bestScores");
        else
        {
            _bestResults.text = "Distance: " + 0 + "   Scores: " + 0;
        }

        PlayerPrefs.SetFloat("bestDistance", (float)Math.Round(_distance, 2));
        PlayerPrefs.SetInt("bestScores", _scores);
        PlayerPrefs.Save();

    }
    
   

    public void Pause()
    {
        _isPuase = !_isPuase;

        if(_isPuase)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }
    }

    public void AddScore(int value)
    {
        _scoresFromJumps += value;
    }
}
