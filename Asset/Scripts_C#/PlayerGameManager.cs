using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGameManager : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;
    public Text userInfoText;

    CurrentPlayer _player;

    private void Start()
    {
        _player = CurrentPlayer.singleton;

        StringBuilder _strBuilder = new StringBuilder();
        _strBuilder.Append("User: ").Append(_player._username).Append(" | Points: ").Append(_player._score);
        
        userInfoText.text = _strBuilder.ToString();
    }

    public void SignOut()
    {
        CurrentPlayer.singleton.DestroyPlayer();
        sceneSwitcher.LoadWelcomeScene();
    }

    public void StartGame()
    {
        sceneSwitcher.LoadInGameScene();
    }

    public void LoadLeaderboard()
    {
        sceneSwitcher.LoadLeaderboadScene();
    }
}
