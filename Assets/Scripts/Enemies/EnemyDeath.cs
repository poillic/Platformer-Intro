using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    #region Public

    public UnityEvent OnDeath;
    private Collider2D[] colliders;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        colliders = GetComponents<BoxCollider2D>();
    }

    private void Update()
    {
        
    }

    #endregion

    #region Main Methods

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.collider.CompareTag("Player") )
        {
            if ( transform.position.y < collision.collider.transform.position.y )
            {
                Death();
            }
        }
    }

    public void Death()
    {
        OnDeath.Invoke();
        transform.Find( "Graphics" ).gameObject.SetActive( false );
        foreach ( var item in colliders )
        {
            item.enabled = false;
        }
        Destroy( gameObject, 1f );
    }

    #endregion

    #region Private & Protected

    #endregion
}