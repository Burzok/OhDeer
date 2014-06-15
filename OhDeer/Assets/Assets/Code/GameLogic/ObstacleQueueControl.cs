using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleQueueControl : MonoBehaviour 
{
	[SerializeField]
	private Transform obstacleType1;
	[SerializeField]
	private Transform obstacleType2;
	[SerializeField]
	private Transform obstacleType3;
	[SerializeField]
	private Transform obstacleType4;
	[SerializeField]
	private Transform obstacleType5;
	[SerializeField]
	private Transform obstacleType6;
	[SerializeField]
	private Transform obstacleType7;
	[SerializeField]
	private Transform obstacleType8;
	[SerializeField]
	private Transform obstacleType9;
	[SerializeField]
	private Transform obstacleType10;

	[SerializeField]
	private int emptyChance;
	[SerializeField]
	private int numberOfStartingObstacles;
	[SerializeField]
	private int changeDifficultyEvery;
	[SerializeField]
	private int changeDifficultyBy;

	private Queue obstacleQueue;
	private int chance;
	private int tapCounter;
	private int emptyObstacles;

	public Obstacle PeekObstacle (int index)
	{
		object[] tab = obstacleQueue.ToArray();

		return (Obstacle)tab[index];
	}

	public Obstacle GetObstacle ()
	{
		Obstacle obstacle = (Obstacle)obstacleQueue.Dequeue();
		tapCounter++;

		if ( tapCounter > changeDifficultyEvery)
		{
			emptyChance -= changeDifficultyBy;

			if ( emptyChance < 40 )
			{
				emptyChance = 40;
			}

			tapCounter = 0;
		}

		InsertObstacle();

		return obstacle;
	}

	void Awake() 
	{
		obstacleQueue = new Queue();

		emptyObstacles = 0;
		emptyChance = 60;
		numberOfStartingObstacles = 10;
		changeDifficultyEvery = 10;
		changeDifficultyBy = 10;
		tapCounter = 0;

		ResetObtacleQueue();
	}
	
	public void ResetObtacleQueue ()
	{
		obstacleQueue.Clear();
		
		obstacleQueue.Enqueue(Obstacle.Empty);
		obstacleQueue.Enqueue(Obstacle.Empty);
		
		for (int i = 0; i < numberOfStartingObstacles-2; i++)
		{
			InsertObstacle();
		}
	}

	void InsertObstacle ()
	{
		chance = Random.Range( 1, 101);

		if ( emptyObstacles > 3 )
		{
			chance += 20;
		}
		else if ( emptyObstacles > 5 )
		{
			chance += 40;
		}
		if ( chance <= emptyChance )
		{
			obstacleQueue.Enqueue(Obstacle.Empty);
		}
		else
		{
			chance = Random.Range( 0, 2);
			
			if ( 0 == chance )
			{
				obstacleQueue.Enqueue(Obstacle.Right);
			}
			else
			{
				obstacleQueue.Enqueue(Obstacle.Left);
			}
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown( KeyCode.F))
		{
			Debug.Log( "obstacle type: " + GetObstacle() );
		}

		if (Input.GetKeyDown( KeyCode.H))
		{
			Debug.Log( "obstacle type: " + PeekObstacle(0) );
			Debug.Log( "obstacle type: " + PeekObstacle(1) );
			Debug.Log( "obstacle type: " + PeekObstacle(2) );
			
		}
	}


}
