using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Which action are we listening to
    [SerializeField] private InputActionReference m_ActionReference;
    public InputActionReference actionReference { get => m_ActionReference ; set => m_ActionReference = value; }

    // Reference to the canvas that we want to toggle
    public Canvas lefthandCanvas;

    // Stores the type of the input (can vary from devices)
    Type lastActiveType = null;

    // Store if the menu button is currently down (This allows for the initial press event to be detected)
    bool menuButtonDown = false;

    void Update()
    {
        // If our action is being triggered
        if (actionReference != null && actionReference.action != null && actionReference.action.enabled && actionReference.action.controls.Count > 0)
        {
            Type typeToUse = null;


            // Figure out what variable type the input is in
            if (actionReference.action.activeControl != null)
            {
                typeToUse = actionReference.action.activeControl.valueType;
            }
            else
            {
                typeToUse = lastActiveType;
            }

            // For either type of input, toggle the canvas
            if(typeToUse == typeof(bool))
            {
                lastActiveType = typeof(bool);
                if(actionReference.action.ReadValue<bool>()){
                    if(!menuButtonDown){
                        lefthandCanvas.gameObject.SetActive(!lefthandCanvas.gameObject.activeSelf);
                        menuButtonDown = true;
                    }
                } else{
                    menuButtonDown = false;
                }
                
            }
            else if(typeToUse == typeof(float))
            {
                lastActiveType = typeof(float);
                if(actionReference.action.ReadValue<float>() > 0.2){
                    if(!menuButtonDown){
                        lefthandCanvas.gameObject.SetActive(!lefthandCanvas.gameObject.activeSelf);
                        menuButtonDown = true;
                    }
                } else{
                    menuButtonDown = false;
                }
            }

        }
    }

}
