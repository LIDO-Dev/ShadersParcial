using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSway1 : MonoBehaviour
{
    [Header("<color=yellow>Sway</color>")]
    public Transform _cameraSwayObject;
    public float _swayAmountA = 1;
    public float _swayAmountB = 2;
    public float _swayScale = 40;
    public float _swayLerpSpeed = 40;
    public float _swayTime;
    public Vector3 _cameraSwayPosition;
    void Update()
    {
       CalculateCameraSway(); 
    }



     public void CalculateCameraSway()
    {
        var targetposition = LissajousCurve(_swayTime, _swayAmountA, _swayAmountB) / _swayScale;

        _cameraSwayPosition = Vector3.Lerp(_cameraSwayPosition, targetposition, Time.deltaTime * _swayLerpSpeed);

        _swayTime += Time.deltaTime;

        _cameraSwayObject.localPosition = _cameraSwayPosition;
    }

        

    private Vector3 LissajousCurve(float Time, float A, float B)
{
    return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
}
}
