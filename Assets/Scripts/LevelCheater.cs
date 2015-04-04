using UnityEngine;
using System.Collections;

public class LevelCheater : MonoBehaviour
{
    public GameObject LevelButtonPrefab;
    public Transform ButtonContainer;

    void Start()
    {
        for (int i = 0; i < Application.levelCount; i++) {
            if (i != Application.loadedLevel) {
                MakeButton(i);
            }
        }

    }

    private void MakeButton(int levelToLoad)
    {
        GameObject levelButton = Instantiate(LevelButtonPrefab);
        levelButton.GetComponent<LevelLoadButtonBehaviour>().SetLevelToLoad(levelToLoad);

        levelButton.transform.SetParent(ButtonContainer);
    }

    void Update()
    {

    }
}