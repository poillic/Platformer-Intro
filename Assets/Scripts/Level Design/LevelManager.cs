using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Public

    public LevelInfo infos;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        infos.Init();
    }

    private void Update()
    {
        
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    #endregion
}