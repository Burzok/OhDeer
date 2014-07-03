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
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				flowControl.GameRestart();
				gameObject.SetActive(false);

			}
		}
	}
}
