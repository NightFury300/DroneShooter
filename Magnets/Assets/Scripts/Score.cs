using TMPro;
using UnityEngine;

public static class Score
{
    [SerializeField]
    private static TextMeshProUGUI scoreText;
    [SerializeField]
    private static TextMeshProUGUI highscoreText;

    private static float score = 0.0f;

    [SerializeField]
    private static int defaultScoreIncreaseSpeedPerSecond = 1000/60;
    
    private static int scoreIncreaseSpeedPerSecond = defaultScoreIncreaseSpeedPerSecond;

    public static void Init(GameObject scoreUI,GameObject highscoreUI)
    {
        scoreText = scoreUI.GetComponentInChildren<TextMeshProUGUI>();
        highscoreText = highscoreUI.GetComponentInChildren<TextMeshProUGUI>();
    }

    private static void UpdateScoreGUI() 
    {
        scoreText.SetText(((int)score).ToString());
    }

    public static void IncreaseScore()
    {
        score += scoreIncreaseSpeedPerSecond * Time.deltaTime;
        Debug.Log(scoreIncreaseSpeedPerSecond);
        UpdateScoreGUI();
    }

    public static void ResetScore()
    {
        score = 0.0f;
        UpdateScoreGUI();
    }

    private static void UpdateHighScoreGUI()
    {
        highscoreText.SetText(((int)PlayerPrefs.GetFloat("highscore")).ToString());
    }

    public static void UpdateHighScore(float highscore)
    {
        PlayerPrefs.SetFloat("highscore", highscore);
        UpdateHighScoreGUI();
    }

    public static void ResetHighScore()
    {
        UpdateHighScore(0.0f);
    }

    public static void BoostScore(int amount)
    {
        score += amount;
    }

    public static void BoostScoreByMultiplier(float multiplier)
    {
        scoreIncreaseSpeedPerSecond = (int)(defaultScoreIncreaseSpeedPerSecond * multiplier);
    }

    public static float GetCurrentScore()
    {
        return score;
    }

    public static float GetCurrentHighScore()
    {
        return PlayerPrefs.GetFloat("highscore");
    }
}
