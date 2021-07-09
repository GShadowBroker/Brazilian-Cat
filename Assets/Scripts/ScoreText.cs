using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }


    public void UpdateScoreText(int newScore)
    {
        scoreText.SetText(newScore.ToString() + "/10");
    }
}
