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

    public bool space_pressed = false;

    // Use this for initialization
    void Awake()
    {
        rg_bul = GetComponent<Rigidbody>();
        tf_bul = GetComponent<Transform>();
   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (space_pressed == false))
        {
            gun = GetComponent<AudioSource>();
            gun.Play();
            angle *= (Mathf.PI / 180);
            vel = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            rg_bul.velocity = (vel * speed);
            space_pressed = true;
            }
        

        if (Input.GetKeyDown(KeyCode.UpArrow) && (space_pressed ==false))
        {
            transform.Rotate(0, 0, 10);
            angle += 10;
           // vel += new Vector3((Mathf.Cos(10), Mathf.Sin(10),0));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && (space_pressed == false))
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
        	rg_bul.velocity = Vector3.Reflect(rg_bul.velocity, col.contacts[0].normal);
			float newAngle = Mathf.Atan2(rg_bul.velocity.y, rg_bul.velocity.x) * 180 / Mathf.PI;
			Debug.Log(newAngle);
			transform.eulerAngles = new Vector3(0, 0, newAngle);
        }
    }
}