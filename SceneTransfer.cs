using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public bool resetHolder;
    private void Awake()
    {
        if (resetHolder) Holder.Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText != null && highscoreText != null)
        {
            scoreText.text += Holder.score.ToString();
            if (Holder.score > Holder.highscore) Holder.highscore = Holder.score;
            highscoreText.text += Holder.highscore.ToString();
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame() {
        Holder.Reset();
        if (SceneManager.GetActiveScene().name == "Menu") {
            SceneManager.LoadScene("Endless");
        }
        else {
            SceneManager.LoadScene(Holder.lastScene); 
        }
    }
    public void LoadTutorial() {
        Holder.Reset();
        SceneManager.LoadScene("Tutorial");
    }
}
