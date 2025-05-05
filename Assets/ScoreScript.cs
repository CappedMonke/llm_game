using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int score;
    public Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [ContextMenu("Add Score")]
    public void addScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
