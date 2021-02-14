﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeStraight : MonoBehaviour, IPipe
{
    Piping piping;
    Tilemap tilemap;
    SpriteRenderer sr;
    Sprite[] sprites;
    public Sprite[] normalSprites;
    public Sprite[] hotSprites;
    public Sprite[] heaterSprites;
    public Sprite[] rotatingSprites;

    [SerializeField]
    byte state = 0;
    [SerializeField]
    public bool isHot = false;
    [SerializeField]
    Material materialHotWater;
    Material materialRegularWater;
    public bool isInteractable = false;
    Color colorRegularWater;
    [SerializeField]
    List<Vector3Int> hotWaterDirs;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponentInParent<Tilemap>();
        piping = GetComponentInParent<Piping>();

        if (hotWaterDirs.Count != 0)
        {
            sprites = heaterSprites;
        }
        else
        {
            sprites = normalSprites;
        }
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

        piping.DeletePathRouteFrom(currentPos);
        switch (state)
        {
            case 0:
                tilePos.y += 1;
                tilePos2.y -= 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                break;
            default:
                tilePos.x += 1;
                tilePos2.x -= 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                break;
        }
        sr.sprite = sprites[state];
    }

    void OnMouseDown()
    {
        if (isInteractable)
        {
            ChangeState();
            SetupPathingAndSprite();

            piping.OnPipeChange();
        }
    }

    void ChangeState()
    {
        state = (byte)((state + 1) % sprites.Length);
    }

    public void SetHotWaterPipe(bool isHot)
    {
        this.isHot = isHot;
        ChangeMaterialHotWaterPipe();
    }

    public void ChangeMaterialHotWaterPipe() //Ngatur ubah warna
    {
        if (hotWaterDirs.Count == 0)
        {
            if (isHot)
            {
                sprites = hotSprites;
                // sr.material = materialHotWater;
            }
            else
            {
                sprites = normalSprites;
                // sr.material = materialRegularWater;
            }
        }
        
        sr.sprite = sprites[state];
    }

    public List<Vector3Int> GetHotWaterDir()
    {
        return hotWaterDirs;
    }
}
