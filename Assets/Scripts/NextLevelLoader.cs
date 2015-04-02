using UnityEngine;
using System.Collections;

public class NextLevelLoader : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadNext()
    {
        Application.LoadLevel(Application.loadedLevel+1);
    }
}