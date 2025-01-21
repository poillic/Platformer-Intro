using UnityEngine;
using UnityEngine.Events;

public class Checkpoints : MonoBehaviour
{
    #region Public

    public UnityEvent OnCheckpoint = new UnityEvent();

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag("Player") )
        {
            OnCheckpoint.Invoke();
        }
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    #endregion
}