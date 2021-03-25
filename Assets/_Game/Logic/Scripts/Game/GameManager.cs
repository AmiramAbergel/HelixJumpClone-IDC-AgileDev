using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public GameConfig gameConfig;
    public Transform ball;
    public Transform startPos;

    [Header("UI Reference")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Levels")]
    public Transform helix;
    public List<Level> levels = new List<Level>();

    [Space]
    public UnityEvent LevelFinishedEvent;

    public static Action<Level> OnLevelStart = delegate { };

    private int _score;
    private int _highScore;
    private int _level = 0;
    private Level _currentLevel;

    private void Awake()
    {
        #region Singleton
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Debug.LogWarning("More then one GameManger was created");
            Destroy(gameObject);
        }
        #endregion

        Platform.OnPlatformPassed += AddScore;
        BallControl.OnKillPlayer += LevelFail;
        BallControl.OnGoalReached += LevelFinifhed;

        UpdateScoreText();
    }

    private void Start()
    {
        RestartGame();
    }

    private void AddScore(Transform transform)
    {
        _score++;

        if (_score > _highScore)
            _highScore = _score;

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = _score.ToString();
        highScoreText.text = _highScore.ToString();
    }

    private void LevelFail()
    {
        _score = 0;
        UpdateScoreText();

        Invoke(nameof(MovePlayerWhenKilled), 0.02f);
        Invoke(nameof(RestartGame), 1f);
    }
    private void LevelFinifhed()
    {
        LevelFinishedEvent.Invoke();
        _level++;

        if (_level >= levels.Count)
            _level = 0;

        Invoke(nameof(StartLevel), 1f);
    }

    private void MovePlayerWhenKilled()
    {
        ball.position = Vector3.up * 200f;
    }

    private void StartLevel()
    {
        if (_currentLevel)
            Destroy(_currentLevel.gameObject);

        _currentLevel = Instantiate(levels[_level], helix).GetComponent<Level>();
        ball.position = startPos.position;
        OnLevelStart.Invoke(_currentLevel);
    }

    private void RestartGame()
    {
        if(_currentLevel)
            Destroy(_currentLevel.gameObject);

        _currentLevel = Instantiate(levels[0], helix).GetComponent<Level>();

        ball.position = startPos.position;
    }
}
