using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public static LevelChanger instance;
    
    public Animator animator;
    private string levelToLoad;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    

    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
