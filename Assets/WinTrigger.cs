// File: Assets/Scripts/WinTrigger.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-15
 * Description: Ends the game when player enters win zone.
 */

/// <summary>
/// Triggers the end-game sequence when a player enters its collider.
/// </summary>
public class WinTrigger : MonoBehaviour
{
    /// <summary>
    /// Called when another collider enters this trigger.
    /// Checks if the entering collider is the player and initiates the win sequence.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Display the win message through the UIManager singleton
            UIManager.Instance?.ShowWinMessage();
            // Play the win sound effect through the AudioManager singleton
            AudioManager.Instance.PlaySFX(AudioManager.Instance.winClip);
            // Log a message to the console indicating the player has won
            Debug.Log("Player entered win zone!");
        }
    }
}