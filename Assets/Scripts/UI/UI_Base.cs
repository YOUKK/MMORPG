using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
	// 타입 별로 관리하기 위해 dictionary로 만듦
	// Type이 Button이면 UnityEngine.Object[]에 Button컴포넌트를 가진 오브젝트가 담겨져있다
	// Object는 최상위 부모. 모든 타입을 Object로 저장할 수 있다
	Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();


	// T : 컴포넌트 이름 -> 딕셔너리의 key로 쓰기 위해 사용
	// type : enum 이름 -> enum의 요소를 string[]으로 쓰기 위해 사용
	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		// 1. 딕셔너리에 저장공간 넣기
		// enum을 string으로 뽑아올 수 있음
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

		// 2. 하나씩 돌면서 찾기
		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed to bind {names[i]}");
		}
	}

	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (!(_objects.TryGetValue(typeof(T), out objects)))
			return null;

		return objects[idx] as T;
	}

	protected Text GetText(int idx)
	{
		return Get<Text>(idx);
	}

	protected Button GetButton(int idx)
	{
		return Get<Button>(idx);
	}

	protected Image GetImage(int idx)
	{
		return Get<Image>(idx);
	}

	public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
		}

		evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
	}
}
