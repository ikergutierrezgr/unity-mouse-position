// Iker Gutierrez - https://github.com/ikergutierrezgr/unity-mouse-position
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [Header("3D Camera Property")]
    [SerializeField] private LayerMask _layerMask;

    [HideInInspector] public static GetMousePosition Instance { get; private set;}

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (!_mainCamera){ _mainCamera = Camera.main; }
    }

    #region 2D Camera
    public Vector3 Get2DMousePosition() => Instance.Calculate2DMousePosition();

    private Vector3 Calculate2DMousePosition()
    {
        if(!_mainCamera.orthographic)
        {
            Debug.LogError("You must select an ortographic camera.");
            return Vector3.zero;
        }
        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion

    #region 3D Camera
    public  Vector3 Get3DMousePosition() => Instance.Calculate3DMousePosition();

    private Vector3 Calculate3DMousePosition()
    {
        if (_mainCamera.orthographic)
        {
            Debug.LogError("You must select a perspective camera.");
            return Vector3.zero;
        }
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _layerMask))
        {
            return raycastHit.point;
        } else
        {
            return Vector3.zero;
        }
    }
    #endregion
}
