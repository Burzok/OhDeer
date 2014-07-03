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
	private GUIText score;

	private GameObject bearHead;
	private GameObject timeBar;
	private GameObject timeBarBackground;
	private GameObject deerLogo;
    private GameObject deerLeft;
    private GameObject leafParticles;

	
	private void Awake() 
	{
		obstacleQueueControl = GetComponent<ObstacleQueueControl>();
		scoreControl = GetComponent<ScoreControl>();
		timeControl = GetComponent<TimeControl>();
		treeTapControl = GetComponent<TreeTapControl>();
		loseText = GameObject.Find ("LoseText").GetComponent<GUIText>();
		highScore = GameObject.Find ("HighScoreText").GetComponent<GUIText>();
		score = GameObject.Find ("ScoreText").GetComponent<GUIText>();
		bearHead = GameObject.Find("bearHead");
		timeBar = GameObject.Find("timeBar");
		timeBarBackground = GameObject.Find("timeBarBackground");
		deerLogo = GameObject.Find("OhDeerLogo");
        deerLeft = GameObject.Find("DeerLeft");
        leafParticles = GameObject.Find("LeafParticles");
	}

	private void Start()
	{
		score.gameObject.SetActive(false);
		highScore.gameObject.SetActive(false);
		restartButton.SetActive(false);
		bearHead.SetActive(false);
		timeBar.SetActive(false);
		timeBarBackground.SetActive(false);
		foreach(GameObject button in tapHereButtons)
		{
			button.SetActive(false);
		}
		GameData.GAME_STATE = GameState.Menu;
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

	public void GoToStartState()
	{
		GameData.GAME_STATE = GameState.Start;
		foreach(GameObject button in tapHereButtons)
		{
			button.SetActive(true);
		}
		bearHead.SetActive(true);
		timeBar.SetActive(true);
		timeBarBackground.SetActive(true);
		score.gameObject.SetActive(true);
		deerLogo.SetActive(false);
        leafParticles.SetActive(false);
        deerLeft.SetActive(true);

        TwitterConnection twitter = GetComponent<TwitterConnection>();
        twitter.PostTweet();
            
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
