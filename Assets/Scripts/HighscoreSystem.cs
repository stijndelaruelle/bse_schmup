using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class HighscoreSystem : MonoBehaviour
{
    [SerializeField]
    private string m_AddHighscoreURL;

    [SerializeField]
    private string m_GetHighscoreURL;

    private string m_SecretKey = "sbasletsscamlhtuspasletcsraelttkey"; //bseschmupsecretkey with salt in between

    public void Start()
    {
        //Debug
        //PostHighscore("Jessica", 3685);
    }

    public void PostHighscore(string name, int score)
    {
        StartCoroutine(PostHighscoreRoutine(name, score));
    }

    public void GetHighscores()
    {
        StartCoroutine(GetHighscoreRoutine());
    }

    private IEnumerator PostHighscoreRoutine(string name, int score)
    {
        string hash = Md5Sum(name + score + m_SecretKey);
        string postUrl = m_AddHighscoreURL + "?name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

        WWW highscorePost = new WWW(postUrl);
        yield return highscorePost;

        if (highscorePost.error != null)
        {
            print("There was an error posting the high score: " + highscorePost.error);
        }
    }

    private IEnumerator GetHighscoreRoutine()
    {
        Debug.Log("Loading Scores");

        WWW highscoreGet = new WWW(m_GetHighscoreURL);
        yield return highscoreGet;

        if (highscoreGet.error != null)
        {
            Debug.Log("There was an error getting the high score: " + highscoreGet.error);
        }
        else
        {
            Debug.Log(highscoreGet.text);
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
