using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Public

    public float followSpeed;

    public Transform target;

    public Vector3 offset;

    public float yPullLimit = 3f;
    public float YCameraLimit = 0f;
    public float XCameraLimit = 0f;
    public float smoothSpeed = 0.3f;
    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 new_position = target.position + offset;

        float yDelta = Mathf.Abs(target.position.y - transform.position.y);

        if ( yDelta > yPullLimit )
        {
            new_position.y = Mathf.Lerp( transform.position.y, target.position.y, 0.5f );
        }
        else
        {
            new_position.y = transform.position.y;
        }

        new_position.y = Mathf.Clamp( new_position.y, YCameraLimit, 100f );
        new_position.x = Mathf.Clamp( new_position.x, XCameraLimit, 100f );
        transform.position = new_position;
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    #endregion
}