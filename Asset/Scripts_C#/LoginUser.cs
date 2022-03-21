using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginUser : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    public Button loginButton;
    public Text loginButtonText;
    public Image loginButtonImage;

    [Header("Current Player Obj.")]
    public CurrentPlayer curPlayer;
    public SceneSwitcher sceneSwitcher;

    private void Awake()
    {
        if (CurrentPlayer.singleton != null)
            CurrentPlayer.singleton.DestroyPlayer();
    }

    public void Login()
    {
        loginButton.interactable = false;
        loginButtonText.text = "Sending...";

        if (usernameInput.text.Length < 3)
        {
            ErrorOnLoginMessage("Check username.");
        }
        else if (passwordInput.text.Length < 3)
        {
            ErrorOnLoginMessage("Check password.");
        }
        else
        {
            StartCoroutine(SendLoginForm());
        }
    }

    void ErrorOnLoginMessage(string message)
    {
        loginButtonImage.color = Color.red;
        loginButtonText.text = message;
        loginButtonText.fontSize = 70;

        loginButton.interactable = false;
    }
    
    void ResetButton()
    {
        loginButtonImage.color = Color.white;
        loginButtonText.text = "Login";
        loginButtonText.fontSize = 100;

        loginButton.interactable = true;
    }

    void SpawnPlayerInfo(string _playerDBInfo)
    {
        CurrentPlayer _curPlayer = Instantiate(curPlayer);

        string[] infoAry = _playerDBInfo.Split(':');
        _curPlayer.AssignInfo(infoAry[0], "", infoAry[1]);
    }

    IEnumerator SendLoginForm()
    {
        WWWForm loginInfo = new WWWForm();
        loginInfo.AddField("username", usernameInput.text);
        loginInfo.AddField("password", passwordInput.text);

        // App Key
        loginInfo.AddField("apppassword", "thisisfromtheapp");

        UnityWebRequest loginRequest = UnityWebRequest.Post("http://localhost/cruds/loginUser.php", loginInfo);
        yield return loginRequest.SendWebRequest();

        if (loginRequest.error == null)
        {
            string response = loginRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "5")
            {
                ErrorOnLoginMessage("Server Error");
            }
            else if (response == "3")
            {
                ErrorOnLoginMessage("Check Username");
            }
            else if (response == "4")
            {
                ErrorOnLoginMessage("Check Password");
            }
            else
            {
                SpawnPlayerInfo(response);
                sceneSwitcher.LoadPlayerGameScene();
            }
        }
        else
        {
            Debug.Log(loginRequest.error);
        }
    }
}
