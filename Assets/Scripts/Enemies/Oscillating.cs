using UnityEngine;

public class Oscillating : MonoBehaviour
{
    #region Public

    public float moveSpeed = 1f;
    public float oscillationHeight = 2f;
    public float oscillationSpeed = 2f;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2( -moveSpeed, Mathf.Sin( Time.time * oscillationSpeed ) * oscillationHeight );

        if( _rb2d != null )
        {
            _rb2d.MovePosition( _rb2d.position + movement * Time.deltaTime );
        }
        else
        {
            transform.Translate( movement * Time.deltaTime );
        }
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    private Rigidbody2D _rb2d;

    #endregion
}