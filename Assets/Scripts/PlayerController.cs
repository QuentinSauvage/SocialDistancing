using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float _speed = 10;
	Rigidbody2D _rigidbody2D;
	PlayerActions _playerActions;
	Animator _animator;
	Vector2 _movementAction;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_playerActions = new PlayerActions();
		_playerActions.PlayerMovement.Move.performed += ctx => _movementAction = ctx.ReadValue<Vector2>();
		_animator = GetComponent<Animator>();
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	void FixedUpdate()
	{
		float x = _movementAction.x * _speed * Time.deltaTime;
		float y = _movementAction.y * _speed * Time.deltaTime;
		Vector3 move = new Vector3(x, y, 0);

		if(y != 0)
		{
			_animator.SetFloat("MoveX", 0);
			_animator.SetFloat("MoveY", y);
		}
		else if(x != 0)
		{
			_animator.SetFloat("MoveX", x);
			_animator.SetFloat("MoveY", 0);
		}
		_animator.SetFloat("Speed", move.magnitude);
		Debug.Log(move.magnitude);
		move += transform.position;

		_rigidbody2D.MovePosition(move);
	}

	void OnEnable()
	{
		_playerActions.Enable();
	}

	void OnDisable()
	{
		_playerActions.Disable();
	}
}
