using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.src;
using UnityEngine;

public class Enemy : Character
{
    public Player Player { get; set; }
    
    private AStarPathfinder _pathfinder;

    private List<Vector2Int> _path = new List<Vector2Int>();
    
    private Coroutine _movementCoroutine = null;
    private Coroutine _recalculateCoroutine = null;

    private void Start()
    {
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
        if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
        while (true)
        {
            if (_isMoving)
            {
                yield return null;
                continue;
            }
            
            _path = _pathfinder.FindPath(PositionInLabyrinth, Player.PositionInLabyrinth);

            _movementCoroutine = StartCoroutine(MoveAlongPath());

            yield break;
        }
    }

    private IEnumerator MoveAlongPath()
    {
        foreach (var point in _path)
            yield return StartCoroutine(Move(point));
    }
}