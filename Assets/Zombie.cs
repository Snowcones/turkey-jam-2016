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

	private const int numCoins = 5;
	private const float coinScatter = 3.0f;
	private const float coinLife = 2.5f;

	private GameObject coin;
	private GameObject[] coins;
	private float[] coinLives;

    //public float delta = 1.5f; // Amount to move left & right from start point
    //public float speed = 2.0f;
    //Vector3 vel;
    //private Vector3 pos1 = new Vector3(-4, 0, 0);
    //private Vector3 pos2 = new Vector3(4, 0, 0);
    //public float speed = 1.0f;

      

    // Use this for initialization
    void Awake()
    {
        levelCleared = Resources.Load("level_cleared3") as Texture;
        zombie = GetComponent<AudioSource>();
        coin = Resources.Load("coin") as GameObject;
        coins = new GameObject[numCoins];
        coinLives = new float[numCoins];
    }

    // Update is called once per frame
    void Update()
    {  

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GetComponent<Animator>().SetTrigger()
        }

      //  if (Input.GetKeyDown(KeyCode.N)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
      //  if (SceneManager.GetActiveScene().buildIndex == 2); {
        //        if (gameObject.GetComponent<Rigidbody>() != null) {
          //      transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
                //vel = new Vector3(1, 0, 0);
                //transform.Translate(vel * speed * Time.deltaTime);
            //}
          updateCoins(Time.deltaTime);
               
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

            for(int c=0; c<numCoins; c++) {
           		coins[c] = Instantiate(coin);
           		coinLives[c] = coinLife + .2f * coinLife * Random.value;
           		Rigidbody rigidbody = coins[c].GetComponent<Rigidbody>();
           		rigidbody.position = transform.position;
           		rigidbody.velocity = new Vector3((2*Random.value-1)*coinScatter, 2*Random.value*coinScatter, 0);
            }

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
	        GUI.DrawTexture(new Rect(Vector2.zero, new Vector2(Screen.width, Screen.height)), levelCleared);
	       	Invoke("nextScene", 2.0f);
        }
    }

    void updateCoins(float dt) {
		for(int c=0; c<numCoins; c++) {
			if(coins[c] == null) {
				continue;
			}
			Rigidbody rigidbody = coins[c].GetComponent<Rigidbody>();
       		rigidbody.velocity -= new Vector3(0, 9.8f*dt, 0);
			if(rigidbody.position.y < transform.position.y - gameObject.GetComponent<BoxCollider>().bounds.size.y / 2 && rigidbody.velocity.y < 0) {
       			rigidbody.velocity = new Vector3(.5f*rigidbody.velocity.x, -.3f*rigidbody.velocity.y, rigidbody.velocity.z);
       		}
       		coinLives[c] -= dt;
       		if (coinLives[c] < 1) {
       			SpriteRenderer renderer = coins[c].GetComponent<SpriteRenderer>();
       			renderer.color = new Color(1.0f, 1.0f, 1.0f, coinLives[c]);
       		}
       		if (coinLives[c] < 0) {
       			Destroy(coins[c]);
       			coins[c] = null;
       		}
    	}
    }
}
