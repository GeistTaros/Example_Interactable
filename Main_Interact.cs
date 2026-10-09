//Namespace
using TMPro; 
using UnityEngine;
using UnityEngine.InputSystem; // InputSystem is used for handling player input, including actions and bindings.
using System.Collections; //Used for coroutines, which allow for asynchronous operations and delays in Unity.


// The IInteractable interface defines a contract for objects that can be interacted with in the game.
//Interface
public interface IInteractable // The IInteractable interface defines a contract for objects that can be interacted with in the game.
{
    void Interact();            // The Interact method is called when the player interacts with the object.
                                // Implementing classes must provide their own logic for this method.

}
// The Main_Interact class handles the interaction logic for a GameObject that implements the IInteractable interface.
public class Main_Interact : MonoBehaviour
{
    // Fields
    [SerializeField] InputActionAsset playerActionAsset;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private string interactButtonText = "E";
    [SerializeField] private string interactedText = "Interacted!";
    [SerializeField] private int delayBeforeDisableUI = 2; 

    private TextMeshProUGUI uiText;
    private bool isInteracting;
    private IInteractable interactable;
    private InputAction interactAction;
    private bool hasInteracted = false;


    // Methods
    private void Awake()
    {

        var interactMap = playerActionAsset.FindActionMap("Player");
        interactAction = interactMap.FindAction("Interact");
        
        interactable = GetComponent<IInteractable>();
        uiText = uiContainer.GetComponentInChildren<TextMeshProUGUI>(true);
        if (uiText == null)
        {
            uiText = uiContainer.AddComponent<TextMeshProUGUI>();
        }
        if (uiText != null)
        {
            if (interactAction != null)
            {
                uiText.text = interactButtonText;
            }
        }
        uiContainer.SetActive(false);
        this.enabled = false; // Disable the script initially to prevent interaction until the player is in range.
    }
    private void OnEnable()
    {
        interactAction?.Enable();
    }

    private void OnDisable()
    {
        interactAction?.Disable();
    }

    void Update()
    {
        if (isInteracting)

        {

            if (interactAction.WasPressedThisFrame())
            {
                interactable.Interact();
                hasInteracted = true;
                if (uiText != null)
                {
                    uiText.text = interactedText;
                }
                StartCoroutine(DisableUIAfterDelay(delayBeforeDisableUI));

            }
        }
        if (Time.timeScale == 0f)
        {
            uiContainer.SetActive(false);
        }
    }
    private IEnumerator DisableUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiContainer.SetActive(false);
        hasInteracted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            this.enabled = true; 
            if (!hasInteracted)
            {
                uiContainer.SetActive(true);
                if (uiText != null)
                {
                    uiText.text = interactButtonText;
                }
                isInteracting = true;
            }
            else
            {
                uiContainer.SetActive(true);
                if (uiText != null)
                {
                    uiText.text = interactedText;
                }
                StartCoroutine(DisableUIAfterDelay(delayBeforeDisableUI));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiContainer.SetActive(false);
            isInteracting = false;
            hasInteracted = false;
            this.enabled = false;
        }
    }

}
