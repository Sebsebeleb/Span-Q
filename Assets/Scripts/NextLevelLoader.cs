using UnityEngine;

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
        Application.LoadLevel(Application.loadedLevel + 1);
        TimeLine.Reset();
    }
}