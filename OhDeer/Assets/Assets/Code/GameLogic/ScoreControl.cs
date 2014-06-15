using UnityEngine;
using System.Collections;

public class ScoreControl : MonoBehaviour 
{
	[SerializeField]
	private Transform leefObject;

	void Start () 
	{
		GameData.HIGH_SCORE = PlayerPrefs.GetInt("highScore");
		GameData.GAME_SCORE = 0;
	}

	public void AddPoint()
	{
		GameData.GAME_SCORE++;

		PlayLeef();
	}

	public void ResetScore ()
	{
		GameData.GAME_SCORE = 0;
	}

	private void PlayLeef()
	{
		if ( DeerState.Left == GameData.DEER_STATE )
		{
			leefObject.animation.Play("LeefFallFromLeft");
		}
		else 
		{
			leefObject.animation.Play("LeefFallFromRight");
		}
	}

	public void CheckAndUpdateHighscore()
	{
		if(GameData.GAME_SCORE > GameData.HIGH_SCORE)
		{
			GameData.HIGH_SCORE = GameData.GAME_SCORE;
			PlayerPrefs.SetInt("highScore", GameData.HIGH_SCORE);
			PlayerPrefs.Save();
		}
	}

	// debug stuff
	void Update ()
	{
		if ( Input.GetKeyDown( KeyCode.D) )
		{
			if ( GameData.DEER_STATE == DeerState.Left )
			{
				GameData.DEER_STATE = DeerState.Right;
				AddPoint();
			}
			else
			{
				GameData.DEER_STATE = DeerState.Left;
				AddPoint();
			}
		}
	}
}
