using UnityEngine;
using System.Collections;

public class FlowControl : MonoBehaviour {

	private ObstacleQueueControl obstacleQueueControl;
	private ScoreControl scoreControl;
	private TimeControl timeControl;
	private TreeTapControl treeTapControl;

	[SerializeField]
	private GameObject[] tapHereButtons;
	[SerializeField]
	private GameObject restartButton;
	private GUIText loseText;
	private GUIText highScore;
	
	private void Awake() 
	{
		obstacleQueueControl = GetComponent<ObstacleQueueControl>();
		scoreControl = GetComponent<ScoreControl>();
		timeControl = GetComponent<TimeControl>();
		treeTapControl = GetComponent<TreeTapControl>();
		loseText = GameObject.Find ("LoseText").GetComponent<GUIText>();
		highScore = GameObject.Find ("HighScoreText").GetComponent<GUIText>();
	}

	private void Start()
	{
		highScore.gameObject.SetActive(false);
		restartButton.SetActive(false);
		GameData.GAME_STATE = GameState.Start;
	}

	public void GameFail() 
	{
		GameData.GAME_STATE = GameState.Fail;
		highScore.gameObject.SetActive(true);
		restartButton.SetActive(true);

		scoreControl.CheckAndUpdateHighscore();
		timeControl.PauseTime();

		loseText.text = "Ouch! Try again!";
		highScore.text = "Your Highscore: " + GameData.HIGH_SCORE;
	}

	public void FirstGameStart() 
	{
		timeControl.ResumeTime();
		GameData.GAME_STATE = GameState.InProgress;
		foreach(GameObject button in tapHereButtons)
		{
			button.SetActive(false);
		}
	}

	public void GameRestart()
	{
		GameData.GAME_STATE = GameState.InProgress;
		highScore.gameObject.SetActive(false);

		scoreControl.ResetScore();
		timeControl.StartTime();
		obstacleQueueControl.ResetObtacleQueue();
		treeTapControl.ToggleObstaclesRowVisible();

		loseText.text = "";
	}
}
