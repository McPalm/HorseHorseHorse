using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceDisplay
{
    void SetScale(int current, int max);
    void SetCurrent(int current);
    void Setmax(int max);
}
