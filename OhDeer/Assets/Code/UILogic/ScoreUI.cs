using UnityEngine;
using System.Collections;

public class ScoreUI : MonoBehaviour {

	private GUIText scoreText;

	void Awake()
	{
		scoreText = GetComponent<GUIText>();
	}	

	void Update()
	{
		scoreText.text = "Score: " + GameData.GAME_SCORE;
	}
}
