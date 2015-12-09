using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;

public struct Highscore
{
    public Highscore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

    public string name;
    public int score;
}

public class HighscoreSystem : MonoBehaviour
{
    [SerializeField]
    private string m_AddHighscoreURL;

    [SerializeField]
    private string m_GetHighscoreURL;

    private List<Highscore> m_Highscores;
    public List<Highscore> Highscores
    {
        get { return m_Highscores; }
    }

    private string m_SecretKey = "sbasletsscamlhtuspasletcsraelttkey"; //bseschmupsecretkey with salt in between

    private Action m_HighscorePostedEvent;
    public Action HighscorePostedEvent
    {
        get { return m_HighscorePostedEvent; }
        set { m_HighscorePostedEvent = value; }
    }

    private Action m_HighscoreUpdatedEvent;
    public Action HighscoreUpdatedEvent
    {
        get { return m_HighscoreUpdatedEvent; }
        set { m_HighscoreUpdatedEvent = value; }
    }

    public void Start()
    {
        m_Highscores = new List<Highscore>();
    }

    public void PostHighscore(string name, string email, int score)
    {
        StartCoroutine(PostHighscoreRoutine(name, email, score));
    }

    public void UpdateHighscores()
    {
        StartCoroutine(UpdateHighscoresRoutine());
    }

    public Highscore GetHighscore(int highscoreID)
    {
        if (highscoreID >= 0 && highscoreID < m_Highscores.Count)
        {
            return m_Highscores[highscoreID];
        }

        return new Highscore("Empty", 0);
    }

    private IEnumerator PostHighscoreRoutine(string name, string email, int score)
    {
        string hash = Md5Sum(name + score + email + m_SecretKey);
        string postUrl = m_AddHighscoreURL + "?name=" + WWW.EscapeURL(name) + "&score=" + score + "&email=" + WWW.EscapeURL(email) + "&hash=" + hash;

        WWW highscorePost = new WWW(postUrl);
        yield return highscorePost;

        if (highscorePost.error != null)
        {
            Debug.Log("There was an error posting the high score: " + highscorePost.error);
        }
        else
        {
            if (m_HighscorePostedEvent != null)
                m_HighscorePostedEvent();
        }
    }

    private IEnumerator UpdateHighscoresRoutine()
    {
        Debug.Log("Loading Scores");

        WWW highscoreGet = new WWW(m_GetHighscoreURL);
        yield return highscoreGet;

        if (highscoreGet.error != null)
        {
            Debug.Log("There was an error getting the highscores: " + highscoreGet.error);
        }
        else
        {
            //Split the string (comma separated)
            char[] delimiterChars = { ',' };
            string[] words = highscoreGet.text.Split(delimiterChars);

            m_Highscores.Clear();
            for (int i = 0; i < words.Length; i += 2)
            {
                if (String.IsNullOrEmpty(words[i]))
                    continue;

                Highscore highscore = new Highscore();
                highscore.name = words[i];
                bool success = int.TryParse(words[i + 1], out highscore.score);

                m_Highscores.Add(highscore);
            }

            if (m_HighscoreUpdatedEvent != null)
                m_HighscoreUpdatedEvent();
        }
    }

    //Taken from: http://wiki.unity3d.com/index.php?title=MD5
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
