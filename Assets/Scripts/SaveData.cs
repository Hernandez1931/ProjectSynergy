using UnityEngine;
using System.Collections;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;

    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public PlayerProfile profile;
    // should record the amount of "scrap" there is. this is to ensure that the player knows how much upgrade tokens, or "scrap" there is.
    public int scrap;
}