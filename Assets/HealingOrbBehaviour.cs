// File: Assets/Scripts/HealingOrbBehaviour.cs

using UnityEngine;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Heals the player on contact and then deactivates.
 */

public class HealingOrb : MonoBehaviour
{
    [SerializeField] private int healAmount = 40;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            player.ModifyHealth(healAmount);
            Debug.Log($"Healed for {healAmount} HP");

            gameObject.SetActive(false); // or Destroy(gameObject);
        }
    }
}
