using System.Collections.Generic;
using UnityEngine;

public interface IWallGenerator
{
    List<GameObject> Generate(Vector3 size, float difficulty, List<Color32> colors);
}
