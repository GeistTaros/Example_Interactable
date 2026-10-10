## Documentation for Interact with Object via Main_Interact.cs and Example_Interact.cs
Used in Unity [6000.3.12f1 LTS](https://docs.unity3d.com/6000.3/Documentation/Manual/WhatsNewUnity6.html), for other versions, please check the compatibility of the used features and functions.
Requires [Main_Interact.cs](Main_Interact.cs) and another Script, like [Example_Interact.cs](Example_Interact.cs)
For the another Script, it is required to use Name_Interact.cs for proper declaration and easy understanding.
The first part of the script defines the interaction performed, the second part defines,
that is an Interaction with an Object. Please use the “_” between the first and the second part to avoid confusion.
 [Main_Interact.cs](Main_Interact.cs) is the main Script, it can be modified/extended, but normally this is not required


 ***1. Overview of the Main_Interact.cs Script***


 _Code for Main_Interact.cs_
```charp

using TMPro; 
using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;

public interface IInteractable 
{
    void Interact();           

}

public class Main_Interact : MonoBehaviour
{
    [SerializeField] InputActionAsset playerActionAsset;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private string interactButtonText = "E";
    [SerializeField] private string interactedText = "Interacted!";

    private TextMeshProUGUI uiText;
    private bool isInteracting;
    private IInteractable interactable;
    private InputAction interactAction;
    private bool hasInteracted = false;


    
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
        this.enabled = false; 
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
                StartCoroutine(DisableUIAfterDelay(2f));

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
                StartCoroutine(DisableUIAfterDelay(2f));
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
```


***_First Part: Namespaces_***


```charp
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
```
[TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@4.0/manual/index.html) is reqired for UI Text, please install TextMeshPro via Package Manager, if not installed yet. And don´t use UnityEngine.UI, because it is not supported in this project.

[UnityEngine](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) is required for Unity Core features, like MonoBehaviour, GameObject, Collider, etc.,

[UnityEngine.InputSystem](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/index.html) is required for Input System, including actions and keyboard input,

[System.Collections](https://docs.microsoft.com/en-us/dotnet/api/system.collections?view=net-7.0) is required for IEnumerator and Coroutine.


***_Second Part: IInteractable Interface // Interface Implementation_***

```charp
public interface IInteractable
{
    void Interact();
}
```
[IInteractable](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/) is an interface that defines a contract for interactable objects. Any class that implements this interface must provide an implementation for the ``Interact()`` method. This allows for a consistent way to handle interactions with different objects in the game.
Interfaces are useful for defining common behavior across different classes without enforcing a specific class hierarchy. The method ```Interact()``` is a placeholder for the interaction logic that will be defined in the classes that implement this interface and is written in the second part of the script, like [Example_Interact.cs](Example_Interact.cs). 
The [Main_Interact.cs](Main_Interact.cs) script will call this method when the player interacts with the object.

***_Third Part: class Main\_Interact and its functionality_***

The [Main_Interact.cs](Main_Interact.cs) script is a [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) that handles the interaction logic for objects in the game.
It uses the [Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/index.html) to detect when the player presses the interact button and manages the UI elements that provide feedback to the player. The script also implements the ``IInteractable`` interface,
allowing it to define specific interaction behavior in the ``Interact()`` method. For more infomation about Interactable, 
please check the second part of the script and the [Example_Interact.cs](Example_Interact.cs) script,
which is an example of how to implement the ``IInteractable`` interface and define specific interaction behavior.
It exisst only one class in the script, which is ``public class Main_Interact : MonoBehaviour``, and it is responsible for managing the interaction logic, UI elements, and player input for interactable objects in the game.
The class is a [MonoBehaviour](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html), which means it can be attached to GameObjects in the Unity scene and will have access to Unity's event methods, such as ``Awake()``, ``Update()``, ``OnTriggerEnter()``, and ``OnTriggerExit()``.
More information about the class and its functionality can be found in the [Unity Documentation](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html) and the [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/classes).
In the [Example_Interact.cs](Example_Interact.cs) exist only one class, which is ``public class Example_Interact : MonoBehaviour, IInteractable``, and it is responsible for defining specific interaction behavior for an object in the game.
More infamtion about the [Example_Interact.cs](Example_Interact.cs) can be found below in the second part of the documentation


 _***Fourth Part: Serialized Fields and Private Variables***_

 [Serialized fields](https://docs.unity3d.com/ScriptReference/SerializeField.html) and [private variables](https://docs.unity3d.com/ScriptReference/SerializeField.html) are used to store references to other objects, UI elements, and configuration settings that can be adjusted in the Unity Inspector.
 In this script, the serialized fields include:
```csharp
    [SerializeField] InputActionAsset playerActionAsset;
    [SerializeField] private GameObject uiContainer;
    [SerializeField] private string interactButtonText = "E";
    [SerializeField] private string interactedText = "Interacted!";
    [SerializeField] private int delayBeforeDisableUI = 2; 
 ```
 The Serialized fields are used to store references, below is a description of each serialized field:

``InputActionAsset`` _playerActionAsset_, which is necessary for the [Input System](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/index.html) to detect player input.

 ``GameObject``_UI Container_, which should be the [UI Canvas](https://docs.unity3d.com/Manual/UICanvas.html), and need to be assigned in the [Inspector](https://docs.unity3d.com/6000.3/Documentation/Manual/InspectorOptions.html).
 
 ``string`` _interactButtonText_, which is the text displayed on the UI when the player can interact with the object. Standard is "E", but can be changed in the [Inspector](https://docs.unity3d.com/6000.3/Documentation/Manual/InspectorOptions.html).
 
 ``string`` _interactedText_, which is the text displayed on the UI after the player has interacted with the object. Standard is "Interacted!", but can be changed in the [Inspector](https://docs.unity3d.com/6000.3/Documentation/Manual/InspectorOptions.html).

 ``int`` _delayBeforeDisableUI_, which is the delay in seconds before the UI is disabled after interaction. Standard is 2 seconds, but can be changed in the [Inspector](https://docs.unity3d.com/6000.3/Documentation/Manual/InspectorOptions.html).

 ```charp
    private TextMeshProUGUI uiText;
    private bool isInteracting;
    private IInteractable interactable;
    private InputAction interactAction;
    private bool hasInteracted = false;
 ```
 The private variables are used to store references, below is a description of each private variable:

 ``TextMeshProUGUI`` _uiText_, which is a reference to the [TextMeshProUGUI](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html) component that displays the interaction text on the UI.

 ``bool`` _isInteracting_, which is a flag that indicates whether the player is currently in range to interact with the object.

 ``IInteractable`` _interactable_, which is a reference to the object that implements the IInteractable interface, allowing for specific interaction behavior.

 ``InputAction`` _interactAction_, which is a reference to the [InputAction](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/manual/index.html) that detects when the player presses the interact button.

 ``bool`` _hasInteracted_, which is a flag that indicates whether the player has already interacted with the object, preventing multiple interactions in quick succession.

 You can change the serialized fields in the Unity Inspector, but please do not change the private variables, because they are used internally in the script and changing them can cause unexpected behavior or errors.
 More information about the serialized fields and private variables can be found in the [Unity Documentation](https://docs.unity3d.com/6000.3/Documentation/Manual/InspectorOptions.html) and the [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields).

 

 ***__Fifth Part: Methods and their functionality__***

 The Methods below will be described in seperate parts, numbered from I to VII, and the functionality of each method will be explained in detail, including the purpose of the method, how it works, and any important considerations or best practices for using it.

 ***I: Awake() Method***

 ```charp
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
        this.enabled = false; 
    }
   ```
   The ``Awake()`` method is called when the script instance is being loaded.
   It initializes the interaction system by finding the action map __"Player"__ 
   Code:[```var interactMap = playerActionAsset.FindActionMap("Player");```](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/api/UnityEngine.InputSystem.InputSystem.html) and action for player input, [var](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/var) is for the variable type, which is automatically determined by the compiler. 
   The action for player input is called __"Interact"__. Code:[```interactAction = interactMap.FindAction("Interact");```](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.20/api/UnityEngine.InputSystem.InputSystem.html) Also, it retrieves the component that implements the ``IInteractable`` interface 
   and sets up the UI text for interaction prompts. If the UI text component is not found, it adds a new one.
   Finally, it disables the UI container and the script itself until the player enters the interaction range.
   _UiText_ [```GetComponentInChildren<TextMeshProUGUI>(true)```](https://docs.unity3d.com/ScriptReference/GameObject.GetComponentInChildren.html) is used to find the TextMeshProUGUI component in the UI container child,
   even if it is inactive. For the inactive child, the parameter ```true``` in ```GetComponentInChildren``` is required, otherwise it will return null.
   If the ``TextMeshProUGUI`` component is not found, it adds a new one to the UI container. 
   This ensures that there is always a ```TextMeshProUGUI``` component available for displaying interaction prompts.
   The UI container is set to inactive initially, preventing it from being visible until the player is in range to interact with the object.
   ```this.enabled = false``` is used to disable the script until the player enters the interaction range, 
   preventing unnecessary updates and checks when the player is not nearby. That is important for performance optimization,
   especially in larger scenes with many interactable objects.


   ***II: OnEnable() and OnDisable() Methods***

   ```csharp
    private void OnEnable()
    {
        interactAction?.Enable();
    }
    private void OnDisable()
    {
        interactAction?.Disable();
    }
   ```
   Both methoths are described together, because they are related to each other and are used to enable and disable the input action for player interaction.
   OnEnable() and OnDisable() are Unity event methods that are called when the script is enabled or disabled, respectively.
   That is called when the player enters or exits the interaction range, or when the script is enabled or disabled in the Unity Inspector.
   It is important to enable and disable the input action to ensure that the script only listens for player input when it is active and relevant.
   It prevents unnecessary input processing and potential conflicts with other scripts or systems in the game.


   ***III: Update() Method***

   ```csharp
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
   ```

   The [Update()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html) method is called once per frame and is responsible for checking player input and managing the interaction logic.
   Because the script is disabled when the player is not in range, the Update() method will only run when the player is nearby and can interact with the object.
   When the player is in range and the interaction button is pressed, it calls the ``Interact()`` method on the interactable object, also sets the ``hasInteracted`` flag to true,
   updates the UI text prompt to ``interactedText`` instead of the default interaction prompt ``interactButtonText``,
   and starts a coroutine to disable the UI after a short delay, which is specified by the ``delayBeforeDisableUI`` variable and can be adjusted in the Unity Inspector.
   If the game is paused (i.e., ``Time.timeScale`` is 0), 
   it hides the UI container to prevent it from being visible during pause. That prevents the UI from being displayed when the game is paused.

   ***IV: DisableUIAfterDelay() Coroutine***

   ```charp
    private IEnumerator DisableUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiContainer.SetActive(false);
        hasInteracted = false;
    }
   ```
   The ``DisableUIAfterDelay()`` method is a coroutine that waits for a specified delay before disabling the UI container and resetting the ``hasInteracted`` flag.
   This is useful for providing feedback to the player after an interaction, allowing them to see the interaction result before the UI disappears.
   For more information about coroutines, please check the [Unity Documentation](https://docs.unity3d.com/Manual/Coroutines.html) and the [IEnumerator](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=net-7.0) interface in C#.


   ****V: OnTriggerEnter() Method**

   ```charp
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
   ```
   [OnTriggerEnter()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter.html) is called when another collider enters the trigger collider attached to the object this script is on. 
   It checks if the other collider belongs to the player by comparing tags with ``other.CompareTag("Player")``.
   If the player enters the trigger, it enables the script ``this.enabled = true``, shows the interaction UI with ``uiContainer.SetActive(true)``, and sets the ``isInteracting`` flag to true. 
   If the player has already interacted with the object, it shows the ``interactedText`` instead of the default interaction prompt and starts a coroutine to disable the UI after a short delay, 
   specified by the ``delayBeforeDisableUI`` variable. isInteracting is set to true,
   allowing the Update() method to check for player input and handle the interaction logic.


   ***VI: OnTriggerExit() Method***

   ```charp
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
   ```
   [OnTriggerExit()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerExit.html) 
   is called when another collider exits the trigger collider attached to the object this script is on. 
   It checks if the other collider belongs to the player by comparing tags with ``other.CompareTag("Player")``.
   If the player exits the trigger, it hides the interaction UI with ``uiContainer.SetActive(false)``, resets the ``isInteracting`` and ``hasInteracted`` flags to false, 
   and disables the script with ``this.enabled = false``. 

   ***VII: OnTriggerStay() Method (Optional)***


   [OnTriggerStay()](https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerStay.html) is not used in this script, it is also not recommended anymore.
   It is unneccesary and cost more CPU usage, you can modify the ``OnTriggerEnter()`` and the ``OnTriggerExit()`` with a boolean  and use an ``if`` statement
   in the ``Update()``


   ***2. Overview of the Example_Interact.cs Script***

   The [Example_Interact.cs](Example_Interact.cs) script is an example implementation of the ``IInteractable`` interface, 
   demonstrating how to define specific interaction behavior for an object in the game.
   In the Example_Interact.cs script, the ``Interact()`` method is implemented to perform a specific action when the player interacts with the object.
   It can be modified to perform any desired action, such as opening a door,
   picking up an item, or triggering an event in the game, or play a sound, or change the color of the object,
   or any other action that is required for the game.

   Make sure that the Example_Interact.cs script is attached to the same GameObject as the Main_Interact.cs script,
   and that the GameObject has a Collider component with the "Is Trigger" property enabled,
   Also the components for the specific action, like AudioSource for playing a sound, 
   or Animator for playing an animation, or any other component that is required for the specific action
   and need to be called in the Interact() method, are attached to the same GameObject or referenced in the Example_Interact.cs script.

   For example, if there is a animation to play when the player interacts with the object, make sure that the Animator component is attached to the child of the GameObject,
   and GetComponentInChildren<Animator>() is called in the Example_Interact.cs script to reference the Animator component, 
   and then the animation can be played in the Interact() method. If the Component is attached to the same GameObject,
   GetComponent<Animator>() can be used to reference the Animator component. Make sure that the component is a child of the GameObject, 
   otherwise GetComponentInChildren<Animator>() will return null.
   In Example_Interact.cs script, the Interact() method can be implemented like this:
   ```charp
    public void Interact()
    {
        // Play animation
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("PlayAnimation");
        }
        // Play sound
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        // Change color
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
    }
   ```
   More information about the Interact() method and its implementation can be found in the [Example_Interact.cs](Example_Interact.cs) script, 
   where it is defined to perform specific actions when the player interacts with the object.
   
   