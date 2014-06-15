using UnityEngine;
using System.Collections;

public class RestartButtonUI : MonoBehaviour {

	private FlowControl flowControl;

	void Awake()
	{
		flowControl = GameObject.Find("GameController").GetComponent<FlowControl>();
	}

	void Update()
	{
		if( Input.touchCount >= 1 )
		{
			Ray cursorRay = Camera.main.ScreenPointToRay( Input.GetTouch(0).position );
			RaycastHit hit;
			if( collider.Raycast( cursorRay, out hit, 1000.0f ) )
			{
				flowControl.GameRestart();
				gameObject.SetActive(false);
			}
		}
	}
}
