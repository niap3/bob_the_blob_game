using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeElapsed = 0.0f; // Set your initial time here
    public GameObject playerObject; // Drag and drop the player GameObject in the Inspector
    public Text timerText;

    private bool isTimerRunning = true;

    private void Start()
    {
        UpdateTimerText();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timeElapsed);
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cyan") && playerObject != null && other.gameObject == playerObject)
        {
            // Stop the timer when player collides with "Cyan"
            StopTimer();
        }
    }
}
