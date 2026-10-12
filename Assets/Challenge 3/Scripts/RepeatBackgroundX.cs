using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackgroundX : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private void Start()
    {
        startPos = transform.position;
        // Fixed: Use .size.x / 2 for horizontal (X-axis) repetition
        repeatWidth = GetComponent<BoxCollider>().size.x / 2f;
    }

    private void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}


