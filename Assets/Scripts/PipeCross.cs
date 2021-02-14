﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeCross : MonoBehaviour, IPipe
{
    Piping piping;
    Tilemap tilemap;
    SpriteRenderer sr;
    [SerializeField]
    Sprite[] sprites;
    public Sprite[] rotatingSprites;

    [SerializeField]
    bool isHot = false;
    [SerializeField]
    Material materialHotWater;
    Material materialRegularWater;
    Color colorRegularWater;
    [SerializeField]
    List<Vector3Int> hotWaterDirs;
    
    public bool isInteractable = false;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponentInParent<Tilemap>();
        piping = GetComponentInParent<Piping>();
        if (isInteractable)
        {
            sprites = rotatingSprites;
        }
        SetupPathingAndSprite();
        materialRegularWater = sr.material;
        colorRegularWater = sr.color;
        ChangeMaterialHotWaterPipe();
    }

    public Vector3 worldPos()
    {
        return GetComponent<Transform>().position;
    }

    void SetupPathingAndSprite()
    {
        var currentPos = tilemap.WorldToCell(transform.position);

        var tilePos = tilemap.WorldToCell(transform.position);
        var tilePos2 = tilemap.WorldToCell(transform.position);
        var tilePos3 = tilemap.WorldToCell(transform.position);
        var tilePos4 = tilemap.WorldToCell(transform.position);

        piping.DeletePathRouteFrom(currentPos);

        tilePos.y += 1;
        tilePos2.x += 1;
        tilePos3.y -= 1;
        tilePos4.x -= 1;
        piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
        piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
        piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
        piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos4));

        // switch (state)
        // {
        //     case 0:
        //         tilePos.y += 1;
        //         tilePos2.x += 1;
        //         tilePos3.y -= 1;
        //         piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
        //         piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
        //         piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos3));
        //         break;
        // }
        // sr.sprite = sprites[state];
    }

    // void OnMouseDown()
    // {
    //     ChangeState();
    //     SetupPathingAndSprite();

    //     piping.OnPipeChange();
    // }

    // void ChangeState()
    // {
    //     state = (byte)((state + 1) % sprites.Length);
    // }

    public void SetHotWaterPipe(bool isHot)
    {
        this.isHot = isHot;
        ChangeMaterialHotWaterPipe();
    }

    public void ChangeMaterialHotWaterPipe()
    {
        if (isHot)
        {
            sr.color = Color.red;
            // sr.material = materialHotWater;
        }
        else
        {
            sr.color = colorRegularWater;
            // sr.material = materialRegularWater;
        }
    }

    public List<Vector3Int> GetHotWaterDir()
    {
        return hotWaterDirs;
    }
}
