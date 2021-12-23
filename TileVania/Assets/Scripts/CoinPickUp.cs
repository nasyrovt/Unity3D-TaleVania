using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] int coinPoints = 1;
    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().addToScore(coinPoints);
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }

}
