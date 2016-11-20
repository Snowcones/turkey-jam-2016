using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Zombie : MonoBehaviour
{
    public string animation;
    public bool isDead = false;
    public AudioSource zombie;
    private bool level_cleared = false;
    private Texture levelCleared;

    public float delta = 1.5f; // Amount to move left & right from start point
    public float speed = 2.0f;
    private Vector3 startPos;

    // Use this for initialization
    void Awake()
    {
        levelCleared = Resources.Load("level_cleared3") as Texture;
        zombie = GetComponent<AudioSource>();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

        Vector3 v = startPos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }


    bool AllZombiesDead() {
		GameObject[] zombieGameObjs = GameObject.FindGameObjectsWithTag("zombie");
		Zombie[] zombies = new Zombie[zombieGameObjs.Length];

		for(int i=0; i<zombieGameObjs.Length; i++) {
			zombies[i] = zombieGameObjs[i].GetComponent<Zombie>();
		}

		for(int i=0; i<zombieGameObjs.Length; i++) {
			if(!zombies[i].isDead) {
				return false;
			}
		}
		return true;
	}

	void nextScene() {
		int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
		if(nextScene < SceneManager.sceneCountInBuildSettings) {
            //Load the next level
            SceneManager.LoadScene(nextScene);

      	} else {
			//At end of game
            Debug.Log(SceneManager.sceneCount);
    	}
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet")
        {  //Destroy(gameObject);
            zombie.Play();
            GetComponent<Animator>().SetTrigger(animation);
            isDead = true;

            //Checks to see if the level is cleared
            if(AllZombiesDead()) {
            	Invoke("showCleared", .5f);
            }
        }
    }

    void showCleared() {
		level_cleared = true;
    }

    void OnGUI() {
    	if(level_cleared) {
    		Debug.Log("Much GUI");
	        GUI.DrawTexture(new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height)), levelCleared);
	        Invoke("nextScene", 2.0f);
        }
    }
}
