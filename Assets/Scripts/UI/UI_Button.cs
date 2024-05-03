using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Type 쓰기 위해 추가함
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
		// enum타입 넘기기
		// enum은 Buttons인데 Button이라는 컴포넌트를 찾아가지고 해당하는 오브젝트를 매핑해 주세요
		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));
		Bind<GameObject>(typeof(GameObjects));
		Bind<Image>(typeof(Images));

		GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

		GameObject go = GetImage((int)Images.ItemIcon).gameObject;

		AddUIEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);
	}

	

	int _score = 0;

	// public으로 해주어야 UI에서 뜬다
    public void OnButtonClicked(PointerEventData data)
	{
		_score++;
		GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
	}
}
