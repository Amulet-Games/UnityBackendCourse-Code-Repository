using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI (Drops).")]
    public InputField searchInputField;
    public Button searchButton;
    public Text searchButtonText;
    public Image searchButtonImage;

    [Header("Prefabs (Drops).")]
    public LeaderBoardElement lbElementPrefab;
    public Transform contentTransform;

    [Header("Refs (Drops).")]
    public SceneSwitcher sceneSwitcher;

    #region Private.
    List<LeaderBoardElement> leaderBoardElements = new List<LeaderBoardElement>();
    int leaderBoardElementCount;
    #endregion

    #region Callbacks
    private void Start()
    {
        StartCoroutine(GetAllPlayer());
    }
    #endregion

    #region Networking.
    IEnumerator GetAllPlayer()
    {
        WWWForm getAllPlayerForm = new WWWForm();

        // App Key
        getAllPlayerForm.AddField("apppassword", "thisisfromtheapp");

        UnityWebRequest getAllPlayerRequest = UnityWebRequest.Post("http://localhost/cruds/getAllPlayer.php", getAllPlayerForm);
        yield return getAllPlayerRequest.SendWebRequest();
        if (getAllPlayerRequest.error == null)
        {
            PlayerRequestResultDecoder(getAllPlayerRequest);
        }
        else
        {
            Debug.Log(getAllPlayerRequest.error);
        }
    }

    IEnumerator GetPlayerByName()
    {
        WWWForm getPlayerByNameForm = new WWWForm();
        getPlayerByNameForm.AddField("search", searchInputField.text);

        // App Key
        getPlayerByNameForm.AddField("apppassword", "thisisfromtheapp");

        UnityWebRequest getPlayerByNameRequest = UnityWebRequest.Post("http://localhost/cruds/getPlayerByName.php", getPlayerByNameForm);
        yield return getPlayerByNameRequest.SendWebRequest();
        if (getPlayerByNameRequest.error == null)
        {
            PlayerRequestResultDecoder(getPlayerByNameRequest);
        }
        else
        {
            Debug.Log(getPlayerByNameRequest.error);
        }

        ResetSearchBtn();
    }
    #endregion

    #region Buttons OnClick.
    public void UIButton_LoadPlayerGameScene()
    {
        sceneSwitcher.LoadPlayerGameScene();
    }

    public void UIButton_SearchPlayer()
    {
        DestroyAllPlayerRequestedResults();
        SetSearchBtnAsSearching();
        StartCoroutine(GetPlayerByName());
    }
    #endregion

    #region Search Text Image Changes.
    private void SetSearchBtnAsSearching()
    {
        searchButtonText.text = "Searching...";
        searchButtonImage.color = Color.green;
        searchButton.interactable = false;
    }

    private void ResetSearchBtn()
    {
        searchButtonText.text = "Search";
        searchButtonImage.color = Color.white;
        searchButton.interactable = true;
    }
    #endregion

    private void PlayerRequestResultDecoder(UnityWebRequest unityWebRequest)
    {
        JSONNode allPlayers = JSON.Parse(unityWebRequest.downloadHandler.text);
        int allPlayersCount = allPlayers.Count;
        for (int i = 0; i < allPlayersCount; i++)
        {
            LeaderBoardElement newLbElement = Instantiate(lbElementPrefab, Vector3.zero, Quaternion.identity);
            newLbElement.transform.SetParent(contentTransform);

            newLbElement.userRank = i + 1;
            newLbElement.username = allPlayers[i][0];
            newLbElement.userScore = allPlayers[i][1];
            newLbElement.AssignInfo();

            leaderBoardElements.Add(newLbElement);
            leaderBoardElementCount++;
        }
    }

    private void DestroyAllPlayerRequestedResults()
    {
        for (int i = 0; i < leaderBoardElementCount; i++)
        {
            Destroy(leaderBoardElements[i].gameObject);
        }

        leaderBoardElements.Clear();
        leaderBoardElementCount = 0;
    }
}
