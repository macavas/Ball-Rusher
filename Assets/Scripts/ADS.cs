using UnityEngine.Advertisements;
using UnityEngine;

public class ADS : MonoBehaviour {
    
    public static void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

}
