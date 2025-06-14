// File: Assets/Scripts/CoinBehaviour.cs

using UnityEngine;

/*
* Author: Kai Ming
* Date: 2025-06-12
* Description: Defines behavior for collectible coins.
*/

/// Coin object that increases player score when collected.

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField]
    private int coinValue = 1;

    /// Called by player when they interact with this coin.

    public void Collect(PlayerBehaviour player)
    {
        Debug.Log("Coin collected!");
        player.ModifyScore(coinValue);
        Destroy(gameObject);
    }
}
