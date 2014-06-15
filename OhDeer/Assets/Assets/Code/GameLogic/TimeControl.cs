using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour 
{
	[SerializeField]
	private Transform bar;
	 
	private float gameTime;
	private bool playMode;

	private float maxScaleX;
	private float maxScaleY;
	private float maxScaleZ;
	private Vector3 moveVector;

	private FlowControl flowControl;

	void Awake()
	{
		flowControl = GetComponent<FlowControl>(); 
	}

	void Start()
	{
		maxScaleX = bar.transform.localScale.x;
		maxScaleY = bar.transform.localScale.y;
		maxScaleZ = bar.transform.localScale.z;
		playMode = false;
		gameTime = GameData.GAME_START_TIME;
		moveVector = new Vector3( maxScaleX, maxScaleY, maxScaleZ);

		StartTime();
		PauseTime();
	}

	void FixedUpdate ()
	{
		if ( playMode )
		{
			gameTime -= Time.deltaTime;

			if ( 0 >= gameTime )
			{
				GameOver();
			}
		}
	}

	public void StartTime()
	{
		gameTime = GameData.GAME_START_TIME;
		playMode = true;
	}

	public void ResumeTime()
	{
		playMode = true;
	}

	public void PauseTime()
	{
		playMode = false;
	}

	public void GameOver()
	{
		flowControl.GameFail();
	}

	public void AddTime()
	{
		gameTime += GameData.TAP_ADD_TIME;

		if ( GameData.GAME_MAX_TIME < gameTime )
		{
			gameTime = GameData.GAME_MAX_TIME;
		}
	}

	void Update()
	{
		moveBar();
	}

	private void moveBar ()
	{
		float procent = (gameTime * 100) / GameData.GAME_MAX_TIME ;

		float newScale = (maxScaleX * procent) / 100;

		moveVector.x = newScale;

		bar.localScale = moveVector;
	}
}
