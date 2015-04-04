using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelLoadButtonBehaviour : MonoBehaviour
{

    private int levelToLoad;

    public void SetLevelToLoad(int i)
    {
        levelToLoad = i;

        GetComponentInChildren<Text>().text = "Level " + (i + 1);
    }

    public void LoadOurLevel()
    {
        Application.LoadLevel(levelToLoad);   
    }
}