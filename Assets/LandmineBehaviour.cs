// File: Assets/Scripts/LandmineBehaviour.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Landmine that damages player on contact, then self-destructs.
 */

/// <summary>
/// Triggers on player contact, deals random damage, plays SFX, then deactivates.
/// </summary>
public class Landmine : MonoBehaviour
{
    [SerializeField] private int minDamage = 40; // Minimum damage dealt
    [SerializeField] private int maxDamage = 60; // Maximum damage dealt

    /// <summary>
    /// Called when the player enters the landmine's trigger.
    /// Applies random damage and disables the landmine.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            int damage = Random.Range(minDamage, maxDamage + 1); // Random damage
            player.TakeDamage(damage);
            Debug.Log($"Landmine exploded for {damage} damage!");

            AudioManager.Instance.PlaySFX(AudioManager.Instance.landmineClip); // Explosion sound
            gameObject.SetActive(false); // Disable (can be reset later on respawn)
        }
    }
}
