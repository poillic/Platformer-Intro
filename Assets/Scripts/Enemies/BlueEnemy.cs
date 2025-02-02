using UnityEngine;

public class BlueEnemy : MonoBehaviour
{
    #region Public

    public float moveSpeed = 2f;
    public LayerMask groundLayer;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        transform.localScale = new Vector3( -direction.x, 1f, 1f );
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast( transform.position, direction, 0.5f, groundLayer );
        if ( hit.collider != null )
        {
            direction *= -1f;
            transform.localScale = new Vector3( -direction.x, 1f, 1f );
        }

        Vector3 vel = direction * moveSpeed;
        vel.y = _rb2d.velocity.y;

        _rb2d.velocity = vel;
    }

    #endregion

    #region Main Methods

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay( transform.position, direction * 0.5f );
    }

    #endregion

    #region Private & Protected

    private Rigidbody2D _rb2d;
    private Vector2 direction = Vector2.right;

    #endregion
}