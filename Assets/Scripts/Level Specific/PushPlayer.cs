using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    private Rigidbody2D playerBody;

    public Vector2 ForceToApply;

    void Start()
    {
    }

    void OnEnabled()
    {
    }

    public void ApplyPush()
    {
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerBody.AddForce(ForceToApply, ForceMode2D.Impulse);
    }

}