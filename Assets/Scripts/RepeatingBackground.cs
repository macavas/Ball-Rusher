using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

    public float backgroundSize;
    public float overLimit;

    private Transform cameraTransform;
    private Transform[] layers;
    private int downIndex;
    private int upIndex;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        downIndex = 0;
        upIndex = layers.Length - 1;
    }

    private void Update()
    {
        if (cameraTransform.position.y > overLimit)
            return;
        if (cameraTransform.position.y > (layers[upIndex].transform.position.y)-backgroundSize/2)
            ScrollUp();
    }

    private void ScrollUp()
    {
        layers[downIndex].position = Vector3.up * (layers[upIndex].position.y + backgroundSize);
        upIndex = downIndex;
        downIndex++;

        if (downIndex == layers.Length)
        {
            downIndex = 0;
        }
    }

}
