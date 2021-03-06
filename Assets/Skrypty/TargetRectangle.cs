﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRectangle : MonoBehaviour {

    public GameObject target;
    public float margin = 10;
    public bool draw = false;
    public Texture2D background;
    public float[] box_coord = new float[4];

    private GUIStyle style = null;
    private Vector3[] pts = new Vector3[8];

    public void OnGUI()
    {
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.box);
            style.normal.background = background;
        }
        Bounds bounds = target.GetComponent<Renderer>().bounds;
        Camera cam = Camera.main;
        // obiekt jest niewidoczny
        if (cam.WorldToScreenPoint(bounds.center).z < 0) return;
        // 8 wspolrzednych 
        pts[0] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
        pts[1] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
        pts[2] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
        pts[3] = cam.WorldToScreenPoint(new Vector3(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));
        pts[4] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z + bounds.extents.z));
        pts[5] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y, bounds.center.z - bounds.extents.z));
        pts[6] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z + bounds.extents.z));
        pts[7] = cam.WorldToScreenPoint(new Vector3(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y, bounds.center.z - bounds.extents.z));
        // odpowiednie wspolrzedne na ekranie
        for (int i = 0; i < pts.Length; i++) pts[i].y = Screen.height - pts[i].y;
        // maksymalne i minimalne wartosci
        Vector3 min = pts[0];
        Vector3 max = pts[0];
        for (int i = 1; i < pts.Length; i++)
        {
            min = Vector3.Min(min, pts[i]);
            max = Vector3.Max(max, pts[i]);
        }
        box_coord[0] = min.x;
        box_coord[1] = min.y;
        box_coord[2] = max.x;
        box_coord[3] = max.y;
        // prostokat
        if (draw)
        {
            Rect r = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
            r.xMin -= margin;
            r.xMax += margin;
            r.yMin -= margin;
            r.yMax += margin;
            // 'box'
            GUI.Box(r, "", style);
        }
    }
}


