using UnityEngine;
using System.Collections;

public class Text1 : MonoBehaviour {
    public GameObject TextObj;
    public float time;

	// Use this for initialization
	void Awake () {
        gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if (time<0)
        {gameObject.SetActive(false); }
	
	}
}
