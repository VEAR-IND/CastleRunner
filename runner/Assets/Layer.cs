using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Layer
{
    public string name;
    public int order;
    [SerializeField]        
    public Vector3 transform;
    public bool isParalaxeble;
    public List<Tile> tiles;
    public int chunkCount;    
}
