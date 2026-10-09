using UnityEngine; // Required for MonoBehaviour, GameObject, Animation, Debug, etc.
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.InputSystem; // Required for InputActionAsset, InputAction, etc.
using UnityEngine.SceneManagement; // Required for scene management
using System.Collections; // Required for using IEnumerator and coroutines
// Example_Interact is a simple implementation of the IInteractable interface.
// Based on the IInteractable interface, this script allows the player to interact with the GameObject it is attached to.
// When the player interacts with this object, it will attempt to play an animation if one is found in the children of the GameObject.
// Please use this script as a template for creating your own interactable objects in the game.
// Main_Interact.cs handles the interaction logic and calls the Interact method when the player presses the interact button.
// More Infomation can be find in the Documentation folder in the project,
// which contains detailed explanations of how to use the IInteractable interface and the Main_Interact .cs script.
public class Example_Interact : MonoBehaviour ,IInteractable
{
    // The Interact method is called when the player interacts with this object.
    // In this example, it plays an animation if one is found in the children of the GameObject.
    // You can extend this method to include additional interaction logic, such as playing sound effects, changing materials, or updating UI elements.
    // More infomation about the IInteractable interface and how to use it can be found in the ObjectInteract.cs script,
    // which handles the interaction logic and calls this Interact method when the player presses the interact button.
    // Make sure to attach the ObjectInteract.cs script to the same GameObject as this Example_Interact.cs script for the interaction to work correctly.
    public void Interact()
    {
        // Attempt to get the Animation component from the children of this GameObject
        Animation examplAnim = GetComponentInChildren<Animation>();
        // If an Animation component is found, play the animation
        if (examplAnim != null)
        {
            examplAnim.Play();
        }
        // If no Animation component is found, log a warning message
        else
        {
            Debug.LogWarning("No Animation component found in children of " + gameObject.name);
        }
        // if statements are used to check for null references before accessing components or properties.
        // It is not strictly necessary to use if statements in this case,
        // but can help prevent potential null reference exceptions if the Animation component is not found.


        // Additional interaction logic can be added here
        // For example, you could trigger a sound effect, change a material, or update a UI element


        // Example: Play a sound effect
        // AudioSource audioSource = GetComponent<AudioSource>();
        // if (audioSource != null)
        // {
        //     audioSource.Play();
        // }


        // Example: Change material color
        // Renderer renderer = GetComponent<Renderer>();
        // if (renderer != null)
        // {
        //     renderer.material.color = Color.red; // Change to red as an example
        // }


        // Example: Update a UI element
        // TMP_Text uiText = FindObjectOfType<TMP_Text>();
        // if (uiText != null)
        // {
        //     uiText.text = "Interacted with " + gameObject.name;
        // }

        // Example: Trigger an event
        // EventManager.TriggerEvent("OnInteract", gameObject);
        // Note: The above examples are commented out. You can uncomment and modify them based on your specific interaction requirements.

        // Note: Ensure that the necessary components (AudioSource, Renderer, TMP_Text, etc.)
        // are attached to the GameObject or its children for the above examples to work correctly.

        // Note: StartCoroutine can be used for delayed actions or animations if needed.
        // Example: StartCoroutine(DelayedAction());
        // IEnumerator DelayedAction()
        // {
        //     yield return new WaitForSeconds(1f); // Wait for 1 second
        //     // Perform the action after the delay
        // }
        // Example 2: StartCoroutine(PlayAnimationWithDelay(2f));
        // IEnumerator PlayAnimationWithDelay(float delay)
        // {
        //      Animation anim = GetComponentInChildren<Animation>();
        //      delay = 2f; // Set the delay to 2 seconds
        //      anim.wrapMode = WrapMode.Loop;
        //      anim.Play();
        //      yield return new WaitForSeconds(delay);
        //      anim.Stop();
        //  }






    }
}
