using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Type ���� ���� �߰���
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
	
	enum Buttons
	{
		PointButton,
	}

	enum Texts
	{
		PointText,
		ScoreText,
	}

	enum GameObjects
	{
		TestObject,
	}

	enum Images
	{
		ItemIcon,
	}

	private void Start()
	{
		// enumŸ�� �ѱ��
		// enum�� Buttons�ε� Button�̶�� ������Ʈ�� ã�ư����� �ش��ϴ� ������Ʈ�� ������ �ּ���
		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));
		Bind<GameObject>(typeof(GameObjects));
		Bind<Image>(typeof(Images));

		GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

		GameObject go = GetImage((int)Images.ItemIcon).gameObject;

		AddUIEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
	}

	

	int _score = 0;

	// public���� ���־�� UI���� ���
    public void OnButtonClicked(PointerEventData data)
	{
		_score++;
		GetText((int)Texts.ScoreText).text = $"���� : {_score}";
	}
}
