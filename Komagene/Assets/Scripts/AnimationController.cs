using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onStartAnimation;
    [SerializeField] private VoidEvent onEndAnimation;

    [Header("Animation Prefab")]
    [SerializeField] private GameObject startAnimPrefab;
    [SerializeField] private GameObject endAnimPrefab;

    private void OnEnable()
    {
        onStartAnimation.AddListener(StartAnim);
        onEndAnimation.AddListener(EndAnim);
    }
    private void OnDisable()
    {
        onStartAnimation.RemoveListener(StartAnim);
        onEndAnimation.RemoveListener(EndAnim);
    }

    private void StartAnim()
    {
        startAnimPrefab.SetActive(true);
        AudioManager.Instance.PlaySFX("gecisBas");
        Invoke(nameof(DisableStartAnim), 5f);
    }
    private void DisableStartAnim()
    {
        startAnimPrefab.SetActive(false);
    }

    private void EndAnim()
    {
        endAnimPrefab.SetActive(true);
        AudioManager.Instance.PlaySFX("gecisSon");
        Invoke(nameof(DisableEndAnim), 1f);
    }

    private void DisableEndAnim()
    {
        endAnimPrefab.SetActive(false);
    }

}
