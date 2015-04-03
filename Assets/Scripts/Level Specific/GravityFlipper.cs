using UnityEngine;
using System.Collections;

public class GravityFlipper : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }


    public void FlipGravity()
    {
        Vector2 oldGrav = Physics2D.gravity;
        oldGrav *= -1;
        Physics2D.gravity = oldGrav;
    }
}