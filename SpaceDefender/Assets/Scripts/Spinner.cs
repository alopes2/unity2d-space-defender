using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float _spinSpeed = 720f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _spinSpeed * Time.deltaTime);       
    }
}
