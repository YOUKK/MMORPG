using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
	public T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}


	// Object�� [����Ƽ�� ������ �� �ִ� ��� ��ü�� ���� Ŭ����]�̱� ������
	// ��ü ����/���� ��ɵ� Object Ŭ���� �ȿ� ���ǵǾ� �ִ�.

	// �� ����ϱ� ���ϰ� ����� �������̽��� Instantiate�޼ҵ带 ���� �����ϰ�(����)
	// �� �޼ҵ� ���ο��� ����Ƽ���� �����ϴ� Object�� Instantiate�� ȣ����
	public GameObject Instantiate(string path, Transform parent = null)
	{
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if(prefab == null)
		{
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}

		// Obejct.�� �� �ٿ��ָ� ResourceManager ���� Instantiate�� ��������� ȣ���ϱ� ����.
		return Object.Instantiate(prefab, parent);
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
