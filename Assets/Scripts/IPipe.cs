using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IPipe
{
    Vector3 worldPos();
    List<Vector3Int> GetHotWaterDir();
    void SetHotWaterPipe(bool isHot);
    void ChangeMaterialHotWaterPipe();
};
