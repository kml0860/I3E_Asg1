// File: Assets/Scripts/LandmineBehaviour.cs

using UnityEngine;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Landmine that damages player on contact, then self-destructs.
 */

public class Landmine : MonoBehaviour
{
    [SerializeField] private int minDamage = 40;
    [SerializeField] private int maxDamage = 60;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            int damage = Random.Range(minDamage, maxDamage + 1);
            player.TakeDamage(damage);
            Debug.Log($"Landmine exploded for {damage} damage!");
            gameObject.SetActive(false);
        }
    }
}
