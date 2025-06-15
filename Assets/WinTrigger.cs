// File: Assets/Scripts/WinTrigger.cs

using UnityEngine;

/*
 * Author: Your Name
 * Date: 2025-06-14
 * Description: Ends the game when player enters win zone.
 */

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance?.ShowWinMessage();
            Debug.Log("Player entered win zone!");
        }
    }
}
