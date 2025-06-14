// File: Assets/Scripts/KeyBehaviour.cs

using UnityEngine;

/// <summary>
/// Represents a colored key the player can collect.
/// </summary>
public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] private string keyName = "Green"; // e.g., "Red", "Blue"

    public void Collect(PlayerBehaviour player)
    {
        player.AddKey(keyName);
        Debug.Log("Collected key: " + keyName);
        Destroy(gameObject);
    }

    public string GetKeyName() => keyName;
}
