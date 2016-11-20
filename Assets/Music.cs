using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Music : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 0)
        { Destroy(this.gameObject); }
        
    }
}
