using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math
{
    /// <summary>
    /// 获得线段与直线的交点
    /// </summary>
    public static int BetweenLineAndCircle(Vector2 circleCenter, float circleRadius, Vector2 startPos, Vector2 endPos, out Vector2 intersection1, out Vector2 intersection2) {
        if ((startPos  - circleCenter).magnitude < circleRadius && (endPos - circleCenter).magnitude < circleRadius) {
            //当起点合终点都在圆内时，说明没有交点，直接返回
            intersection1 = Vector2.zero;
            intersection2 = Vector2.zero;
            return 0;
        }
        float t;

        var dx = endPos.x - startPos.x;
        var dy = endPos.y - startPos.y;

        var a = dx * dx + dy * dy;
        var b = 2 * (dx * (startPos.x - circleCenter.x) + dy * (startPos.y - circleCenter.y));
        var c = (startPos.x - circleCenter.x) * (startPos.x - circleCenter.x) + (startPos.y - circleCenter.y) * (startPos.y - circleCenter.y) - circleRadius * circleRadius;

        var determinate = b * b - 4 * a * c;
        if ((a <= 0.0000001) || (determinate < -0.0000001)) {
            // No real solutions.
            intersection1 = Vector2.zero;
            intersection2 = Vector2.zero;
            return 0;
        }
        if (determinate < 0.0000001 && determinate > -0.0000001) {
            // One solution.
            t = -b / (2 * a);
            intersection1 = new Vector2(startPos.x + t * dx, startPos.y + t * dy);
            intersection2 = Vector2.zero;
            return 1;
        }

        // Two solutions.
        t = (float)((-b + Mathf.Sqrt(determinate)) / (2 * a));
        intersection1 = new Vector2(startPos.x + t * dx, startPos.y + t * dy);
        t = (float)((-b - Mathf.Sqrt(determinate)) / (2 * a));
        intersection2 = new Vector2(startPos.x + t * dx, startPos.y + t * dy);

        return 2;
    }

    /// <summary>
    /// 一次贝塞尔曲线
    /// </summary>
    public static Vector3 CalculateCubicBezierPointfor2C(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        //B(t) = (1-t)(1-t)P0+ 2 (1-t) tP1 + ttP2,   0 <= t <= 1 
        //二次贝塞尔曲线
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
