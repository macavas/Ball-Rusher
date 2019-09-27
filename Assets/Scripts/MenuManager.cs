using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private const string GOOGLEPLAY_URL = "";

    public static MenuManager instance;

    public Text highScore;
    public Text coins;

    public Button soundButton;
    public Sprite soundOn;
    public Sprite soundOff;

    public Button musicButton;
    public Sprite musicOn;
    public Sprite musicOff;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(PlayerPrefs.GetInt("Sound" ,1) == 1)
        {
            soundButton.image.sprite = soundOn;
        }
        else
        {
            soundButton.image.sprite = soundOff;
        }
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            musicButton.image.sprite = musicOn;
        }
        else
        {
            musicButton.image.sprite = musicOff;
        }
        Time.timeScale = 1;
        highScore.text = PlayerPrefs.GetInt("HighScore" ,0).ToString();
        coins.text = PlayerPrefs.GetInt("Coins",0).ToString();
    }

    public void OnPlayClick()
    {
        FindObjectOfType<AudioManager>().Play("MenuButton");
        //SceneManager.LoadScene("Main");
        LevelChanger.instance.FadeToLevel("Main");
    }

    public void OnShopClick()
    {
        FindObjectOfType<AudioManager>().Play("MenuButton");
        //SceneManager.LoadScene("Shop");
        LevelChanger.instance.FadeToLevel("Shop");
    }

    public void OnRateClick()
    {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.SquareWarriorGames.BallRusher");
    }

    public void OnSoundClick()
    {
        if(PlayerPrefs.GetInt("Sound" ,1) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
            soundButton.image.sprite = soundOff;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundButton.image.sprite = soundOn;
        }
    }

    public void OnMusicClick()
    {
        if(PlayerPrefs.GetInt("Music" ,1) == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            musicButton.image.sprite = musicOff;
            FindObjectOfType<AudioManager>().StopMusic();
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            musicButton.image.sprite = musicOn;
            FindObjectOfType<AudioManager>().PlayMusic();
        }
    }

    public void OnNoAdsClick()
    {

    }

    public void OnLeaderBoardClick()
    {

    }


}
