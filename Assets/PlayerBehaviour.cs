// File: Assets/Scripts/PlayerBehaviour.cs

using UnityEngine;
using UnityEngine.InputSystem; // Required for Unity's new Input System
using System.Collections.Generic;

/*
* Author: Kai Ming
* Date: 2025-06-12
* Description: Handles player score, health, inventory, interaction logic, and respawning.
*/
public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int currentScore = 0;

    [Header("Respawn Settings")]
    [SerializeField] private Transform spawnPoint;

    // Stores collected keys (e.g. "Red", "Green")
    private HashSet<string> collectedKeys = new HashSet<string>();

    // Tracks interactable objects in trigger zones
    private bool canInteract = false;
    private CoinBehaviour currentCoin = null;

    /// Called by Input System when Interact button is pressed
    public void OnInteract()
    {
        // Interact with coin if nearby
        if (canInteract && currentCoin != null)
        {
            currentCoin.Collect(this);
        }

        // Raycast forward from camera for other interactions
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

    /// Increases player score by given amount and updates UI
    public void ModifyScore(int amt)
    {
        Debug.Log("UIManager.Instance = " + UIManager.Instance);
        currentScore += amt;
        UIManager.Instance.SetCoins(currentScore);
    }

    /// Increases player health (capped at maxHealth) and updates UI
    public void ModifyHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UIManager.Instance.SetHealth(currentHealth, maxHealth);
    }

    /// Reduces health, updates UI, and triggers Respawn if health reaches zero
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Took damage. Health: " + currentHealth);

        UIManager.Instance?.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Respawn();
    }

    /// Resets health, moves player to spawn point, and reactivates collectibles
    public void Respawn()
    {
        Debug.Log("Player died. Respawning...");

        // Reset player position (disable CharacterController briefly if present)
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

        // Reactivate all deactivated landmines
        foreach (Landmine mine in FindObjectsOfType<Landmine>(true))
        {
            mine.gameObject.SetActive(true);
        }

        // Reactivate all deactivated healing orbs
        foreach (HealingOrb orb in FindObjectsOfType<HealingOrb>(true))
        {
            orb.gameObject.SetActive(true);
        }
    }

    /// Trigger enter: detects collectible coins nearby
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible") &&
            other.TryGetComponent(out CoinBehaviour coin))
        {
            canInteract = true;
            currentCoin = coin;
        }
    }

    /// Trigger exit: stops player from interacting with coin once they leave range
    private void OnTriggerExit(Collider other)
    {
        if (currentCoin != null && other.gameObject == currentCoin.gameObject)
        {
            canInteract = false;
            currentCoin = null;
        }
    }

    /// Adds a key to player inventory and updates key UI
    public void AddKey(string keyName)
    {
        if (!collectedKeys.Contains(keyName))
        {
            collectedKeys.Add(keyName);
            UIManager.Instance?.SetKeys(string.Join(", ", collectedKeys));
        }
    }

    /// Returns true if player has a specific key
    public bool HasKey(string keyName) => collectedKeys.Contains(keyName);

    // Singleton reference for accessing the player globally
    public static PlayerBehaviour Instance;

    /// Unity Awake: sets static reference
    private void Awake()
    {
        Instance = this;
    }

    /// Read-only property to expose player's current score
    public int CurrentScore => currentScore;
}
