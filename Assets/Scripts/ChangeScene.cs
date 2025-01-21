using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneChanger", menuName = "Utils/Scene Changer")]
public class ChangeScene : ScriptableObject
{
    public void GoTo( string sceneName )
    {
        SceneManager.LoadScene( sceneName );
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}