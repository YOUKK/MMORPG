using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �̱������� ������ managers�� �־ �̰� ������Ʈ�� ���� �ʿ䰡 ��� �Ϲ����� c#���Ϸ� ����

public class InputManager
{
    public Action KeyAction = null; // Ű �Է�
    public Action<Define.MouseEvent> MouseAction = null; // ���콺 �Է�

    bool _pressed = false;

    // �������̺�� ��ӹ��� �ʾƼ� ���� ȣ���ؾ��ϴϱ� OnUpdate�� �̸� �ٲ�??
    public void OnUpdate()
    {
        // UI�� Ŭ���ߴ��� �� �� �ִ�.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction != null)
		{
			if (Input.GetMouseButton(0))
			{
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
			}
			else
			{
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
			}
		}
    }
}
