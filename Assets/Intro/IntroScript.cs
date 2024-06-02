using UnityEngine;
using UnityEngine.InputSystem;

public class SlideshowScript : MonoBehaviour
{
    public GameObject[] images;  // Assign your images to this array in the Unity Editor
    private int currentIndex = 0;

    private void Start()
    {
        ShowCurrentImage();
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame))
        {
            DestroyCurrentImage();
        }
    }

    private void DestroyCurrentImage()
    {
        if (currentIndex < images.Length)
        {
            Destroy(images[currentIndex]);
            currentIndex++;

            if (currentIndex < images.Length)
            {
                ShowCurrentImage();
            }
            else
            {
                Debug.Log("Slideshow completed!");
            }
        }
    }

    private void ShowCurrentImage()
    {
        if (currentIndex < images.Length)
        {
            images[currentIndex].SetActive(true);
        }
    }
}
