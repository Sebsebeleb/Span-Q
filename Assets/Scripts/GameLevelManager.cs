using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes sure the level is able to be rebuilt after a simulation stop. Warning: ALL game level objects that are supposed to be part of the level must be parented to this' gameobject
/// </summary>
public class GameLevelManager : MonoBehaviour
{

    private List<GameObject> levelData = new List<GameObject>();

    void Start()
    {
        // First we make copies of all the gameobjects to use as secret prefabs
        StoreLevelObjects();
    }

    private void StoreLevelObjects()
    {
        foreach (Transform levelObject in transform)
        {
            GameObject copy = Instantiate(levelObject.gameObject);
            copy.SetActive(false);
            levelData.Add(copy);
        }
    }

    void Update()
    {

    }

    /// Bug: Old timeline event display images are not removed, could potentially be a problem
    public void ResetLevel()
    {
        DeleteAll();
        RestoreAll();
    }

    private void DeleteAll()
    {
        foreach (Transform levelObject in transform)
        {
            Destroy(levelObject.gameObject);
        }
    }

    private void RestoreAll()
    {
        foreach (GameObject levelObject in levelData)
        {
            GameObject newCopy = Instantiate(levelObject);
            newCopy.transform.SetParent(transform);
            newCopy.SetActive(true);
        }
    }
}