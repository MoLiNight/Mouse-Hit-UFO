using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object
{
    // singlton instance
    private static SSDirector _instance;
    public RoundController roundController { get; set; }
    public bool running { get; set; }

    public int gameMode; // gameMode = 0 -> Physis, gameMode = 1 -> CCAction

    // get instance anytime anywhare!
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }

    public int getFPS()
    {
        return Application.targetFrameRate;
    }

    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void NextScene()
    {
        Debug.Log("Waiting next Scene now...");
        Application.Quit();
    }

    public void SetGameMode(int gameMode)
    {
        this.gameMode = gameMode;
    }

}
