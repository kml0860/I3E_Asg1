// File: Assets/Scripts/CoinBehaviour.cs

using UnityEngine;

/*
* Author: Kai Ming
* Date: 2025-06-12
* Description: Defines behavior for collectible coins.
*/

/// <summary>
/// Coin object that increases player score when collected.
/// </summary>
public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    private int coinValue = 1; // Score value this coin adds to the player

    /// <summary>
    /// Called by the player when interacting with the coin.
    /// Increases score, plays sound, and destroys the coin.
    /// </summary>
    /// <param name="player">The PlayerBehaviour calling the collection.</param>
    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Coin collected!");

        // Increase player score
        player.ModifyScore(coinValue);

        // Play coin collection sound effect
        AudioManager.Instance.PlaySFX(AudioManager.Instance.coinClip);

        // Remove coin from the scene
        Destroy(gameObject);
    }
}
