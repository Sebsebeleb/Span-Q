using UnityEngine;
using System.Collections;

public class GoalCheck : MonoBehaviour
{

    public GameObject YouWinButton;

    void Start()
    {
        // Debug check to make sure there actually is a damned win button prefab thing
        if (!YouWinButton) {
            Debug.LogError("Goddamnit, you forgot to assing the win button");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            Win();
        }
    }

    private void Win()
    {
        GameObject newButton = Instantiate(YouWinButton);
        newButton.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, true);
        Vector3 newPos = newButton.transform.position;
        newPos.x = Screen.width/2;
        newPos.y = Screen.height/2;

        newButton.transform.position = newPos;
        newButton.SetActive(true);
    }
}