using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float distance, time, xPos;

    [SerializeField]
    private float touchSensitivity = 0.02f; // Sensitivity for touch input (adjustable in Inspector)

    private float speed;

    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;
        speed = distance / time;
    }

    private void FixedUpdate()
    {
        if (!hasGameStarted) return;

        // Movement direction
        float horizontalInput = 0f;

        // Check if running on a phone (Touch Input)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Process touch
            if (touch.phase == TouchPhase.Moved)
            {
                horizontalInput = touch.deltaPosition.x * touchSensitivity; // Adjusted for sensitivity
            }
        }
        else
        {
            // Use keyboard or controller input for non-touch platforms
            horizontalInput = Input.GetAxis("Horizontal");
        }

        // Normalize horizontalInput for consistent speed
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);

        Vector3 temp = new Vector3(horizontalInput * speed * 2f, 0, speed) * Time.fixedDeltaTime;
        transform.Translate(temp);

        // Clamp position to stay within bounds
        temp = transform.position;
        if (temp.x > xPos)
            temp.x = xPos;
        if (temp.x < -xPos)
            temp.x = -xPos;
        transform.position = temp;
    }
}
