using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.8f;
    public float dampingSpeed = 0.5f;
    Vector3 initialPosition;

    void Awake()
    {
        if (transform == null)
        {
            Debug.LogError("Transform is NULL");
        }
    }

    void OnEnable()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = initialPosition;
        }
    }

    public void TriggerShake(float duration)
    {
        shakeDuration = duration;
    }
}
