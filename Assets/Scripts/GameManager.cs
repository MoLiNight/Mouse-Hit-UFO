using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject physisManager;
    public GameObject ccActionManager;

    public GameObject physisButton;
    public GameObject ccActionButton;
    // Start is called before the first frame update
    void Start()
    {
        GameObject plane = GameObject.Instantiate(Resources.Load("Prefabs/Plane", typeof(GameObject))) as GameObject;
    }

    public void ChoosePhysis()
    {
        physisButton.SetActive(false);
        ccActionButton.SetActive(false);
        physisManager.SetActive(true);

        SSDirector.GetInstance().SetGameMode(0);
    }

    public void ChooseCCAction()
    {
        physisButton.SetActive(false);
        ccActionButton.SetActive(false);
        ccActionManager.SetActive(true);

        SSDirector.GetInstance().SetGameMode(1);
    }
}
