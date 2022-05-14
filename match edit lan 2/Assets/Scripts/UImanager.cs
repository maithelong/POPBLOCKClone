using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameoverpanel;
    [SerializeField] Text scoretxt;
    [SerializeField] Text highscoretxt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setScoretext(string text)
    {
        if (scoretxt)
        { scoretxt.text = text; }

    }    public void setHighScoretext(string text)
    {
        if (highscoretxt)
        { highscoretxt.text = text; }

    }
    public void setgameoverpanel(bool isshow)
    {
        if (gameoverpanel)
        { gameoverpanel.SetActive(isshow); }
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }    
    public void Exit()
    {
        Application.Quit();
    }    
}
