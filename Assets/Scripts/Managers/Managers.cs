using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers s_instance; // static���� ���ϼ��� ����ȴ�.
	static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�.

	// ��ǲ�Ŵ����� ����ϰ� ������ Managers.Input���� �θ��� �ȴ�
	InputManager _input = new InputManager();
	ResourceManager _resource = new ResourceManager();
	// �Ʒ� ���Ϳ��� Instance�� ���� �� ������ Instance���� Init() �ʱ�ȭ�� ���ֱ� ����
	public static InputManager Input { get { return Instance._input; } }
	public static ResourceManager Resource { get { return Instance._resource; } }

	void Start()
	{
		Init();
	}

	void Update()
	{
		// ��ǲ üũ�� Managers�� ��ǥ�� ���ش�.
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