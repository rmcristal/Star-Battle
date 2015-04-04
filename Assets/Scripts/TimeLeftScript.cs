using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLeftScript : MonoBehaviour {

	private Text timeLeft;
	private static int secondsRemaining = 60;

	public int SecondsRemaining
	{
		get
		{
			return secondsRemaining;
		}
		set
		{
			secondsRemaining = value;
		}
	}

	// Use this for initialization
	void Start () 
	{
		timeLeft = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (SecondsRemaining > 0)
			SecondsRemaining = 60 - (int)Time.time;
		else
			SecondsRemaining = 0;
		timeLeft.text = ("Seconds Remaining: " + SecondsRemaining);

	}
}
