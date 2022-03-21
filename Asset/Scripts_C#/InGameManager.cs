using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InGameManager : MonoBehaviour
{
    public SceneSwitcher sceneSwitcher;
    public Text userScoreText;

    CurrentPlayer _player;

    private void Start()
    {
        _player = CurrentPlayer.singleton;

        RefreshPlayerScoreText();
    }

    void RefreshPlayerScoreText()
    {
        StringBuilder _strBuilder = new StringBuilder();
        _strBuilder.Append("Points: ").Append(_player._score);

        userScoreText.text = _strBuilder.ToString();
    }

    public void PlayerScorePlus10()
    {
        _player._score += 10;
        RefreshPlayerScoreText();
    }

    public void PlayerScorePlus100()
    {
        _player._score += 100;
        RefreshPlayerScoreText();
    }

    public void EndGame()
    {
        StartCoroutine(SavePlayerScore());
        sceneSwitcher.LoadPlayerGameScene();
    }

    IEnumerator SavePlayerScore()
    {
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("username", _player._username);
        scoreForm.AddField("score", _player._score);

        // App Key
        scoreForm.AddField("apppassword", "thisisfromtheapp");

        UnityWebRequest updatePlayerScoreRequest = UnityWebRequest.Post("http://localhost/cruds/updatePlayerScore.php", scoreForm);
        yield return updatePlayerScoreRequest.SendWebRequest();

        if (updatePlayerScoreRequest.error == null)
        {
            string response = updatePlayerScoreRequest.downloadHandler.text;
            if (response == "0")
            {
                Debug.Log("Good to go");
            }
            else
            {
                Debug.Log("Something's not right...");
            }
        }
        else
        {
            Debug.Log(updatePlayerScoreRequest.error);
        }
    }
}
