using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeL : MonoBehaviour, IPipe
{
    Piping piping;
    Tilemap tilemap;
    SpriteRenderer sr;
    Sprite[] sprites;
    public Sprite[] normalSprites;
    public Sprite[] hotSprites;
    [SerializeField]
    byte state = 0;
    [SerializeField]
    bool isHot = false;
    [SerializeField]
    Material materialHotWater;
    Material materialRegularWater;

    public bool isInteractable = false;
    Color colorRegularWater;
    [SerializeField]
    List<Vector3Int> hotWaterDirs;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponentInParent<Tilemap>();
        piping = GetComponentInParent<Piping>();
        sprites = normalSprites;
    }

    void Start()
    {
        
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
                tilePos2.x += 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                break;
            case 1:
                tilePos.y -= 1;
                tilePos2.x += 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                break;
            case 2:
                tilePos.y -= 1;
                tilePos2.x -= 1;
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos));
                piping.path.Add(new KeyValuePair<Vector3Int, Vector3Int>(currentPos, tilePos2));
                break;
            default:
                tilePos.y += 1;
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

    public void ChangeMaterialHotWaterPipe()
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
