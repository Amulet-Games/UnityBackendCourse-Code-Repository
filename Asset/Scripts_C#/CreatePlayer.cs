using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreatePlayer : MonoBehaviour
{
    public InputField usernameInput;
    public InputField emailInput;
    public InputField passwordInput;

    public Button RegisterButton;
    public Text RegisterButtonText;
    public Image RegisterButtonImage;

    public void RegisterNewPlayer()
    {
        RegisterButton.interactable = false;

        if (usernameInput.text.Length < 3)
        {
            ErrorMessage("Username is too short");
        }
        else if (passwordInput.text.Length < 3)
        {
            ErrorMessage("Password is too short");
        }
        else if (emailInput.text.Length < 5)
        {
            ErrorMessage("Email is too short");
        }
        else
        {
            SetButtonToSending();
            StartCoroutine(CreatePlayerPostRequest());
        }
    }

    public void ErrorMessage(string message)
    {
        RegisterButtonImage.color = Color.red;
        RegisterButtonText.text = message;
        RegisterButtonText.fontSize = 57;
    }

    public void ResetRegisterButton()
    {
        RegisterButtonImage.color = Color.white;
        RegisterButtonText.text = "Register";
        RegisterButtonText.fontSize = 100;

        RegisterButton.interactable = true;
    }

    public void SetButtonToSending()
    {
        RegisterButtonImage.color = Color.blue;
        RegisterButtonText.text = "Sending...";
        RegisterButtonText.fontSize = 70;
    }

    public void SetButtonSuccess()
    {
        RegisterButtonImage.color = Color.green;
        RegisterButtonText.text = "Success!";
        RegisterButtonText.fontSize = 80;
    }

    IEnumerator CreatePlayerPostRequest()
    {
        WWWForm newPlayerInfo = new WWWForm();
        newPlayerInfo.AddField("username", usernameInput.text);
        newPlayerInfo.AddField("email", emailInput.text);
        newPlayerInfo.AddField("password", passwordInput.text);

        // App Key
        newPlayerInfo.AddField("apppassword", "thisisfromtheapp");

        UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/cruds/newPlayer.php", newPlayerInfo);
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "4" || response == "6")
            {
                ErrorMessage("Server Error");
            }
            else if (response == "3")
            {
                ErrorMessage("Username already exists.");
            }
            else if (response == "6")
            {
                ErrorMessage("Email already exists.");
            }
            else
            {
                SetButtonSuccess();
            }
        }
        else
        {
            Debug.Log(createPostRequest.error);
        }
    }
}
