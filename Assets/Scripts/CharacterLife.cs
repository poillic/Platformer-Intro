using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CharacterLife : MonoBehaviour
{
    #region Public

    public float respawnTime = 1f;

    public UnityEvent OnPlayerDeath = new UnityEvent();

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
        if( collision.CompareTag("Checkpoint") )
        {
            respawnPosition = transform.position;
        }
        else if( collision.CompareTag("Death") )
        {
            StartCoroutine( Respawn() );
        }
    }

    #endregion

    #region Main Methods

    private IEnumerator Respawn()
    {
        OnPlayerDeath.Invoke();
        yield return new WaitForSeconds( respawnTime );
        transform.position = respawnPosition;
    }

    #endregion

    #region Private & Protected

    private Vector3 respawnPosition = Vector3.zero;

    #endregion
}