// File: Assets/Scripts/FireHazard.cs

using System.Collections;
using UnityEngine;

/*
* Author: Kai Ming
* Date: 2025-06-14
* Description: Damages player over time while inside fire trigger.
*/

/// <summary>
/// A hazard that continuously deals damage to the player while they remain in its trigger zone.
/// </summary>
public class FireHazard : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 5;    // Amount of damage per interval
    [SerializeField] private float tickRate = 0.5f;    // Time between damage ticks

    private Coroutine damageRoutine; // Reference to the running damage coroutine

    /// <summary>
    /// Starts damaging the player when they enter the hazard zone.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            damageRoutine = StartCoroutine(ApplyDamage(player));
        }
    }

    /// <summary>
    /// Stops damaging the player when they exit the hazard zone.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    /// <summary>
    /// Coroutine that deals damage repeatedly while the player is inside the hazard.
    /// </summary>
    private IEnumerator ApplyDamage(PlayerBehaviour player)
    {
        while (true)
        {
            player.TakeDamage(damagePerTick);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.landmineClip); // Can replace with a fire sound
            yield return new WaitForSeconds(tickRate);
        }
    }
}
