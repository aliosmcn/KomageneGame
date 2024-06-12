using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private VoidEvent onSpacePressed;
    [SerializeField] private VoidEvent onCtrlPressed;

    [SerializeField] private FloatEvent onHorizontalValueChanged;
    [SerializeField] private FloatEvent onVerticalValueChanged;

    private void Update()
    {

        onHorizontalValueChanged.Raise(Input.GetAxisRaw("Horizontal"));
        onVerticalValueChanged.Raise(Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onSpacePressed.Raise();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            onCtrlPressed.Raise();
        }

    }
}
