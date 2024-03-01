using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "ScriptableObjects/Values/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxLifePoints;
    public int currentLifePoints;
}
