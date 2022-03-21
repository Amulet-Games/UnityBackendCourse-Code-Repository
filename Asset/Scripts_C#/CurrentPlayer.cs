using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPlayer : MonoBehaviour
{
    public string _username;
    public string _email;
    public int _score;

    public static CurrentPlayer singleton;
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AssignInfo(string username, string email, string score)
    {
        _username = username;
        _email = email;
        
        if (int.TryParse(score, out _score))
        {
            _score = int.Parse(score);
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
