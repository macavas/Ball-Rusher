using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {



    public static GameManager instance;

    public GameObject menuPanel;
    public GameObject pauseButton;
    public GameObject restartButton;
    public GameObject resumeButton;
    public GameObject rocketButton;
    public GameObject tapStartImage;
    public GameObject turnScreenImage;
    public Text bonusText;
    public Text highScoreText;
    public Text scoreMenuText;
    public Text coinText;
    public Text scoreText;
    public Text rocketText;
    public Text newHSText;
    public int score;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        if (instance == null)
        {
            instance = this;
        }
        
        score = 0;
        scoreText.text = score.ToString();
        rocketText.text = PlayerPrefs.GetInt("RocketBonus" ,0).ToString();
        highScoreText.text = "High Score: "+PlayerPrefs.GetInt("HighScore",0).ToString();
        // Menu Panel Show Settings
        menuPanel.SetActive(false);
        restartButton.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        rocketButton.SetActive(true);
        tapStartImage.SetActive(true);
        turnScreenImage.SetActive(true);
        bonusText.text = "";
        newHSText.text = "";
    }
	
    public void IncreaseScore(int scr)
    {
        score = scr;
        scoreText.text = score.ToString();
    }

    public void EndGame()
    {
        ShowAdAtTheEnd();
        Time.timeScale = 0f;
        scoreMenuText.text = "Score: "+score.ToString();
        GetHighScore();
        highScoreText.text = "High Score: "+PlayerPrefs.GetInt("HighScore" ,0).ToString();
        coinText.text = "Coins: " + Player.instance.collectedCoins.ToString();
        getCoins();
        restartButton.SetActive(true);
        menuPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ShowAdAtTheEnd()
    {
        int totalGames = PlayerPrefs.GetInt("TotalGames", 0);
        totalGames++;
        PlayerPrefs.SetInt("TotalGames", totalGames);
        if(totalGames % 2 == 0)
        {
            ADS.ShowAd();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("MenuButton");
        //SceneManager.LoadScene("Main");
        LevelChanger.instance.FadeToLevel("Main");
    }

    public void PauseGame()
    {
        if (Player.instance.canThrow)
        {
            tapStartImage.SetActive(false);
            rocketButton.SetActive(false);
        }
        FindObjectOfType<AudioManager>().Play("MenuButton");
        Time.timeScale = 0f;
        scoreMenuText.text = "Score :" + score.ToString();
        coinText.text = "Coins: " + Player.instance.collectedCoins.ToString();
        resumeButton.SetActive(true);
        menuPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        if (Player.instance.canThrow)
        {
            tapStartImage.SetActive(true);
            rocketButton.SetActive(true);
        }
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("MenuButton");
        resumeButton.SetActive(false);
        menuPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void GetHighScore()
    {
        if(PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore",score);
            newHSText.text = "NEW";
        }
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("MenuButton");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        LevelChanger.instance.FadeToLevel("Menu");
    }

    public void getCoins()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        coins += Player.instance.collectedCoins;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void RocketButton()
    {
        int rocketBonusValue = PlayerPrefs.GetInt("RocketBonus", 0);

        if(rocketBonusValue > 0)
        {
            rocketBonusValue--;
            tapStartImage.SetActive(false);
            PlayerPrefs.SetInt("RocketBonus", rocketBonusValue);
            rocketButton.SetActive(false);
            turnScreenImage.SetActive(false);
            Player.instance.StartRocket();
        }
    }

    public void ShowBonusText(string bonus)
    {
        StartCoroutine(BonusText(bonus));
    }

    IEnumerator BonusText(string bonus)
    {
        bonusText.text = bonus;
        
        yield return new WaitForSeconds(1);
        bonusText.text = "";
    }
}
