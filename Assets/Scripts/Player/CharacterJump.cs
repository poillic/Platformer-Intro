using UnityEngine;

[RequireComponent( typeof( Rigidbody2D ) )]
[RequireComponent( typeof( CharacterGround ) )]
public class CharacterJump : MonoBehaviour
{
    #region Public

    [Header( "Jumping Stats" )]
    [SerializeField, Range( 2f, 5.5f )] [Tooltip( "Maximum jump height" )] public float jumpHeight = 7.3f;
    [SerializeField, Range( 0.2f, 1.25f )] [Tooltip( "How long it takes to reach that height before coming back down" )] public float timeToJumpApex;
    [SerializeField, Range( 0f, 5f )] [Tooltip( "Gravity multiplier to apply when going up" )] public float upwardMovementMultiplier = 1f;
    [SerializeField, Range( 1f, 10f )] [Tooltip( "Gravity multiplier to apply when coming down" )] public float downwardMovementMultiplier = 6.17f;
    [SerializeField, Range( 1, 5 )] [Tooltip( "How many times can you jump in the air?" )] public int maxJumps = 2;

    [Header( "Options" )]
    [Tooltip( "Should the character drop when you let go of jump?" )] public bool variablejumpHeight;
    [SerializeField, Range( 1f, 10f )] [Tooltip( "Gravity multiplier when you let go of jump" )] public float jumpCutOff;
    [SerializeField] [Tooltip( "The fastest speed the character can fall" )] public float speedLimit;
    [SerializeField, Range( 0f, 0.3f )] [Tooltip( "How long should coyote time last?" )] public float coyoteTime = 0.15f;
    [SerializeField, Range( 0f, 0.3f )] [Tooltip( "How far from ground should we cache your jump?" )] public float jumpBuffer = 0.15f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<CharacterGround>();
        defaultGravityScale = 1f;
        gravMultiplier = defaultGravityScale;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        SetGravity();
        onGround = groundDetector.GetOnGround();

        if( Input.GetButtonDown( "Jump" ) )
        {
            desiredJump = true;
        }

        pressingJump = Input.GetButton( "Jump" );

        if( desiredJump )
        {
            jumpBufferTimer += Time.deltaTime;

            if( jumpBufferTimer >= jumpBuffer )
            {
                desiredJump = false;
                jumpBufferTimer = 0f;
            }
        }

    }

    private void FixedUpdate()
    {
        velocity = _rb2d.velocity;

        if( desiredJump )
        {
            Jump();
            _rb2d.velocity = velocity;
            return;
        }

        if(!currentlyJumping && !onGround )
        {
            coyoteTimeTimer += Time.deltaTime;
        }
        else
        {
            coyoteTimeTimer = 0f;
        }

        CalculateGravity();
    }

    #endregion

    #region Main Methods

    private void SetGravity()
    {
        Vector2 newGravity = new Vector2( 0, ( -2f * jumpHeight ) / ( timeToJumpApex * timeToJumpApex ) );
        _rb2d.gravityScale = ( newGravity.y / Physics2D.gravity.y ) * gravMultiplier;
    }

    private void Jump()
    {
        Vector2 new_velocity = _rb2d.velocity;

        if( ( onGround || coyoteTimeTimer < coyoteTime ) && jumpDone < maxJumps )
        {
            desiredJump = false;
            coyoteTimeTimer = 0f;
            jumpBufferTimer = 0f;

            jumpDone++;

            jumpSpeed = ( 2f * jumpHeight ) / timeToJumpApex;

            gravMultiplier = upwardMovementMultiplier;
            SetGravity();
            if ( velocity.y >= 0f )
            {
                jumpSpeed = Mathf.Max( jumpSpeed - new_velocity.y, 0f );
            }
            else if( velocity.y < 0f )
            {
                jumpSpeed += Mathf.Abs( _rb2d.velocity.y );
            }

            velocity.y += jumpSpeed;
            currentlyJumping = true;
        }
    }

    private void CalculateGravity()
    {
        if( _rb2d.velocity.y > 0.01f )
        {
            if( onGround )
            {
                gravMultiplier = defaultGravityScale;
            }
            else
            {
                if( variablejumpHeight )
                {
                    if( pressingJump && currentlyJumping )
                    {
                        gravMultiplier = upwardMovementMultiplier;
                    }
                    else
                    {
                        gravMultiplier = jumpCutOff;
                    }
                }
                else
                {
                    gravMultiplier = upwardMovementMultiplier;
                }
            }
        }
        else if( _rb2d.velocity.y <= 0f )
        {
            if( onGround )
            {
                gravMultiplier = defaultGravityScale;
                jumpDone = 0;
            }
            else
            {
                gravMultiplier = downwardMovementMultiplier;
            }
        }
        else
        {
            if( onGround )
            {
                currentlyJumping = false;
                jumpDone = 0;
            }
            gravMultiplier = defaultGravityScale;
        }
      
        _rb2d.velocity = new Vector3( _rb2d.velocity.x, Mathf.Clamp( _rb2d.velocity.y, -speedLimit, 100 ) );
    }

    #endregion

    #region Private & Protected

    private Rigidbody2D _rb2d;
    private CharacterGround groundDetector;

    [SerializeField] private  float jumpSpeed;
    private float defaultGravityScale;
    private float gravMultiplier = 1f;

    private bool desiredJump;
    private bool pressingJump;
    private bool currentlyJumping;
    private bool onGround = true;
    private int jumpDone = 0;

    private float coyoteTimeTimer = 0f;
    private float jumpBufferTimer = 0f;

    private Vector2 velocity;

    #endregion
}