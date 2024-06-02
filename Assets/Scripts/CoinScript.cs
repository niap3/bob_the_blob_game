using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public Text coinsText;
    private int coins = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Bob")
        {
            coins++;
            UpdateCoinsText();
            Destroy(gameObject);
        }
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + coins;
        }
    }
}
