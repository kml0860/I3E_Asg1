// File: Assets/Scripts/UIManager.cs

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/*
 * Author: Kai Ming
 * Date: 2025-06-14
 * Description: Manages player HUD: health, score, keys, and win message.
 */

/// <summary>
/// Manages all aspects of the in-game user interface.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    [SerializeField] private Slider healthBar; // Reference to the player's health bar UI slider
    [SerializeField] private TextMeshProUGUI coinText; // Text element to display the player's coin count
    [SerializeField] private TextMeshProUGUI keyText; // Text element to display the player's collected keys
    [SerializeField] private TextMeshProUGUI winText; // Text element for the win message
    [SerializeField] private TextMeshProUGUI winScoreText; // Text element to display the final score on win
    [SerializeField] private GameObject crosshair; // Reference to the crosshair GameObject (currently unused in this script)
    [SerializeField] private TextMeshProUGUI requirementText; // Text element for showing key requirements
    [SerializeField] private float messageDuration = 2f; // Duration for how long requirement messages are displayed
    private Coroutine messageRoutine; // Stores the reference to the coroutine for hiding messages


    /// <summary>
    /// Ensures only one instance of UIManager exists and initializes UI elements.
    /// </summary>
    private void Awake()
    {
        if (Instance == null) Instance = this;
        winText.gameObject.SetActive(false); // Hide the win text at the start
    }

    /// <summary>
    /// Updates the health bar's maximum and current values.
    /// </summary>
    public void SetHealth(int current, int max)
    {
        healthBar.maxValue = max;
        healthBar.value = current;
    }

    /// <summary>
    /// Updates the displayed coin count.
    /// </summary>
    public void SetCoins(int count)
    {
        coinText.text = $"Coins: {count} / 7";
    }

    /// <summary>
    /// Updates the displayed list of collected keys.
    /// </summary>
    public void SetKeys(string keys)
    {
        if (keyText != null)
            keyText.text = $"Keys: {keys}";
        else
            Debug.LogWarning("KeyText UI is not assigned!");
    }


    /// <summary>
    /// Activates and displays the win message along with the final score.
    /// </summary>
    public void ShowWinMessage()
    {
        winText.gameObject.SetActive(true);

        if (winScoreText != null)
        {
            winScoreText.text = $"Final Score: {PlayerBehaviour.Instance.CurrentScore}";
            winScoreText.gameObject.SetActive(true);
        }
    }


    /// <summary>
    /// Displays a message indicating which key is required to open a door.
    /// </summary>
    public void ShowKeyRequirement(string keyName)
    {
        if (requirementText == null) return;

        // Stop any existing message coroutine to prevent overlapping messages
        if (messageRoutine != null)
            StopCoroutine(messageRoutine);

        requirementText.text = $"You need a {keyName} keycard to open this door.";
        requirementText.gameObject.SetActive(true);
        messageRoutine = StartCoroutine(HideMessageAfterDelay()); // Start coroutine to hide message after a delay
    }

    /// <summary>
    /// Coroutine that waits for a specified duration and then hides the requirement message.
    /// </summary>
    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        requirementText.gameObject.SetActive(false);
    }
}