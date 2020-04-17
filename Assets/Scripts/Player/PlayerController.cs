using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	[SerializeField] LayerMask _layerMask;
	[SerializeField] GameObject _hookIndicator;
	[SerializeField] TextMeshProUGUI _hookText;

	[SerializeField] float _speed = 10;
    private bool _frozen;
	public bool FrozenState { get { return _frozen; } }

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
        _playerActions.PlayerAction.SecondAction.performed += CheckAction;
		_playerActions.PlayerAction.Pause.performed += _gameController.OnPause;
		_playerActions.PlayerAction.CloseMenu.performed += _gameController.OnCloseMenu;
		_playerActions.PlayerAction.SkipTime.started += _gameController.OnStartSkippingTime;
		_playerActions.PlayerAction.SkipTime.canceled += _gameController.OnStopSkippingTime;
		_animator = GetComponent<Animator>();

		_idleTimer = 0;
		_idle = false;

		_facingDirection = Vector3Int.down;
	}

	void Start()
    {
		_hookIndicator.SetActive(false);

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
        if (_frozen) move = Vector3.zero;
        // idle
        if ((y == 0 && x == 0 )||_frozen)
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

	public Vector3Int TileLookedPosition()
	{
		Vector3 center = transform.position;
		center.y += transform.localScale.y / 2;
		Vector3Int target = Vector3Int.FloorToInt(center);

		target += _facingDirection;
		return target;
	}

	// Checks the objects the player is facing and asks the GameController to handle the action of the player
	void CheckAction(InputAction.CallbackContext context)
	{

		Vector3 center = transform.position;
		center.y += transform.localScale.y / 2;
		Vector3Int target = Vector3Int.FloorToInt(center);

		target += _facingDirection;

		Vector3 vv = center + _facingDirection;
		RaycastHit2D hit = Physics2D.Raycast(center, vv, 1, _layerMask);

        Debug.Log(context.action.name);
		_gameController.CheckAction(target, hit, context.action.name== "SecondAction");
	}

	IEnumerator DisplayHookIndicator()
	{
		_hookIndicator.SetActive(true);
		for (float f = 0; f < 1; f += 0.1f)
		{
			_hookIndicator.transform.position = Vector3.Lerp(transform.position + (Vector3.up / 2), transform.position + Vector3.up * 2, f);
			yield return null;
		}
	}

	public void OnStartHooking(int rarity)
	{
		_hookText.text = "";
		for(int i = 0; i <= rarity; ++i)
		{
			_hookText.text += '!';
		}
		StartCoroutine("DisplayHookIndicator");
	}

	public void OnStopHooking()
	{
		_hookIndicator.SetActive(false);
	}
}
