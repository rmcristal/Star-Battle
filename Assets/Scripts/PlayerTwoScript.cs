using UnityEngine;
using System.Collections;

public class PlayerTwoScript : TimeLeftScript {
	

	public GameObject testBlackHole0;
	private Rigidbody playerTwo;
	public int speed;
	private int thrustBoost = 1;
	private static int player2Points;
	public float gravitySlower;
	private int active = 1;
	private static float remainingThrustPlayer2 = 200f;
	public ParticleSystem crashExplosion;

	//This is the property for Player2Points
	public int Player2Points
	{
		get
		{
			return player2Points;
		}
		set
		{
			player2Points = value;
			Debug.Log ("Player 2 score: " + Player2Points);
		}
	}
	
	//This is the Property for RemainingThrustPlayer2
	public float RemainingThrustPlayer2
	{
		get
		{
			return remainingThrustPlayer2;
		}
		set
		{
			remainingThrustPlayer2 = value;
		}
	}
	
	
	
	// Use this for initialization
	void Start () 
	{
		playerTwo = GetComponent<Rigidbody> ();
		
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		
		float moveHorizontal = Input.GetAxis ("HorizontalPlayer2");
		float moveVertical = Input.GetAxis ("VerticalPlayer2");
		
		RemainingThrustPlayer2-= (Mathf.Abs (moveVertical) + Mathf.Abs (moveHorizontal));
		GameObject[] arrayOfBlackHoles = 
		{
			testBlackHole0
		};

		if (RemainingThrustPlayer2> 0) 
		{
			playerTwo.AddForce (new Vector3 (moveHorizontal, 0, moveVertical) * (speed + thrustBoost));
			//Debug.Log ("Remaining thrust: " + RemainingThrustPlayer2);
		} 
		else 
		{
			RemainingThrustPlayer2= 0;
		}
		
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Player2Points -= 25;
			Reset ();
		}


        for (int i = 0; i < arrayOfBlackHoles.Length; i++)
            if (Mathf.Abs(playerTwo.position.magnitude - arrayOfBlackHoles[i].transform.position.magnitude) < 250)
            {
                if (arrayOfBlackHoles[i].transform.childCount == 0 || SecondsRemaining <= 0)
                {
                    active = 0;
                    arrayOfBlackHoles[i].SetActive(false);
                    Debug.Log("Player 2 Score: " + Player2Points);
                    //Debug.Log ("Level completed in: " + Time.time);
                }
                else
                {
                    active = 1;
                }
                playerTwo.AddForce(new Vector3(arrayOfBlackHoles[i].transform.position.x - playerTwo.position.x, 0f, arrayOfBlackHoles[i].transform.position.z - playerTwo.position.z) * gravitySlower * active);
            }
			
			

	}
	
	void OnTriggerEnter(Collider theCollidingObject)
	{
		//Debug.Log (theCollidingObject.tag);
		if (theCollidingObject.tag == "BlackHole") 
		{
			StartCoroutine (CrashExplosion());
			Player2Points -= 100;
		} 
		else if (theCollidingObject.tag == "BigStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player2Points += 10;
			//Debug.Log ("Score: " + Player2Points);
			RemainingThrustPlayer2+= 10;
		}
		else if (theCollidingObject.tag == "MediumStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player2Points += 20;
			//Debug.Log ("Score: " + Player2Points);
			RemainingThrustPlayer2+= 20;
		}
		else if (theCollidingObject.tag == "SmallStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player2Points += 50;
			//Debug.Log ("Score: " + Player2Points);
			RemainingThrustPlayer2+= 50;
		}
		else 
		{
			StartCoroutine (CrashExplosion());
			Player2Points = Player2Points/2;
		}
		
	}
	IEnumerator CrashExplosion()
	{
		crashExplosion.transform.position = playerTwo.position;
		crashExplosion.Play();
		yield return new WaitForSeconds(.2f);
		Reset ();
		//Debug.Log (Player2Points);
	}
	private void Reset()
	{
		playerTwo.velocity = new Vector3(0f, 0f, 0f);
		playerTwo.rotation = Quaternion.Euler (0f, 0f, 0f);
		playerTwo.position = new Vector3(-5.6f, 0f, 184.2f);
		RemainingThrustPlayer2= 200f;
	}
	
	
	
	
	
}