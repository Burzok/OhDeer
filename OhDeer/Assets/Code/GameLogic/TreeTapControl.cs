using UnityEngine;
using System.Collections;
		
public class TreeTapControl : MonoBehaviour {

	private Touch touch;
	private BoxCollider2D tapCollider;
	private ObstacleQueueControl obstacleControl;
	private ScoreControl scoreControl;
	private FlowControl flowControl;
	private TimeControl timeControl;
	
	private GameObject deerLeft;
	private GameObject deerRight;

	private int tapCount;

	[SerializeField]
	private GameObject[] trees;
	[SerializeField]
	private GameObject[] obstaclesLeft;
	[SerializeField]
	private GameObject[] obstaclesRight;
	
	private void Awake() 
	{
		obstacleControl = GetComponent<ObstacleQueueControl>();
		scoreControl = GetComponent<ScoreControl>();
		timeControl = GetComponent<TimeControl>();
		flowControl = GetComponent<FlowControl>();
		deerLeft = GameObject.Find("DeerLeft");
		deerRight = GameObject.Find("DeerRight");
	}

	private void Start() 
	{
		deerRight.SetActive(false);
        deerLeft.SetActive(false);

		foreach (GameObject obstacle in obstaclesLeft)
			obstacle.SetActive(false);

		foreach (GameObject obstacle in obstaclesRight)
			obstacle.SetActive(false);
	}

	private void Update() 
	{
		if(Input.touchCount == 1)
		{
			if(GameData.GAME_STATE == GameState.Start)
			{
				touch = Input.GetTouch (0);
				if(touch.phase == TouchPhase.Began) 
				{
					flowControl.FirstGameStart(); 
					ToggleObstaclesRowVisible();
				}
			}
			if(GameData.GAME_STATE == GameState.InProgress)
			{
				touch = Input.GetTouch (0);
				if(touch.phase == TouchPhase.Began) 
				{
					if(touch.position.x < Screen.width/2 && touch.position.y < Screen.height*0.8) 
					{
						GameData.DEER_STATE = DeerState.Left;

						deerLeft.SetActive(true);
						deerRight.SetActive(false);
						trees[0].animation.Play("ShakingTreeLeft");

						if(obstacleControl.GetObstacle() == Obstacle.Left)
						{
							flowControl.GameFail();
						}
						else
						{
							scoreControl.AddPoint();
							timeControl.AddTime();
						}							
					}

					if(touch.position.x > Screen.width/2 && touch.position.y < Screen.height*0.8) 
					{
						GameData.DEER_STATE = DeerState.Right;
						deerLeft.SetActive(false);
						deerRight.SetActive(true);
						trees[1].animation.Play("ShakingTreeRight");
						timeControl.AddTime();

						if(obstacleControl.GetObstacle() == Obstacle.Right)
						{
							flowControl.GameFail();
						}
						else
						{
							scoreControl.AddPoint();
							timeControl.AddTime();
						}
					}
					ToggleObstaclesRowVisible();
				}
			}
		}
	}

	public void ToggleObstaclesRowVisible()
	{
		for(int index=0; index<3; index++)
		{
			ToggleObstaclesVisible(index);
		}
	}

	private void ToggleObstaclesVisible(int index)
	{
		if(obstacleControl.PeekObstacle(index) == Obstacle.Left)
		{
				obstaclesLeft[index].SetActive(true);
				obstaclesRight[index].SetActive(false);
		}
		if(obstacleControl.PeekObstacle(index) == Obstacle.Right)
		{
				obstaclesLeft[index].SetActive(false);
				obstaclesRight[index].SetActive(true);
		}
		if(obstacleControl.PeekObstacle(index) == Obstacle.Empty)
		{
				obstaclesLeft[index].SetActive(false);
				obstaclesRight[index].SetActive(false);
		}
	}
}