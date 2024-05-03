using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
	public T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}


	// Object는 [유니티가 참조할 수 있는 모든 객체의 조상 클래스]이기 때문에
	// 객체 생성/삭제 기능도 Object 클래스 안에 정의되어 있다.

	// 더 사용하기 편리하게 비슷한 인터페이스로 Instantiate메소드를 직접 정의하고(래핑)
	// 이 메소드 내부에서 유니티에서 제공하는 Object의 Instantiate를 호출함
	public GameObject Instantiate(string path, Transform parent = null)
	{
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if(prefab == null)
		{
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}

		// Obejct.을 안 붙여주면 ResourceManager 내의 Instantiate를 재귀적으로 호출하기 때문.
		return Object.Instantiate(prefab, parent);
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
