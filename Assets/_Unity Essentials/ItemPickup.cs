using UnityEngine;
using TMPro; // Required for TextMeshPro UI

public class ItemPickup : MonoBehaviour
{
	public TextMeshProUGUI scoreText; // Reference to your TMP Text UI
	private static int score = 0;    // Shared score count across pickups

	void OnTriggerEnter(Collider other)
	{
		// Check if the player collided with this pickup
		if (other.CompareTag("Player") || other.gameObject.name == "Player")
		{
			score++;

			// Update UI text
			if (scoreText != null)
			{
				scoreText.text = "Supplies: " + score;
			}

			// Destroy pickup item
			Destroy(gameObject);
		}
	}
}
