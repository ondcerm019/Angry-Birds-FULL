using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLine : MonoBehaviour
{


    LineRenderer _lineRenderer;
    Bird _bird;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _bird = FindObjectOfType<Bird>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bird.IsDragging)
        {
            _lineRenderer.enabled = true;
            //Debug.Log(_bird.StartPosition);
            _lineRenderer.SetPosition(0, new Vector3(_bird.StartPosition.x, _bird.StartPosition.y, (float)-0.1));
            _lineRenderer.SetPosition(1, new Vector3(_bird.transform.position.x, _bird.transform.position.y, (float)-0.1));
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
