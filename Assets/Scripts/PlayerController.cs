using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float _speed = 10;
	Rigidbody2D _rigidbody2D;
	Animator _animator;

	// Inputs of the player
	PlayerActions _playerActions;
	// Receives the movement inputs from the player
	Vector2 _movementAction;
	
	// Time elapsed since the player is idle
	float _idleTimer;
	// Is the idle UI displayed?
	bool _idle;

	TimeManager _timeManager;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_playerActions = new PlayerActions();
		_playerActions.PlayerMovement.Move.performed += ctx => _movementAction = ctx.ReadValue<Vector2>();
		_animator = GetComponent<Animator>();

		_idleTimer = 0;
		_idle = false;

		GameObject gameController = GameObject.Find("GameController");
		_timeManager = gameController.GetComponent<TimeManager>();
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

		// idle
		if(y == 0 && x == 0)
		{
			if(!_idle)
			{
				_idleTimer += Time.deltaTime;
				if(_idleTimer > 2)
				{
					_idle = true;
					_timeManager.DisplayTime();
				}
			}
		}
		else
		{
			if(_idle)
			{
				_idle = false;
				_timeManager.HideTime();
			}
			//vertical move
			if (y != 0)
			{
				_animator.SetFloat("MoveX", 0);
				_animator.SetFloat("MoveY", y);
				_idleTimer = 0;
			}
			// horizontal move 
			else
			{
				_animator.SetFloat("MoveX", x);
				_animator.SetFloat("MoveY", 0);
				_idleTimer = 0;
			}
		}
		_animator.SetFloat("Speed", move.magnitude);
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
