using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	public float posX;
	public float posY;
	public float posZ;
	public Vector3 mousePos;

	public Vector3 wrld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));
	public float halfsz;
	// Use this for initialization
	void Start () {
		halfsz = gameObject.GetComponent<Renderer>().bounds.size.x/2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		posX = transform.position.x;
		posY = transform.position.y;
		posZ = transform.position.z;

		if (gameObject.transform.position.x > (wrld.x - halfsz)) {
			posX = wrld.x - halfsz;
		}
		if (gameObject.transform.position.y > (wrld.y - halfsz)) {
			posY = wrld.y - halfsz;
		}
	}


	void OnMouseDrag() {
		Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		point.z = gameObject.transform.position.z;

		gameObject.transform.position = point;
	}
	//void OnCollisionEnter(Collision col){
	//	if (col.gameObject.name == "Bullet") {
	//GetComponent<Rigidbody>().velocity = Vector3.zero; }

}


