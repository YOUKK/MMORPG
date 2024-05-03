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
        //Managers.Input.KeyAction -= OnKeyBoard; // �ٸ����� �����ϰ� �ִٸ� ����ϰ� �ٽ� ��û
        // �� �ڵ带 �ۼ����� ������ �̺�Ʈ�� 2���� ȣ��� �� ����
        //Managers.Input.KeyAction += OnKeyBoard; // ��ǲ üũ ���� ��û
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

		//Managers.Resource.Instantiate("UI/UI_Button");
    }

    void UpdateDie()
	{
		// �ƹ��͵� ����
	}

    void UpdateMoving()
	{
		Vector3 dir = _desPos - transform.position; // ����-���ʹ� 0�� �� �� ����.
		if (dir.magnitude < 0.0001f)
		{
			//_moveToDest = false; // ���������ϱ� false
			_state = PlayerState.Idle;

		}
		else // ���� �� ������ �� �̵��ؾ���
		{
			float moveDist = Mathf.Clamp(speed * Time.deltaTime, 0, dir.magnitude);
			transform.position += dir.normalized * moveDist;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
			transform.LookAt(_desPos);
		}

		// �ִϸ��̼�
		Animator anim = GetComponent<Animator>();
		// ���� ���� ���¿� ���� ������ �Ѱ��ش�
		anim.SetFloat("speed", speed);
	}

    void UpdateIdle()
	{
		// �ִϸ��̼�
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
	//		// ���忡���� ���� ������
	//		//transform.rotation = Quaternion.LookRotation(Vector3.forward);

	//		// �ε巴�� ȸ���ϱ�
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
