using UnityEngine;
using UnityEngine.Events;

public class LockBlock : MonoBehaviour
{
    #region Public

    public UnityEvent OnOpen;
    public LevelInfo infos;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if ( collision.collider.CompareTag( "Player" ) )
        {
            if ( infos.keyPicked )
            {
                OnOpen.Invoke();
                Destroy( gameObject, .5f );
            }
        }
    }

    #endregion

    #region Main Methods



    #endregion

    #region Private & Protected

    #endregion
}