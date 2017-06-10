using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class ParallaxObject : MonoBehaviour 
{
    public Camera connectedCamera;

    public float xSpeed = 0;
    public float ySpeed = 0;

    public float cameraSizeRatio = 1;

    public bool resetPosition;
    public bool updatePosition = false;

    public Vector3 centerPosition;
    public Vector3 cameraCenterPosition;

	void Start ()
    {
        if (connectedCamera == null)
            connectedCamera = Camera.main;

        if (centerPosition == Vector3.zero && cameraCenterPosition == Vector3.zero)
            GetNewParallaxPosition();	
	}
	
    void GetNewParallaxPosition()
    {
        centerPosition = transform.position;
        cameraCenterPosition = connectedCamera.transform.position;
    }

    void UpdatePosition()
    {
        Vector3 _newPos = Vector3.zero;
        Vector3 _camPos = connectedCamera.transform.position;

        float _divideByScale = Mathf.Log(connectedCamera.orthographicSize);
        _divideByScale = Mathf.Log10(connectedCamera.orthographicSize);
        _divideByScale = Mathf.Log(connectedCamera.orthographicSize, 2);

        _newPos.x = centerPosition.x + ((_camPos.x - cameraCenterPosition.x) * (xSpeed / _divideByScale ));
        _newPos.y = centerPosition.y + ((_camPos.y - cameraCenterPosition.y) * (ySpeed / _divideByScale));
        _newPos.z = centerPosition.z;

        transform.position = _newPos;

        updatePosition = false;
    }


	void LateUpdate ()
    {

        #if UNITY_EDITOR

        if (resetPosition)
        {

            if(Selection.activeGameObject != gameObject)
                resetPosition = false;
            GetNewParallaxPosition();
        }

        if (Selection.activeTransform == transform)
            return;

        #endif

        if (Application.isPlaying || updatePosition)
            UpdatePosition();
	}
}
