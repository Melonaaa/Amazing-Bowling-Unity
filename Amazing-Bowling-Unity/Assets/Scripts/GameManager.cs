using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent _OnReset;

    public static GameManager _Instance;

    public GameObject _ReadyPannel;
    public TextMeshProUGUI _ScoreText;
    public TextMeshProUGUI _BestScoreText;
    public TextMeshProUGUI _MessageText;

    public bool _IsRoundActive = false;

    private int _score = 0;

    public ShooterRotator _ShooterRotator;
    public CamFollow _Cam;

    private void Awake()
    {
        _Instance = this;
        UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RoundRoutine");    
    }

    public void AddScore(int newScore)
    {
        _score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if (GetBestScore() < _score)
            PlayerPrefs.SetInt("BestScore", _score);
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }

    void UpdateUI()
    {
        _ScoreText.text = "Score: " + _score;
        _BestScoreText.text = "Best Score: " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        _IsRoundActive = false;
    }

    public void Reset()
    {
        _score = 0;
        UpdateUI();

        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
        // Ready
        _OnReset.Invoke();
        _ReadyPannel.SetActive(true);
        _Cam.SetTarget(_ShooterRotator.transform, CamFollow.State.Idle);
        _ShooterRotator.enabled = false;
        _IsRoundActive = false;
        _MessageText.text = "Ready...";
        yield return new WaitForSeconds(3f);

        // Play
        _IsRoundActive = true;
        _ReadyPannel.SetActive(false);
        _ShooterRotator.enabled = true;
        _Cam.SetTarget(_ShooterRotator.transform, CamFollow.State.Ready);
        while(_IsRoundActive)
        {
            yield return null;
        }

        // End
        _ReadyPannel.SetActive(true);
        _ShooterRotator.enabled = false;
        _MessageText.text = "Wait For Next Round...";
        yield return new WaitForSeconds(3f);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
