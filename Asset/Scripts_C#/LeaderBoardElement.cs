using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardElement : MonoBehaviour
{
    [Header("UI (Drops).")]
    public Text userRankText;
    public Text usernameText;
    public Text userScoreText;

    #region Hide In Inspector.
    [HideInInspector] public int userRank;
    [HideInInspector] public string username;
    [HideInInspector] public int userScore;
    #endregion

    public void AssignInfo()
    {
        usernameText.text = username;
        userScoreText.text = userScore.ToString();
        userRankText.text = userRank.ToString() + ".";
    }
}
