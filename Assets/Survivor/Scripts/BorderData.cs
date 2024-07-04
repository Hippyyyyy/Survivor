using UnityEngine;

public struct BorderData{
	public Vector2 CenterPos;

	public float HalfWidth;

	public float HalfHeight;

    public BorderData(Vector2 centerPos, float halfWidth, float halfHeight)
    {
        CenterPos = centerPos;
        HalfWidth = halfWidth;
        HalfHeight = halfHeight;
    }
}
