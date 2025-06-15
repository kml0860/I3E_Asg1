// File: Assets/Scripts/HealingOrbBehaviour.cs

using UnityEngine;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Heals the player on contact and then deactivates.
 */

/// <summary>
/// A floating orb that restores health to the player on contact.
/// Disables itself after healing.
/// </summary>
public class HealingOrb : MonoBehaviour
{
    [SerializeField] private int healAmount = 40; // Amount of HP restored on pickup

    /// <summary>
    /// Triggered when a collider enters the orb's area.
    /// Heals player and plays SFX, then disables the orb.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            player.ModifyHealth(healAmount); // Apply healing
            Debug.Log($"Healed for {healAmount} HP");
            AudioManager.Instance.PlaySFX(AudioManager.Instance.healClip); // Play heal sound
            gameObject.SetActive(false); // Deactivate the orb (allows reactivation later)
        }
    }
}
