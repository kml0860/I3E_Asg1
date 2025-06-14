// File: Assets/Scripts/FireHazard.cs

using System.Collections;
using UnityEngine;

/*
* Author: Your Name
* Date: 2025-06-14
* Description: Damages player over time while inside fire trigger.
*/

public class FireHazard : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 5;
    [SerializeField] private float tickRate = 0.5f;

    private Coroutine damageRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            other.TryGetComponent(out PlayerBehaviour player))
        {
            damageRoutine = StartCoroutine(ApplyDamage(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private IEnumerator ApplyDamage(PlayerBehaviour player)
    {
        while (true)
        {
            player.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickRate);
        }
    }
}
