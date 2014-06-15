using UnityEngine;
using System.Collections;

public class TapHereUI : MonoBehaviour
{
	[SerializeField]
	private bool startLeft;

	private void Start()
	{
		InvokeRepeating("Tick", 0.5f, 0.5f);
	}

	private void Tick()
	{
		if(startLeft)
		{
			transform.Translate(Vector3.left);				
		}
		else
		{
			transform.Translate(Vector3.right);
		}
		startLeft = !startLeft;
	}
}
