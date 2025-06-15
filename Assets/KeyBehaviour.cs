// File: Assets/Scripts/KeyBehaviour.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Allows players access to locked doors
 */

/// <summary>
/// Represents a colored key the player can collect.
/// </summary>
public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] private string keyName = "Green"; // Name of the key type (e.g., "Red", "Blue", etc.)

    /// <summary>
    /// Called by the player when interacting with the key.
    /// Adds the key to inventory, plays sound, and removes the key from the scene.
    /// </summary>
    /// <param name="player">The PlayerBehaviour calling the collection.</param>
    public void Collect(PlayerBehaviour player)
    {
        player.AddKey(keyName); // Add key to player's collection
        Debug.Log("Collected key: " + keyName);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.keyClip); // Play pickup sound
        Destroy(gameObject); // Remove key from the scene
    }

    /// <summary>
    /// Returns the internal key name string.
    /// </summary>
    public string GetKeyName() => keyName;
}
