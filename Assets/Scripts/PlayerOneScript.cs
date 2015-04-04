using UnityEngine;
using System.Collections;


public class PlayerOneScript : TimeLeftScript 
{
	
	private Rigidbody playerOne;
	public GameObject testBlackHole0;
	public int speed;
	private int thrustBoost = 1;
	protected static int player1Points;
	public float gravitySlower;
	private int active = 1;
	private static float remainingThrustPlayer1 = 200f;
	public ParticleSystem crashExplosion;
	

	//This is the property for Player1Points
	protected int Player1Points
	{
		get
		{
			return player1Points;
		}
		set
		{
			player1Points = value;
			Debug.Log ("Player 1 score: " + Player1Points);
		}
	}

	//This is the Property for RemainingThrustPlayer1
	protected float RemainingThrustPlayer1
	{
		get
		{
			return remainingThrustPlayer1;
		}
		set
		{
			remainingThrustPlayer1 = value;
		}
	}


	// Use this for initialization
	void Start () 
	{
		playerOne = GetComponent<Rigidbody> ();

	}



	// Update is called once per frame
	void Update () 
	{

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		RemainingThrustPlayer1 -= (Mathf.Abs (moveVertical) + Mathf.Abs (moveHorizontal));
		GameObject[] arrayOfBlackHoles = 
		{
			testBlackHole0
		};


		/*if (Input.GetKeyDown (KeyCode.Space)) 
		{
			thrustBoost = 20;
		} 
		else
			thrustBoost = 1;
		 */

		if (RemainingThrustPlayer1 > 0) 
		{
			playerOne.AddForce (new Vector3 (moveHorizontal, 0, moveVertical) * (speed + thrustBoost));
		} 
		else 
		{
			RemainingThrustPlayer1 = 0;
		}

		if (Input.GetKeyDown (KeyCode.Keypad0))
	    {
			Player1Points -= 25;
			Reset ();
		}


			for (int i = 0; i < arrayOfBlackHoles.Length; i++) {
				if (Mathf.Abs (playerOne.position.magnitude - arrayOfBlackHoles [i].transform.position.magnitude) < 250) {
					if (arrayOfBlackHoles [i].transform.childCount == 0 || SecondsRemaining <= 0) {
						active = 0;
						arrayOfBlackHoles [i].SetActive (false);
						Debug.Log ("Player 1 Score: " + Player1Points);
						//Debug.Log ("Level completed in: " + Time.time);
					} else {
						active = 1;
					}
					playerOne.AddForce (new Vector3 (arrayOfBlackHoles [i].transform.position.x - playerOne.position.x, 0f, arrayOfBlackHoles [i].transform.position.z - playerOne.position.z) * gravitySlower * active);
				}
		
			}


	}

	void OnTriggerEnter(Collider theCollidingObject)
	{
		
		if (theCollidingObject.tag == "BlackHole")
		{
			StartCoroutine (CrashExplosion());
			Player1Points -= 100;
		} 
		else if (theCollidingObject.tag == "BigStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player1Points += 10;
			//Debug.Log ("Score: " + Player1Points);
			RemainingThrustPlayer1 += 10;
		}
		else if (theCollidingObject.tag == "MediumStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player1Points += 20;
			//Debug.Log ("Score: " + Player1Points);
			RemainingThrustPlayer1 += 20;
		}
		else if (theCollidingObject.tag == "SmallStars") 
		{
			Destroy (theCollidingObject.gameObject);
			Player1Points += 50;
			//Debug.Log ("Score: " + Player1Points);
			RemainingThrustPlayer1 += 50;
		}
		else 
		{
			StartCoroutine (CrashExplosion());
			Player1Points = Player1Points/2;
		}

	}
	IEnumerator CrashExplosion()
	{
		crashExplosion.transform.position = playerOne.position;
		crashExplosion.Play();
		yield return new WaitForSeconds(.2f);
		Reset ();
	}
		
	private void Reset()
	{
		playerOne.velocity = new Vector3(0f, 0f, 0f);
		playerOne.rotation = Quaternion.Euler (0f, 0f, 0f);
		playerOne.position = new Vector3(2f, 0f, -50f);
		RemainingThrustPlayer1 = 200f;
	}
}



	
	

