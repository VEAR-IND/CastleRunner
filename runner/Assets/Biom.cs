using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Biom
{
    public string name;
    public int index;
    public int prise;
    public bool isDefault;
    public bool isPremium;
    public bool isBuyed;
    public bool isLocked;
    public Sprite icon;

    public List<Layer> layers;
    public List<GameObject> enemies;
}
