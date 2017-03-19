using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CalculateShapesMatch 
{

    //Вычисляем центры масс исходного многоугольника и нарисованного.
    //    Вычисляем ограничивающий прямоугольних для них.

    //    Приводим многоугольники к одному размеру и в одну точку.

    //    Проверяем была ли проведена линия вблизи мнимой вершины.
    //    Проверяем насколько точно проведены линии вдоль мнимых граней.


    public float CheckPattern(Vector2[] currentPatternVertices, Vector2[] comparebleVertices, float verticesAccuracy, float eps)
    {
        float successPercent = 100;

        Vector2 patternCenter = FindCentroid(currentPatternVertices);

        Rect patternBounds = Bounds(currentPatternVertices);

        Vector2 drawCenter = FindCentroid(comparebleVertices);

        Rect drawBounds = Bounds(comparebleVertices);

        float scaleW = patternBounds.width / drawBounds.width;
        float scaleH = patternBounds.height / drawBounds.height;

        float scale = (scaleH + scaleW) / 2;

        float dx = patternCenter.x - drawCenter.x;
        float dy = patternCenter.y - drawCenter.y;

        Vector2[] scaledMovedDraw = ScalePattern(comparebleVertices, scale).ToArray();

        Vector2 scaledDrawCenter = FindCentroid(scaledMovedDraw);

        float sdx = drawCenter.x - scaledDrawCenter.x;
        float sdy = drawCenter.y - scaledDrawCenter.y;

        scaledMovedDraw = MovePattern(scaledMovedDraw, sdx, sdy).ToArray();


        Vector2[] movedPattern = MovePattern(currentPatternVertices, -dx, -dy).ToArray();

        Vector2[] lowerPattern = ScaledMovedPattern(movedPattern, -eps);

        Vector2[] higherPattern = ScaledMovedPattern(movedPattern, eps);


        Vector2[] insPoints = CheckPointsAgainstPattern(higherPattern, scaledMovedDraw, true);

        Vector2[] badPoints = CheckPointsAgainstPattern(lowerPattern, scaledMovedDraw, false);

        Vector2[] resultPoints = insPoints.Except(badPoints).ToArray();

        
        successPercent = (resultPoints.Length/(float) scaledMovedDraw.Length) * 100;


        float checkingDistance = verticesAccuracy * (patternBounds.width + patternBounds.height) / 2; //если недостаточно близко к углам фигуры, то сокращаем результирующую точность
        if (!CheckVertices(movedPattern, scaledMovedDraw, checkingDistance))
        {
            successPercent /= 20;
        }

        return successPercent;
    }

    public Vector2 FindCentroid(Vector2[] points)
    {
        List<Vector2> pts = new List<Vector2>(points) {points[0]};

        int num_points = points.Length;

        // Find the centroid.
        float X = 0;
        float Y = 0;
        float second_factor;
        for (int i = 0; i < num_points; ++i)
        {
            second_factor =
                pts[i].x * pts[i + 1].y -
                pts[i + 1].x * pts[i].y;
            X += (pts[i].x + pts[i + 1].x) * second_factor;
            Y += (pts[i].y + pts[i + 1].y) * second_factor;
        }

        // Divide by 6 times the polygon's area.
        float polygon_area = PolygonArea(pts);
        X /= (6 * polygon_area);
        Y /= (6 * polygon_area);

        // If the values are negative, the polygon is
        // oriented counterclockwise so reverse the signs.
        if (X < 0)
        {
            X = -X;
            Y = -Y;
        }

        return new Vector2(X, Y);
    }

    public float PolygonArea(List<Vector2> points)
    {
        var area = Math.Abs(points.Take(points.Count - 1)
           .Select((p, i) => (points[i + 1].x - p.x) * (points[i + 1].y + p.y))
           .Sum() / 2);

        return area;
    }

    private Rect Bounds(Vector2[] points)
    {
        float x = points[0].x;
        float y = points[0].y;

        float maxX = points[0].x;
        float maxY = points[0].y;

        float width;
        float height;

        for (int i = 0; i < points.Length; ++i)
        {
            if (points[i].x < x)
                x = points[i].x;

            if (points[i].y < y)
                y = points[i].y;

            if (points[i].x > maxX)
                maxX = points[i].x;

            if (points[i].y > maxY)
                maxY = points[i].y;
        }

        width = maxX - x;
        height = maxY - y;

        Rect rect = new Rect(x, y, width, height);

        return rect;
    }

    List<Vector2> ScalePattern(Vector2[] points, float coef)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < points.Length; ++i)
        {
            newList.Add(points[i] * coef);
        }

        return newList;
    }

    List<Vector2> MovePattern(Vector2[] points, float dx, float dy)
    {
        List<Vector2> newList = new List<Vector2>();

        for (int i = 0; i < points.Length; ++i)
        {
            newList.Add(points[i] + new Vector2(dx, dy));
        }

        return newList;
    }

    Vector2[] ScaledMovedPattern(Vector2[] startPoints, float percent)
    {
        List<Vector2> scaledPattern = ScalePattern(startPoints, 1 + percent);

        Vector2 startCentr = FindCentroid(startPoints);
        Vector2 scaledCentr = FindCentroid(scaledPattern.ToArray());

        float dx = startCentr.x - scaledCentr.x;
        float dy = startCentr.y - scaledCentr.y;

        Vector2[] movedPattern = MovePattern(scaledPattern.ToArray(), dx, dy).ToArray();

        return movedPattern;
    }

    private Vector2[] CheckPointsAgainstPattern(Vector2[] pattern, Vector2[] pointsToCheck, bool checkInside)
    {
        List<Vector2> tPattern = new List<Vector2>(pattern) {pattern[0]};

        List<Vector2> goodPoints = new List<Vector2>();

        for (int i = 0; i < tPattern.Count - 1; ++i)
        {
            for (int j = 0; j < pointsToCheck.Length; ++j)
            {
                float val = (pointsToCheck[j].y - tPattern[i].y) * (tPattern[i + 1].x - tPattern[i].x) -
                            (pointsToCheck[j].x - tPattern[i].x) * (tPattern[i + 1].y - tPattern[i].y);

                if (!goodPoints.Contains(pointsToCheck[j]))
                {
                    if (checkInside)
                    {
                        if (val <= 0)
                        {
                            goodPoints.Add(pointsToCheck[j]);
                        }
                    }
                    else
                    {
                        if (val >= 0)
                        {
                            goodPoints.Add(pointsToCheck[j]);
                        }
                    }
                }
            }
        }

        return goodPoints.ToArray();
    }

    private bool CheckVertices(Vector2[] pattern, Vector2[] pointsToCheck, float checkingDistance)
    {
        int number = 0;

        for (int i = 0; i < pattern.Length; ++i)
        {
            for (int j = 0; j < pointsToCheck.Length; ++j)
            {
                if (Vector2.Distance(pattern[i], pointsToCheck[j]) < checkingDistance)
                {
                    ++number;
                    break;
                }
            }
        }

        return number == pattern.Length;
    }

}
