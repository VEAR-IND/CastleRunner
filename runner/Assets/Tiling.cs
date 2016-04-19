using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;	
	public float RightCorner = 0;
	public float LeftCorner = 0;
	public bool reverseScale = false;
    private bool reverse = false;
    private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform, myTransformParent;
    public List<Tile> tiles2;
    public List<GameObject> tiles;
    public int sortingOrder;
    GameObject curobj;

    public int showCount = 7;
    float positionX;
    
    public float tileBlendValue = 0.1f;
    List<GameObject> terains = new List<GameObject> { };

    void Awake ()
    {
       
		cam = Camera.main;
        myTransform = transform;
        myTransformParent = transform.GetComponentInParent<Transform>().transform;
        positionX = cam.GetComponentInParent<Transform>().transform.position.x;

        
    }

    private void MakeFirst(int showCount = -1)
    {
        Choose();
        MakeNewBuddy(-1);
        MakeNewBuddy(1);
        RightCorner = spriteWidth - tileBlendValue;        
        LeftCorner =  (spriteWidth *-1) + tileBlendValue;
    }

    private void Choose()
    {
        positionX = cam.GetComponentInParent<Transform>().transform.position.x;
        curobj = tiles[UnityEngine.Random.Range(0, tiles.Count)];        
        Renderer rd = curobj.GetComponent<Renderer>();
        spriteWidth = rd.bounds.size.x;
    }
  

    void Start ()
    {
        tiles = new List<GameObject> { };
        foreach (Tile t in tiles2)
        {
            GameObject temp = new GameObject(t.sprite.name);
            temp.AddComponent<SpriteRenderer>().sprite = t.sprite;
            temp.transform.SetParent(this.GetComponent<Transform>());
            temp.transform.localPosition = new Vector3(0, -1000, 0);
            temp.hideFlags = HideFlags.HideInHierarchy;
            
            if (sortingOrder == -1)
            {
                temp.AddComponent<BoxCollider2D>().gameObject.SetActive(true);
            }
            tiles.Add(temp);
        }

        MakeFirst();
    }

    void Update ()
    {
        Choose();
        float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
	    float edgeVisiblePositionRight = positionX - camHorizontalExtend;
        float edgeVisiblePositionLeft = positionX + camHorizontalExtend;

        Debug.DrawLine(new Vector3(myTransformParent.position.x, 0, 0), new Vector3(transform.position.x, 100, 0),Color.black);
        Debug.DrawLine(new Vector3(edgeVisiblePositionRight, 0, 0), new Vector3(edgeVisiblePositionRight, 100, 0), Color.blue);
        Debug.DrawLine(new Vector3(edgeVisiblePositionLeft, 0, 0), new Vector3(edgeVisiblePositionLeft, 100, 0), Color.yellow);
        Debug.DrawLine(new Vector3(RightCorner + myTransformParent.position.x, 0, 0), new Vector3(RightCorner + myTransformParent.position.x, 100, 0), Color.green);
        Debug.DrawLine(new Vector3(LeftCorner + myTransformParent.position.x, 0, 0), new Vector3(LeftCorner + myTransformParent.position.x, 100, 0), Color.red);

        if (RightCorner + myTransformParent.position.x <= edgeVisiblePositionRight +spriteWidth + offsetX)
		{
            MakeNewBuddy (1);
            RightCorner +=  spriteWidth -tileBlendValue;
		}
	    else if (LeftCorner + myTransformParent.position.x  >= edgeVisiblePositionLeft -spriteWidth - offsetX)
		{
            MakeNewBuddy (-1);
            LeftCorner -= spriteWidth +tileBlendValue;
		}        
    }

	void MakeNewBuddy (int rightOrLeft)
    {
        Vector3 newPosition;
        GameObject newBuddy; ;
        
        if (rightOrLeft > 0)
        {
            newPosition = new Vector3(RightCorner + spriteWidth/2 + myTransformParent.position.x - tileBlendValue, transform.position.y, myTransform.position.z);
            newBuddy = Instantiate(curobj, Vector3.zero, myTransform.rotation) as GameObject;
        }
        else
        {
            newPosition = new Vector3(LeftCorner - spriteWidth /2 + myTransformParent.position.x + tileBlendValue, transform.position.y, myTransform.position.z);
            newBuddy = Instantiate(curobj, Vector3.zero, myTransform.rotation) as GameObject;
        }
        newBuddy.transform.localPosition = newPosition;

        if (reverseScale)
        {
            if (reverse)
            {
                newBuddy.transform.localScale = new Vector3(newBuddy.transform.localScale.x * -1, newBuddy.transform.localScale.y, newBuddy.transform.localScale.z);
            }
            reverse = !reverse;            
		}

        newBuddy.transform.parent = this.GetComponentInParent<Transform>();
        newBuddy.GetComponent<Renderer>().sortingOrder = sortingOrder;

        newBuddy.hideFlags = HideFlags.None;
        if (rightOrLeft > 0)
        {
            terains.Insert(0,newBuddy); 
        }
        else
        {
            terains.Add(newBuddy);
        }
        if (terains.Count > showCount)
        {
            if(rightOrLeft > 0)
            {
                Destroy(terains[terains.Count-1]);
                LeftCorner += terains[terains.Count - 1].GetComponent<Renderer>().bounds.size.x;
                terains.RemoveAt(terains.Count - 1);
            }
            else
            {
                Destroy(terains[0]);                
                RightCorner -= terains[0].GetComponent<Renderer>().bounds.size.x;
                terains.RemoveAt(0);
            }
        }
    }
}
