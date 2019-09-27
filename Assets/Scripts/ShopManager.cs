using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public int[] prices = new int[] {200,500,800,1100,1400};
    public int rocketPrice = 100;

    public Text lastCoins;

    public Text biggerPriceText;
    public Text biggerLevelText;

    public Text gravityPriceText;
    public Text gravityLevelText;
    
    public Text coinPriceText;
    public Text coinLevelText;

    public Text immunePriceText;
    public Text immuneLevelText;

    public Text jetPriceText;
    public Text jetLevelText;

    public Text magnetPriceText;
    public Text magnetLevelText;

    public Text rocketPriceText;
    public Text rocketCountText;
    
    private void Start()
    {
        SetBiggerFeatures();
        SetGravityFeatures();
        SetCoinFeatures();
        SetImmuneFeatures();
        SetJetFeatures();
        SetMagnetFeatures();
        SetRocketFeatures();
        ShowCoins();
    }


    public void BiggerIncrease()
    {
        int biggerBL = PlayerPrefs.GetInt("BiggerBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);
        
        if(biggerBL < 6 && coins > prices[biggerBL-1])
        {
            coins = coins - prices[biggerBL - 1];
            PlayerPrefs.SetInt("BiggerBonus", biggerBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }
        
        SetBiggerFeatures();
    }

    public void GravityIncrease()
    {
        int gravityBL = PlayerPrefs.GetInt("GravityBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if (gravityBL < 6 && coins > prices[gravityBL - 1])
        {
            coins = coins - prices[gravityBL - 1];
            PlayerPrefs.SetInt("GravityBonus", gravityBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }

        SetGravityFeatures();
    }

    public void CoinIncrease()
    {
        int coinBL = PlayerPrefs.GetInt("CoinBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if (coinBL < 6 && coins > prices[coinBL - 1])
        {
            coins = coins - prices[coinBL - 1];
            PlayerPrefs.SetInt("CoinBonus", coinBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }

        SetCoinFeatures();
    }

    public void ImmuneIncrease()
    {
        int immuneBL = PlayerPrefs.GetInt("ImmuneBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if (immuneBL < 6 && coins > prices[immuneBL - 1])
        {
            coins = coins - prices[immuneBL - 1];
            PlayerPrefs.SetInt("ImmuneBonus", immuneBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }

        SetImmuneFeatures();
    }

    public void RocketIncrease()
    {
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if(coins > rocketPrice)
        {
            coins -= rocketPrice;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("RocketBonus", PlayerPrefs.GetInt("RocketBonus", 0)+1);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }
        SetRocketFeatures();
    }

    public void JetIncrease()
    {
        int jeyBL = PlayerPrefs.GetInt("JetBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if (jeyBL < 6 && coins > prices[jeyBL - 1])
        {
            coins = coins - prices[jeyBL - 1];
            PlayerPrefs.SetInt("JetBonus", jeyBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }

        SetJetFeatures();
    }
    
    public void MagnetIncrease()
    {
        int magnetBL = PlayerPrefs.GetInt("MagnetBonus", 1);
        int coins = PlayerPrefs.GetInt("Coins", 1);

        if (magnetBL < 6 && coins > prices[magnetBL - 1])
        {
            coins = coins - prices[magnetBL - 1];
            PlayerPrefs.SetInt("MagnetBonus", magnetBL + 1);
            PlayerPrefs.SetInt("Coins", coins);
            FindObjectOfType<AudioManager>().Play("ShopBuy");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("ShopError");
        }

        SetMagnetFeatures();
    }

    void SetBiggerFeatures()
    {
        int biggerBL = PlayerPrefs.GetInt("BiggerBonus", 1);
        
        if(biggerBL == 6)
        {
            biggerLevelText.text = "MAX";
            biggerPriceText.text = "MAX";
        }
        else
        {
            biggerPriceText.text = prices[biggerBL - 1].ToString();
            biggerLevelText.text = "Level " + biggerBL;
        }
        ShowCoins();
    }

    void SetGravityFeatures()
    {
        int gravityBL = PlayerPrefs.GetInt("GravityBonus", 1);
        
        if (gravityBL == 6)
        {
            gravityLevelText.text = "MAX";
            gravityPriceText.text = "MAX";
        }
        else
        {
            gravityPriceText.text = prices[gravityBL - 1].ToString();
            gravityLevelText.text = "Level " + gravityBL;
        }
        ShowCoins();
    }

    void SetCoinFeatures()
    {
        int coinBL = PlayerPrefs.GetInt("CoinBonus", 1);
        
        if (coinBL == 6)
        {
            coinLevelText.text = "MAX";
            coinPriceText.text = "MAX";
        }
        else
        {
            coinPriceText.text = prices[coinBL - 1].ToString();
            coinLevelText.text = "Level " + coinBL;
        }
        ShowCoins();
    }

    void SetImmuneFeatures()
    {
        int immuneBL = PlayerPrefs.GetInt("ImmuneBonus", 1);
        
        if (immuneBL == 6)
        {
            immunePriceText.text = "MAX";
            immuneLevelText.text = "MAX";
        }
        else
        {
            immunePriceText.text = prices[immuneBL - 1].ToString();
            immuneLevelText.text = "Level " + immuneBL;
        }
        ShowCoins();
    }

    void SetJetFeatures()
    {
        int jetBL = PlayerPrefs.GetInt("JetBonus", 1);
        
        if (jetBL == 6)
        {
            jetPriceText.text = "MAX";
            jetLevelText.text = "MAX";
        }
        else
        {
            jetPriceText.text = prices[jetBL - 1].ToString();
            jetLevelText.text = "Level " + jetBL;
        }
        ShowCoins();
    }

    void SetMagnetFeatures()
    {
        int magnetBL = PlayerPrefs.GetInt("MagnetBonus", 1);
        
        if (magnetBL == 6)
        {
            magnetPriceText.text = "MAX";
            magnetLevelText.text = "MAX";
        }
        else
        {
            magnetPriceText.text = prices[magnetBL - 1].ToString();
            magnetLevelText.text = "Level " + magnetBL;
        }
        ShowCoins();
    }

    void SetRocketFeatures()
    {
        rocketPriceText.text = rocketPrice.ToString();
        rocketCountText.text = "Piece: "+PlayerPrefs.GetInt("RocketBonus", 0).ToString();
        ShowCoins();
    }

    public void MenuButton()
    {
        FindObjectOfType<AudioManager>().Play("MenuButton");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        LevelChanger.instance.FadeToLevel("Menu");
    }

    public void ShowCoins()
    {
        lastCoins.text = PlayerPrefs.GetInt("Coins", 1).ToString();
    }
}
