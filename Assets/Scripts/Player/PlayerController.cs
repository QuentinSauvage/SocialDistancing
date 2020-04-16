using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] LayerMask _layerMask;

	[SerializeField] float _speed = 10;
    private bool _frozen;

	Rigidbody2D _rigidbody2D;
	Animator _animator;

	GameController _gameController;

	// Inputs of the player
	PlayerActions _playerActions;
	// Receives the movement inputs from the player
	Vector2 _movementAction;
	// The direction the player is facing
	Vector3Int _facingDirection;

	
	// Time elapsed since the player is idle
	float _idleTimer;
	// Is the idle UI displayed?
	bool _idle;

	private void Awake()
	{
		_gameController = GameObject.Find("GameController").GetComponent<GameController>();

		_rigidbody2D = GetComponent<Rigidbody2D>();
		_playerActions = new PlayerActions();
		_playerActions.PlayerMovement.Move.performed += ctx => _movementAction = ctx.ReadValue<Vector2>();
		_playerActions.PlayerAction.DefaultAction.performed += CheckAction;
		_animator = GetComponent<Animator>();

		_idleTimer = 0;
		_idle = false;

		_facingDirection = Vector3Int.down;
	}

	void Start()
    {
        
    }

    void Update()
    {
		
    }

    public void Freeze()
    {
        _frozen = true;
    }

    public void UnFreeze()
    {
        _frozen = false;
    }

    void FixedUpdate()
    {


        float x = _movementAction.x * _speed * Time.deltaTime;
        float y = _movementAction.y * _speed * Time.deltaTime;
        Vector3 move = new Vector3(x, y, 0);

        // idle
        if ((y == 0 && x == 0 )|| _frozen)
        {
            if (!_idle)
            {
                _idleTimer += Time.deltaTime;
                if (_idleTimer > 2)
                {
                    _idle = true;
                    _gameController.DisplayTime();
                }
            }
        }
        else
        {
            if (_idle)
            {
                _idle = false;
                _gameController.HideTime();
            }
            //vertical move
            if (y != 0)
            {
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", y);
                _idleTimer = 0;

                if (y > 0)
                {
                    _facingDirection = Vector3Int.up;
                }
                else
                {
                    _facingDirection = Vector3Int.down;
                }
            }
            // horizontal move 
            else
            {
                _animator.SetFloat("MoveX", x);
                _animator.SetFloat("MoveY", 0);
                _idleTimer = 0;

                if (x > 0)
                {
                    _facingDirection = Vector3Int.right;
                }
                else
                {
                    _facingDirection = Vector3Int.left;
                }
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

	void CheckAction(InputAction.CallbackContext context)
	{

		Vector3 center = transform.position;
		center.y += transform.localScale.y / 2;
		Vector3Int target = Vector3Int.FloorToInt(center);

		target += _facingDirection;

		Vector3 vv = center + _facingDirection;
		RaycastHit2D hit = Physics2D.Raycast(center, vv, 1, _layerMask);

		_gameController.CheckAction(target, hit);
	}
}
