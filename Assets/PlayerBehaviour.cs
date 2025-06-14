// File: Assets/Scripts/PlayerBehaviour.cs

using UnityEngine;
using UnityEngine.InputSystem; // Required for Input System
using System.Collections.Generic;


/*
* Author: Kai Ming
* Date: 2025-06-12
* Description: Handles player score, health, and interactions.
*/
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int currentScore = 0;

    [Header("Respawn Settings")]
    [SerializeField] private Transform spawnPoint;

   private HashSet<string> collectedKeys = new HashSet<string>();

    private bool canInteract = false;
    private CoinBehaviour currentCoin = null;

    /// Called by Unity Input System when Interact button is pressed.

    public void OnInteract()
    {
        // fallback: check Input manually if needed
        if (canInteract && currentCoin != null)
        {
            currentCoin.Collect(this);
        }
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
RaycastHit hit;

if (Physics.Raycast(ray, out hit, 3f))
{
    if (hit.collider.TryGetComponent(out SlidingDoor door))
        door.Interact();

    else if (hit.collider.TryGetComponent(out KeyBehaviour key))
        key.Collect(this);

    else if (hit.collider.TryGetComponent(out LockedDoor locked))
        locked.TryOpen(this);
}
      
}


    /// Adds to player's score.

    public void ModifyScore(int amt)
{
    Debug.Log("UIManager.Instance = " + UIManager.Instance);

    currentScore += amt;
    UIManager.Instance.SetCoins(currentScore);
}

public void ModifyHealth(int amount)
{
    currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    UIManager.Instance.SetHealth(currentHealth, maxHealth);
}


   public void TakeDamage(int amount)
{
    currentHealth -= amount;
    Debug.Log("Took damage. Health: " + currentHealth);

    if (UIManager.Instance != null)
        UIManager.Instance.SetHealth(currentHealth, maxHealth);

    if (currentHealth <= 0)
        Respawn();
}


    public void Respawn()
{
    Debug.Log("Player died. Respawning...");

    // If using CharacterController, disable before moving
    if (TryGetComponent(out CharacterController controller))
    {
        controller.enabled = false;
        transform.position = spawnPoint.position;
        controller.enabled = true;
    }
    else
    {
        transform.position = spawnPoint.position;
    }

    currentHealth = maxHealth;
    UIManager.Instance?.SetHealth(currentHealth, maxHealth);

    foreach (Landmine mine in FindObjectsOfType<Landmine>(true))
{
    mine.gameObject.SetActive(true);
}

    foreach (HealingOrb orb in FindObjectsOfType<HealingOrb>(true))
{
    orb.gameObject.SetActive(true);
}

}


    private void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("Collectible") &&
            other.TryGetComponent(out CoinBehaviour coin))
        {
            canInteract = true;
            currentCoin = coin;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentCoin != null && other.gameObject == currentCoin.gameObject)
        {
            canInteract = false;
            currentCoin = null;
        }
    }

        public void AddKey(string keyName)
    {
        if (!collectedKeys.Contains(keyName))
        {
            collectedKeys.Add(keyName);
            UIManager.Instance?.SetKeys(string.Join(", ", collectedKeys));
        }
    }

    public bool HasKey(string keyName) => collectedKeys.Contains(keyName);
}




       