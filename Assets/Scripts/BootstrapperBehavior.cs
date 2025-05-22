using TMPro;
using UnityEngine;

public class BootstrapperBehavior : MonoBehaviour
{
    // This is the value that will be passed to the Swift code
    [SerializeField] private string argumentValue = "Julian";

    // This will display the result of the Swift code
    [SerializeField] private TextMeshProUGUI textEl;

    void Start()
    {
        textEl.text = MyPlugin.Run(argumentValue);
    }
}
