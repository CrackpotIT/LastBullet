using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	
	// How long the object should shake for.
	private float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.7f;
	private float decreaseFactor = 1.0f;
	
	private Vector3 originalPos;
    private bool shake = false;

    // Static instance
    static CameraShake _instance;

    private void Start() {
        _instance = this;
    }

    public static CameraShake GetInstance() {
        if (!_instance) {
            Debug.LogError("CameraShake Error, not instanciated!");
        }
        return _instance;
    }

    public void StartShake(float shakeDuration, float shakeAmount, float decreaseFactor = 1f) {
        this.shakeDuration = shakeDuration;
        this.shakeAmount = shakeAmount;
        this.decreaseFactor = decreaseFactor;
        shake = true;

    }

    void OnEnable()
	{
		originalPos = transform.localPosition;
	}

	void Update()
	{
        if (shake) {
            if (shakeDuration > 0) {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                shakeDuration = 0f;
                transform.localPosition = originalPos;
                shake = false;
            }
        }
	}
}
