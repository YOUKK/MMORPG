using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers s_instance; // static으로 유일성이 보장된다.
	static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다.

	// 인풋매니저를 사용하고 싶으면 Managers.Input으로 부르면 된다
	InputManager _input = new InputManager();
	ResourceManager _resource = new ResourceManager();
	// 아래 게터에서 Instance를 통해 쓴 이유는 Instance에선 Init() 초기화를 해주기 때문
	public static InputManager Input { get { return Instance._input; } }
	public static ResourceManager Resource { get { return Instance._resource; } }

	void Start()
	{
		Init();
	}

	void Update()
	{
		// 인풋 체크를 Managers가 대표로 해준다.
		_input.OnUpdate();
	}

	static void Init()
	{
		if (s_instance == null)
		{
			GameObject go = GameObject.Find("@Managers");
			if (go == null)
			{
				go = new GameObject { name = "@Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);
			s_instance = go.GetComponent<Managers>();
		}
	}
}