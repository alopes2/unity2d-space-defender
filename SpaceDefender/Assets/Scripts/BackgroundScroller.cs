using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] 
    private float _backgroundScrollSpeed = 0.5f;

    private Material _myMaterial;

    private Vector2 _offSet;

    // Start is called before the first frame update
    void Start()
    {
        _myMaterial = GetComponent<Renderer>().material;
        _offSet = new Vector2(0f, _backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        _myMaterial.mainTextureOffset += _offSet * Time.deltaTime;
    }
}
