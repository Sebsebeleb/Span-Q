using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    private Rigidbody2D playerBody;

    public Vector2 ForceToApply;
    public bool InvertedofGravity = false;

    void Start()
    {
    }

    void OnEnabled()
    {
    }

    public void ApplyPush()
    {
        Vector2 actualForce = ForceToApply;
        // Only cares about +/- y
        if (InvertedofGravity) {
            if (Physics2D.gravity.y > 0) {
                actualForce.y *= -1;
            }
            
        }
        playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        playerBody.AddForce(actualForce, ForceMode2D.Impulse);
    }

}