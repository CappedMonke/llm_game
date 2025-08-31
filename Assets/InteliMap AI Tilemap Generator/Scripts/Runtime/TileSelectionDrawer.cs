using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileSelectionDrawer
{
    public static void DrawBounds(Tilemap map, Vector3Int a, Vector3Int b, Action<Vector3, Vector3> drawLine)
    {
        Vector3 sizeX;
        Vector3 sizeY;
        Vector3 offset = Vector3.zero;
        if (map.cellLayout == GridLayout.CellLayout.Hexagon)
        {
            if (map.cellSwizzle == GridLayout.CellSwizzle.XYZ)
            {
                offset = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(-map.cellSize.x * 0.5f, -map.cellSize.y * 0.5f));
                sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x, 0));
                sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.y));
            }
            else // yxz
            {
                offset = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(-map.cellSize.y * 0.5f, -map.cellSize.x * 0.5f));
                sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.y, 0));
                sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.x));
            }
        }
        else if (map.cellLayout == GridLayout.CellLayout.Isometric || map.cellLayout == GridLayout.CellLayout.IsometricZAsY)
        {
            sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x * 0.5f, map.cellSize.y * 0.5f));
            sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x * -0.5f, map.cellSize.y * 0.5f));
        }
        else
        {
            sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x, 0));
            sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.y));
        }

        Vector3Int min = Vector3Int.Min(a, b);
        Vector3Int max = Vector3Int.Max(a, b);

        DrawRect(map,
            min,
            new Vector3Int(min.x, max.y),
            max,
            new Vector3Int(max.x, min.y),
            map.orientationMatrix.MultiplyVector(sizeX),
            map.orientationMatrix.MultiplyVector(sizeY),
            offset,
            drawLine);
    }

    public static void DrawBounds(Tilemap map, BoundsInt bounds, Action<Vector3, Vector3> drawLine)
    {
        Vector3 sizeX;
        Vector3 sizeY;
        Vector3 offset = Vector3.zero;
        if (map.cellLayout == GridLayout.CellLayout.Hexagon)
        {
            if (map.cellSwizzle == GridLayout.CellSwizzle.XYZ)
            {
                offset = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(-map.cellSize.x * 0.5f, -map.cellSize.y * 0.5f));
                sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x, 0));
                sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.y));
            }
            else // yxz
            {
                offset = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(-map.cellSize.y * 0.5f, -map.cellSize.x * 0.5f));
                sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.y, 0));
                sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.x));
            }
        }
        else if (map.cellLayout == GridLayout.CellLayout.Isometric || map.cellLayout == GridLayout.CellLayout.IsometricZAsY)
        {
            sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x * 0.5f, map.cellSize.y * 0.5f));
            sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x * -0.5f, map.cellSize.y * 0.5f));
        }
        else
        {
            sizeX = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(map.cellSize.x, 0));
            sizeY = map.transform.localToWorldMatrix.MultiplyVector(new Vector3(0, map.cellSize.y));
        }

        DrawRect(map,
            bounds.min,
            bounds.min + new Vector3Int(0, bounds.size.y - 1),
            bounds.max - Vector3Int.one,
            bounds.min + new Vector3Int(bounds.size.x - 1, 0),
            map.orientationMatrix.MultiplyVector(sizeX),
            map.orientationMatrix.MultiplyVector(sizeY),
            offset,
            drawLine);
    }

    private static void DrawRect(Tilemap map, Vector3Int a, Vector3Int b, Vector3Int c, Vector3Int d, Vector3 sizeX, Vector3 sizeY, Vector3 offset, Action<Vector3, Vector3> drawLine)
    {
        Vector3 aPos = map.layoutGrid.CellToLocal(a) + offset;
        Vector3 bPos = map.layoutGrid.CellToLocal(b) + offset;
        Vector3 cPos = map.layoutGrid.CellToLocal(c) + offset;
        Vector3 dPos = map.layoutGrid.CellToLocal(d) + offset;

        Vector3 size = sizeX + sizeY;

        if (a.x <= c.x)
        {
            if (a.y <= c.y)
            {
                drawLine(aPos, bPos + sizeY);
                drawLine(dPos + sizeX, cPos + size);
            }
            else
            {
                drawLine(bPos, aPos + sizeY);
                drawLine(cPos + sizeX, dPos + size);
            }
        }
        else
        {
            if (a.y <= c.y)
            {
                drawLine(aPos + sizeX, bPos + size);
                drawLine(dPos, cPos + sizeY);
            }
            else
            {
                drawLine(bPos + sizeX, aPos + size);
                drawLine(cPos, dPos + sizeY);
            }
        }

        if (a.y <= c.y)
        {
            if (a.x <= c.x)
            {
                drawLine(bPos + sizeY, cPos + size);
                drawLine(aPos, dPos + sizeX);
            }
            else
            {
                drawLine(cPos + sizeY, bPos + size);
                drawLine(dPos, aPos + sizeX);
            }
        }
        else
        {
            if (a.x <= c.x)
            {
                drawLine(bPos, cPos + sizeX);
                drawLine(aPos + sizeY, dPos + size);
            }
            else
            {
                drawLine(cPos, bPos + sizeX);
                drawLine(dPos + sizeY, aPos + size);
            }
        }
    }
}
