using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonImage : Image
{
    private PolygonCollider2D mCollider;

    protected override void Awake()
    {
        base.Awake();
        mCollider = transform.GetOrAddComponent<PolygonCollider2D>();
    }

    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return ContainsPoint(mCollider.points, sp);
    }

    bool ContainsPoint(Vector2[] polyPoints, Vector2 p)
    {
        int j = polyPoints.Length - 1;
        bool inside = false;
        for (int i = 0 ; i < polyPoints. Length ; j = i++)
        {
            polyPoints [ i ]. x += transform. position. x;
            polyPoints [ i ]. y += transform. position. y;
            if (((polyPoints [ i ]. y <= p. y && p. y < polyPoints [ j ]. y) || (polyPoints [ j ]. y <= p. y && p. y < polyPoints [ i ]. y)) && (p. x < (polyPoints [ j ]. x - polyPoints [ i ]. x) * (p. y - polyPoints [ i ]. y) / (polyPoints [ j ]. y - polyPoints [ i ]. y) + polyPoints [ i ]. x))
                inside = !inside;
        }

        return inside;
    }
}