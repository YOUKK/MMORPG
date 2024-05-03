using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 싱글톤으로 구현된 managers가 있어서 이걸 컴포넌트로 만들 필요가 없어서 일반적인 c#파일로 만듦

public class InputManager
{
    public Action KeyAction = null; // 키 입력
    public Action<Define.MouseEvent> MouseAction = null; // 마우스 입력

    bool _pressed = false;

    // 모노비헤이비어 상속받지 않아서 직접 호출해야하니까 OnUpdate로 이름 바꿈??
    public void OnUpdate()
    {
        // UI를 클릭했는지 알 수 있다.
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
