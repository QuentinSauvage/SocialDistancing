using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float _speed = 10;
	PlayerActions _playerActions;
	Vector2 _movementAction;

	private void Awake()
	{
		_playerActions = new PlayerActions();
		_playerActions.PlayerMovement.Move.performed += ctx => _movementAction = ctx.ReadValue<Vector2>();
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
		Debug.Log(move);
		transform.position += move;
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
