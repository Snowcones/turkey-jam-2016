using UnityEngine;
using System.Collections;

public class pacing : MonoBehaviour
{

    /* public Transform farEnd;
     private Vector3 from = new Vector3(-4.65f,-1.95f, 0);
     private Vector3 to = new Vector3(-4.65f, 3f, 0);
     private float secondsForOneLength = 20f; */

    private bool dirRight = true;
    public float speed = 2.0f;

    void Update()
    {
        if (dirRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(-Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 4.0f)
        {
            dirRight = false;
        }

        if (transform.position.x <= -4)
        {
            dirRight = true;
        }

        // Use this for initialization
        //void Start () {
    }
}
