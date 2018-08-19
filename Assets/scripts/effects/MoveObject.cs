using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public float targetPosOffsetX;
    public float targetPosOffsetY;

    public float speed;

    private Vector3 targetPos;
    private float startingDistance;
    private TextMesh textMesh;

    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMesh>();
        InitPosition();
	}

    private void InitPosition() {
        targetPos.x = transform.position.x + targetPosOffsetX;
        targetPos.y = transform.position.y + targetPosOffsetY;
        targetPos.z = transform.position.z;

        startingDistance = Vector3.Distance(transform.position, targetPos);
    }

    // Update is called once per frame
    void Update() {
        float newDistance = Vector3.Distance(transform.position, targetPos);
        float percentDistance = (1 - (newDistance / startingDistance));
        Debug.Log("Percent distance: " + (1 - (newDistance / startingDistance)));

        float x1 = targetPos.x;
        float y1 = targetPos.y;
        float currentSpeed = speed - (speed * percentDistance);
        
        float nextX = Mathf.MoveTowards(transform.position.x, x1, currentSpeed * Time.deltaTime);
        float nextY = Mathf.MoveTowards(transform.position.y, y1, currentSpeed * Time.deltaTime);
        Vector3 nextPos = new Vector3(nextX, nextY, transform.position.z);
        transform.position = nextPos;

        if (percentDistance > 0.5F) {
            // ab 50% anfangen auszublenden
            float percentAlpha = ((1 - percentDistance) / 0.5F);

            Color newColor = textMesh.color;
            newColor.a = percentAlpha;
            textMesh.color = newColor;
        }
        
        // Do something when we reach the target
        if (percentDistance > 0.9F) {
            Destroy(gameObject);
        }
    }
}
