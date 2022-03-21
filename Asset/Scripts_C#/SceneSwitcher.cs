using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadWelcomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNewUserScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLoginScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadPlayerGameScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadInGameScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLeaderboadScene()
    {
        SceneManager.LoadScene(5);
    }
}
