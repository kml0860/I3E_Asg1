// File: Assets/Scripts/UIManager.cs

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Manages player HUD: health, score, keys, and win message.
 */

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI winScoreText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private TextMeshProUGUI requirementText;
    [SerializeField] private float messageDuration = 2f;
    private Coroutine messageRoutine;


    private void Awake()
    {
        if (Instance == null) Instance = this;
       winText.gameObject.SetActive(false);
    }

    public void SetHealth(int current, int max)
    {
        healthBar.maxValue = max;
        healthBar.value = current;
    }

    public void SetCoins(int count)
    {
        coinText.text = $"Coins: {count} / 7";
    }

    public void SetKeys(string keys)
    {
    if (keyText != null)
        keyText.text = $"Keys: {keys}";
    else
        Debug.LogWarning("KeyText UI is not assigned!");
    }


    public void ShowWinMessage()
{
    winText.gameObject.SetActive(true);

    if (winScoreText != null)
    {
        winScoreText.text = $"Final Score: {PlayerBehaviour.Instance.CurrentScore}";
        winScoreText.gameObject.SetActive(true);
    }
}


    public void ShowKeyRequirement(string keyName)
    {
        if (requirementText == null) return;

        if (messageRoutine != null)
            StopCoroutine(messageRoutine);

                requirementText.text = $"You need a {keyName} keycard to open this door.";
                requirementText.gameObject.SetActive(true);
                messageRoutine = StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
        {
            yield return new WaitForSeconds(messageDuration);
            requirementText.gameObject.SetActive(false);
        }
}
