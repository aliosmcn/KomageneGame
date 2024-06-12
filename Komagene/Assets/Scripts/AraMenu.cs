using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AraMenu : MonoBehaviour
{
    [SerializeField] private VoidEvent onStartAnimation;
    private void Start()
    {
        onStartAnimation.Raise();
    }
}
