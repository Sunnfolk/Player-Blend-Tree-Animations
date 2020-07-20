using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // We use Serialize to make the variable appear in the Inspector
    // This allows us to change the variable at runtime 
    [SerializeField] private float _speed = 5f;

    private float _xMove = 0f;
    private float _yMove = 0f;
    private Vector2 _moveVector;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody2D;

    void Start ()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        // Fetch the Raw input from our Movement Axis (-1 to 1)
        _xMove = Input.GetAxisRaw("Horizontal");
        _yMove = Input.GetAxisRaw("Vertical");

        // Combine the input data into one Vector
        _moveVector = new Vector2(_xMove, _yMove);

        if (_moveVector != Vector2.zero)
		{
            _animator.SetFloat("Horizontal", _xMove);
            _animator.SetFloat("Vertical", _yMove);
            _animator.SetBool("isWalking", true);
		}
		else { _animator.SetBool("isWalking", false); }
		
		if (_xMove != 0f)
		{
            // Flips the Object - useful for shooting
            transform.localScale = new Vector2(_xMove, 1f);
            
            // Flips just the sprite
            _spriteRenderer.flipX = (_xMove < 0) ? true : false;
		}
    }
    void FixedUpdate()
    {
        // If the Magnitude of the two movement vectors are above 1
        if (_moveVector.magnitude > 1)
        {
            // Remove additional Diagonal speed by normalizing the movement vector (Setting the magnitude back to 1)
            _moveVector = _moveVector.normalized;
        }
        // Set our velocity to be equal to the _moveVector multiplied with our movement speed;
        _rigidBody2D.velocity = _moveVector * _speed;
    }
}