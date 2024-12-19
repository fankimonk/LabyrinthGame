using System;
using System.Collections.Generic;
using Assets.src;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    [SerializeField] private GameObject _cellPrefab;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawPath(Labyrinth _labyrinth, List<Vector2Int> path)
    {
        Reset();
        _lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            var point = _labyrinth.GetCell(path[i]).Position;
            _lineRenderer.SetPosition(i, new Vector3(point.x, _cellPrefab.transform.localScale.y * 2f, point.y));
        }
    }

    public void ToggleHidePath()
    {
        _lineRenderer.enabled = !_lineRenderer.enabled;
    }
    
    public void HidePath()
    {
        _lineRenderer.enabled = false;
    }

    public void ShowPath()
    {
        _lineRenderer.enabled = true;
    }
    
    public void Reset()
    {
        _lineRenderer.positionCount = 0;
    }
}
