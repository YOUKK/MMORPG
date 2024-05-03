using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기능 함수 모음집
public class Util
{
	public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
	{
		T component = go.GetComponent<T>();
		if (component == null)
			component = go.AddComponent<T>();
		return component;
	}

	public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
	{
		// 모든 게임오브젝트는 Transform을 가지기 때문에 Transform을 return해준다
		Transform transform = FindChild<Transform>(go, name, recursive);

		if (transform == null)
			return null;
			
		return transform.gameObject;
	}

	// 최상위 객체 go의 모든 자식 객체 훑는 함수
	public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
	{
		// go는 최상위 객체
		if (go == null)
			return null;

		// 직속 자식만 찾기
		if (!recursive)
		{
			for (int i = 0; i < go.transform.childCount; i++)
			{
				Transform transform = go.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || transform.name == name)
				{
					T component = transform.GetComponent<T>();
					if (component != null)
						return component;
				}
					
			}
		}
		else
		{
			foreach(T component in go.GetComponentsInChildren<T>())
			{
				if (string.IsNullOrEmpty(name) || component.name == name)
					return component;
			}
		}

		return null;
	}
}
