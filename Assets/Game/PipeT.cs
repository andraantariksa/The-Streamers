﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeT : MonoBehaviour
{
    Piping piping;
    Tilemap tilemap;
    SpriteRenderer sr;
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    byte state = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponentInParent<Tilemap>();
        piping = GetComponentInParent<Piping>();

        SetupPathingAndSprite();
    }

    void SetupPathingAndSprite()
    {
        var currentPos = tilemap.WorldToCell(transform.position);

        var tilePos = tilemap.WorldToCell(transform.position);
        var tilePos2 = tilemap.WorldToCell(transform.position);
        var tilePos3 = tilemap.WorldToCell(transform.position);

        piping.DeletePathRouteFrom(currentPos);
        switch (state)
        {
            case 0:
                tilePos.y += 1;
                tilePos2.x += 1;
                tilePos3.y -= 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
                break;
            case 1:
                tilePos.x += 1;
                tilePos2.y -= 1;
                tilePos3.x -= 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
                break;
            case 2:
                tilePos.y -= 1;
                tilePos2.x -= 1;
                tilePos3.y += 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
                break;
            default:
                tilePos.x -= 1;
                tilePos2.y += 1;
                tilePos3.x += 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
                break;
        }
        sr.sprite = sprites[state];
    }

    void OnMouseDown()
    {
        ChangeState();
        SetupPathingAndSprite();

        piping.OnPipeChange();
    }

    void ChangeState()
    {
        state = (byte)((state + 1) % sprites.Length);
    }
}
