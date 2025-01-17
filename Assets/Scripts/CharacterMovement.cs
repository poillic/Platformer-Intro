using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent( typeof( CharacterGround ) )]
public class CharacterMovement : MonoBehaviour
{
    #region Public

    [Header( "Movement Parameters" )]
    [SerializeField, Range( 0f, 20f )][Tooltip( "Maximum movement speed" )] private float maxSpeed = 10f;
    [Tooltip( "When false, the charcter will skip acceleration and deceleration and instantly move and stop" )] public bool useAcceleration = true;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to reach max speed" )] public float maxAcceleration = 52f;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to stop after letting go" )] public float maxDecceleration = 52f;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to stop when changing direction" )] public float maxTurnSpeed = 80f;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to reach max speed when in mid-air" )] public float maxAirAcceleration;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to stop in mid-air when no direction is used" )] public float maxAirDeceleration;
    [SerializeField, Range( 0f, 100f )] [Tooltip( "How fast to stop when changing direction when in mid-air" )] public float maxAirTurnSpeed = 80f;
    [SerializeField] [Tooltip( "Friction to apply against movement on stick" )] private float friction;

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        groundDetector = GetComponent<CharacterGround>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        direction = Input.GetAxis( "Horizontal" );
    }

    private void FixedUpdate()
    {
        onGround = groundDetector.GetOnGround();
        Vector2 new_velocity = _rb2d.velocity;
        Vector2 maxVelocity = new Vector2( direction, 0f ) * Mathf.Max( maxSpeed, 0f );

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        deceleration = onGround ? maxDecceleration : maxAirDeceleration;
        turnSpeed = onGround ? maxTurnSpeed : maxAirTurnSpeed;

        if( direction != 0f )
        {
            if ( Mathf.Sign( direction ) != Mathf.Sign( new_velocity.x ) )
            {
                maxSpeedChange = turnSpeed * Time.deltaTime;
            }
            else
            { 
                maxSpeedChange = acceleration * Time.deltaTime;
            }
        }
        else
        {
            maxSpeedChange = deceleration * Time.deltaTime;
        }

        new_velocity.x = Mathf.MoveTowards( new_velocity.x, maxVelocity.x, maxSpeedChange );
        _rb2d.velocity = new_velocity;
    }

    #endregion

    #region Main Methods

    public bool IsMoving() { return direction != 0f; }

    public int MoveDirection() { return direction != 0 ? (int) Mathf.Sign(direction) : 0; }

    #endregion

    #region Private & Protected

    private Rigidbody2D _rb2d;
    private float direction = 0f;
    private float maxSpeedChange;
    private float acceleration;
    private float deceleration;
    private float turnSpeed;
    private bool onGround = true;
    private CharacterGround groundDetector;

    #endregion
}