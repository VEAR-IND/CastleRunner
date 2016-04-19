using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BiomsGenerator : MonoBehaviour
{
    public GameObject bioms;
    BiomsDatabase biomsDatabase;

    public Biom selectedOrDefault;

    // Use this for initialization

    void Awake()
    {
        biomsDatabase = bioms.GetComponent("BiomsDatabase") as BiomsDatabase;
        Chose();
        CreateSkeleton();
    }
	void Start ()
    {
        
    }
    void Update()
    {
        int selectedIndex = (PlayerPrefs.GetInt("BiomSelected", -1));
        if (selectedIndex != selectedOrDefault.index && selectedIndex !=-1)
        {
            Chose();
        }
        else if(selectedIndex == -1 && !selectedOrDefault.isDefault)
        {
            Chose();
        }
    }
    void Chose()
    {
        int selectedIndex = (PlayerPrefs.GetInt("BiomSelected",-1));
        foreach (Biom b in biomsDatabase.bioms)
        {
            if (selectedIndex == -1)
            {
                if (b.isDefault)
                {
                    selectedOrDefault = b;
                    break;
                }
            }
            else
            {
                if (b.index == selectedIndex)
                {
                    selectedOrDefault = b;
                    break;
                }
            }
        }
    }	
    void CreateSkeleton()
    {
        
        GameObject biom = new GameObject("biom");
        biom.AddComponent<Parallaxing>().gameObject.SetActive(false);

        List <GameObject> layers = new List<GameObject>() { };
        List <Transform> transforms = new List<Transform>() { };
        foreach (Layer l in selectedOrDefault.layers)
        {            
            GameObject tempLayer = new GameObject("B"+l.name);
            GameObject tempLayerChild = new GameObject(l.name );
            tempLayer.transform.SetParent(biom.transform);
            tempLayer.transform.position = new Vector3(0, 0, l.transform.z);
            tempLayerChild.AddComponent<Tiling>().tiles2 = l.tiles;
            tempLayerChild.GetComponent<Tiling>().gameObject.SetActive(true);
            tempLayerChild.GetComponent<Tiling>().sortingOrder = l.order;
            tempLayerChild.transform.SetParent(tempLayer.transform);
            tempLayerChild.transform.localPosition = new Vector3(0, l.transform.y, 0);
            
            layers.Add(tempLayer);
            if (l.isParalaxeble)
            {
                transforms.Add(tempLayer.transform);
            }
            
        }
        biom.GetComponent<Parallaxing>().backgrounds = transforms;
        biom.GetComponent<Parallaxing>().gameObject.SetActive(true);

    }
}
