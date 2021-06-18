// Iker Gutierrez - https://github.com/ikergutierrezgr/unity-mouse-position

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameAxes
{
    XY,
    XZ,
    YZ
}

public static class GetMousePositionsUtils 
{

    //Static methods to access mouse World position

    #region 2D Camera
    public static Vector2 Get2DMouseWorldPositionWithAxes(Camera selectedCamera, GameAxes gameAxis)
    {
        Vector3 pos = Get2DMouseWorldPosition(Input.mousePosition, selectedCamera);
        switch (gameAxis)
        {
            case GameAxes.XY:
                return new Vector2(pos.x, pos.y);
            case GameAxes.XZ:
                return new Vector2(pos.x, pos.z);
            case GameAxes.YZ:
                return new Vector2(pos.y, pos.z);
        }
        return pos;
    }
    
    public static Vector3 Get2DMouseWorldPosition()
    {
        return Get2DMouseWorldPosition(Input.mousePosition, Camera.main);
    }
    public static Vector3 Get2DMouseWorldPosition(Camera selectedCamera)
    {
        return Get2DMouseWorldPosition(Input.mousePosition, selectedCamera);
    }
    public static Vector3 Get2DMouseWorldPosition(Vector3 screenPosition, Camera selectedCamera)
    {
        return selectedCamera.ScreenToWorldPoint(screenPosition);
    }
    #endregion

    #region 3D Camera
    public static Vector3 Get3DMouseWorldPosition()
    {
        LayerMask defaultMask = ~0;
        return Get3DMouseWorldPosition(Input.mousePosition, Camera.main, defaultMask);
    }
    public static Vector3 Get3DMouseWorldPosition(Camera selectedCamera)
    {
        LayerMask defaultMask = ~0;
        return Get3DMouseWorldPosition(Input.mousePosition, selectedCamera, defaultMask);
    }
    public static Vector3 Get3DMouseWorldPosition(Vector3 screenPosition, Camera selectedCamera, LayerMask layerMask)
    {
        Ray ray = selectedCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    #endregion
}
