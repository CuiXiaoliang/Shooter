using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Para
    public Transform TargetTransform;
    public float Smoothing = 5f;

    private Vector3 _offset;
    #endregion

    #region UnityInternalCall

    void Start()
    {
        _offset = transform.position - TargetTransform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = TargetTransform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
    }

    #endregion


}
