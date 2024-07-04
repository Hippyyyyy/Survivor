using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] Vector2 minMaxXY;
    


    private void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPos = target.position;
        /*targetPos.x = Mathf.Clamp(targetPos.x, -minMaxXY.x, minMaxXY.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -minMaxXY.y, minMaxXY.y);*/
        targetPos.z = -10;
        transform.position = targetPos;
    }
}
