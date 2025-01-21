using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Utils/LevelInfos")]
public class LevelInfo : ScriptableObject
{
    public int coinCollected = 0;
    public bool keyPicked = false;

    public void Init()
    {
        coinCollected = 0;
        keyPicked = false;
    }

    public void KeyCollected()
    {
        keyPicked = true;
    }

    public void AddCoin( int value )
    {
        coinCollected += value ;
    }

    public void AddOneCoin()
    {
        AddCoin( 1 );
    }
}