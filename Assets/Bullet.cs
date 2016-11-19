using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private Rigidbody rg_bul;
    private Transform tf_bul;
    public float speed = 6f;
    public Vector3 vel;
    public float angle = 0;
    public AudioSource gun;

    // Use this for initialization
    void Awake()
    {
        rg_bul = GetComponent<Rigidbody>();
        tf_bul = GetComponent<Transform>();
        gun = GetComponent<AudioSource>();
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gun.Play();
            angle *= (Mathf.PI / 180);
            vel = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            rg_bul.velocity = (vel * speed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 10);
            angle += 10;
           // vel += new Vector3((Mathf.Cos(10), Mathf.Sin(10),0));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Rotate(0, 0, -10);
            angle += (-10);
          
        }

        //vel.Normalize();
    }

   // void 
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.gameObject.name == "Box1")
        {
            angle += 45;
            rg_bul.velocity = speed * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
 
            //transform.Rotate(0, 0, 45);
        }
    }
}