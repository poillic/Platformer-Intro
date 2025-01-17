using UnityEngine;
using UnityEngine.Events;

public class Coins : MonoBehaviour
{
    #region Public

    public UnityEvent OnPickUp;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag("Player") )
        {
            OnPickUp.Invoke();
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            Destroy( gameObject, 1f );
        }
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    #endregion
}