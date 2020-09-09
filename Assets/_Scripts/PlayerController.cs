using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // We use Serialize to make the variable appear in the Inspector
    // This allows us to change the variable at runtime 
    [SerializeField] private float _speed = 5f;

    // A Vector variable contains several floats bound to X, Y and Z: A Vector2 only has X & Y
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
        _moveVector.x = Input.GetAxisRaw("Horizontal");
        _moveVector.y = Input.GetAxisRaw("Vertical");

        // If there is any input, run the animation code
        if (_moveVector != Vector2.zero)
		{
            // Sets the animator parametres to be equal Player Input.
            _animator.SetFloat("Horizontal", _moveVector.x);
            _animator.SetFloat("Vertical", _moveVector.y);
            // Switch between Idle & Walking
            _animator.SetBool("isWalking", true);
		}
		else { _animator.SetBool("isWalking", false); }
		
        // If we are moving on the X axis, flip the player to face the correct way.
		if (_moveVector.x != 0f)
		{
            // Flips the Object - useful for shooting
            transform.localScale = new Vector2(_moveVector.x, 1f);
            
            // Flips just the sprite
            //_spriteRenderer.flipX = (_moveVector.x < 0) ? true : false;
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