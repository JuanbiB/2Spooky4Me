using UnityEngine;
//using UnityEditor;
using System.Collections;

public class Character : MonoBehaviour
{
    private float clock;
    GameObject colFolder;
    private int index=1;
    private float trailTime; //For the length of trail and time trailColliders exist
	private bool item = false; 
	bool right = true;

    void Start()
    {
        trailTime = 4.0f;
        this.gameObject.GetComponent<SpriteRenderer>().receiveShadows = true;
        this.gameObject.GetComponent<Renderer>().receiveShadows = true;
        this.GetComponent<SpriteRenderer>().receiveShadows = true;
        this.GetComponent<Renderer>().receiveShadows = true;
        
		TrailRenderer trail = this.gameObject.AddComponent<TrailRenderer>();
        trail.startWidth = 0.2f;
        trail.endWidth = 1f;
        trail.time = trailTime;
		//trail.material = AssetDatabase.GetBuiltinExtraResource<Material> ("Default-Particle.mat");
	
        colFolder = new GameObject();
        colFolder.name = "trailColliders";

        

        InvokeRepeating("spawnCollider", 0.01f, 0.1f);
        
        /*mat = GetComponent<Renderer>().material;
        mat.mainTexture = Resources.Load<Texture2D>("tileWall");   //in case we want to add a texture for testing
        mat.color = new Color(1, 1, 1);                                         
        mat.shader = Shader.Find("Sprites/Default");*/
    }

    void spawnCollider()
    {
        GameObject colObject = new GameObject();
        colObject.transform.parent = colFolder.transform;
		colObject.name =  index.ToString();
        index++;
        colObject.transform.position = this.gameObject.transform.position;
        BoxCollider col = colObject.AddComponent<BoxCollider>();
        col.size = new Vector2(1f, 1f);
        col.isTrigger = true;
		colObject.tag = "Scent";

        StartCoroutine(destroyCollider(colObject,trailTime));
    }
    IEnumerator destroyCollider(GameObject col, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(col);
    }

//	void OnCollisionEnter(Collision coll){
//		this.GetComponent<Rigidbody>().velocity = Vector3.zero; 
//	}

    // Update is called once per frame
    void Update()
    {
		// Direction toon's facing
		if (right == true)
		{
			transform.eulerAngles = new Vector3(45, 0, 0);
		}
		else
		{
			transform.eulerAngles = new Vector3(-45, 180, 0);
		}

		transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		int speed = 3;

		// Moving 'Upwards' by modifing z-value +
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.localPosition += new Vector3(0, 0, 1) * speed * Time.deltaTime;
		}
		// Moving 'Downwards' by modifing z-value - 
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.localPosition += new Vector3(0, 0, -1) * speed * Time.deltaTime;
		}

		// Moving Right 
		if (Input.GetKey(KeyCode.RightArrow))
		{
			right = true;
			transform.localPosition += new Vector3(1, 0, 0) * speed * Time.deltaTime;
			//this.GetComponent<Rigidbody>().velocity = transform.right * Time.deltaTime * 300;
		}

		// Move Left 
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			right = false;
			transform.localPosition += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
		}
    }

	void gotSteak(){
		item = true;
		GameObject GameManager = GameObject.FindGameObjectWithTag ("Game Controller");
		GameManager.SendMessage("UpdateGUI", "Steak");
	}

	void useSteak(){
		item = false;
		GameObject GameManager = GameObject.FindGameObjectWithTag ("Game Controller");
		GameManager.SendMessage("UpdateGUI", "Nothing");
	}
}
