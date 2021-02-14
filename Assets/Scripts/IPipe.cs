using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface IPipe
{
    Vector3 worldPos();
    void SetHotWaterPipe(bool isHot);
    void ChangeMaterialHotWaterPipe();
};
