using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 10.0f;

	Vector3 _desPos;

    public enum PlayerState
	{
        Die,
        Moving,
        Idle,
        Channeling,
        Jumping,
        Falling,
	}

    PlayerState _state = PlayerState.Idle;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyBoard; // 다른데서 구독하고 있다면 취소하고 다시 신청
        // 위 코드를 작성하지 않으면 이벤트가 2번씩 호출될 수 있음
        //Managers.Input.KeyAction += OnKeyBoard; // 인풋 체크 구독 신청
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

		//Managers.Resource.Instantiate("UI/UI_Button");
    }

    void UpdateDie()
	{
		// 아무것도 못함
	}

    void UpdateMoving()
	{
		Vector3 dir = _desPos - transform.position; // 벡터-벡터는 0이 될 수 없음.
		if (dir.magnitude < 0.0001f)
		{
			//_moveToDest = false; // 도착했으니까 false
			_state = PlayerState.Idle;

		}
		else // 도착 안 했으니 더 이동해야함
		{
			float moveDist = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
			transform.LookAt(_desPos);
		}

		// 애니메이션
		Animator anim = GetComponent<Animator>();
		// 현재 게임 상태에 대한 정보를 넘겨준다
		anim.SetFloat("speed", speed);
	}

    void UpdateIdle()
	{
		// 애니메이션
		Animator anim = GetComponent<Animator>();
		anim.SetFloat("speed", 0);
	}

    void Update()
    {
		switch (_state)
		{
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
	}

	//void OnKeyBoard()
	//{
	//	if (Input.GetKey(KeyCode.W))
	//	{
	//		// 월드에서의 각도 설정임
	//		//transform.rotation = Quaternion.LookRotation(Vector3.forward);

	//		// 부드럽게 회전하기
	//		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
	//		transform.position += Vector3.forward * Time.deltaTime * speed;
	//	}
	//	if (Input.GetKey(KeyCode.S))
	//	{
	//		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
	//		transform.position += Vector3.back * Time.deltaTime * speed;
	//	}
	//	if (Input.GetKey(KeyCode.A))
	//	{
	//		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
	//		transform.position += Vector3.left * Time.deltaTime * speed;
	//	}
	//	if (Input.GetKey(KeyCode.D))
	//	{
	//		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
	//		transform.position += Vector3.right * Time.deltaTime * speed;
	//	}

	//	_moveToDest = false;
	//}

	void OnMouseClicked(Define.MouseEvent evt)
	{
		//if (evt != Define.MouseEvent.Click)
		//    return;

		if (_state == PlayerState.Die)
			return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _desPos = hit.point;
			//_moveToDest = true;
			_state = PlayerState.Moving;
        }
    }
}
