using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Assets.src;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Enemy : Character
{
    public Player Player { get; set; }
    
    private AStarPathfinder _pathfinder;

    private List<Vector2Int> _path = new List<Vector2Int>();
    
    private PathDrawer _pathDrawer => _gameManager.PathDrawer;
    
    private Coroutine _movementCoroutine = null;
    private Coroutine _recalculateCoroutine = null;

    private Vector2Int _nextPoint;
    
    private void Start()
    {
        _nextPoint = PositionInLabyrinth;
        _pathfinder = new AStarPathfinder(_labyrinth);
        
        Player.OnMoved.AddListener(StartRecalculatePath);
    }

    private void StartRecalculatePath()
    {
        if (_recalculateCoroutine != null) StopCoroutine(_recalculateCoroutine);
        _recalculateCoroutine = StartCoroutine(RecalculatePath());
    }

    private IEnumerator RecalculatePath()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _path = _pathfinder.FindPath(_nextPoint, Player.PositionInLabyrinth);
        stopwatch.Stop();
        Debug.Log(stopwatch.ElapsedMilliseconds);
        stopwatch.Reset();
        
        _pathDrawer.DrawPath(_labyrinth, _path);
        if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
        while (true)
        {
            if (_isMoving)
            {
                yield return null;
                continue;
            }

            _movementCoroutine = StartCoroutine(MoveAlongPath());
            yield break;
        }
    }

    private IEnumerator MoveAlongPath()
    {
        foreach (var point in _path)
        {
            if (point == PositionInLabyrinth) continue;
            _nextPoint = point;
            yield return StartCoroutine(Move(point));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            _gameManager.OnLose.Invoke();
        }
    }
}