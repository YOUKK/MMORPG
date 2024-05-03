using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Collision!");
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger!");
	}

	void Start()
    {
        
    }


	void Update()
    {
		if (Input.GetMouseButtonDown(0)) // 0은 마우스 왼쪽
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

			int mask = (1 << 8) | (1 << 9); // Monster 레이어, Wall 레이어

			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100.0f, mask))
			{
				Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
			}
		}
	}
}