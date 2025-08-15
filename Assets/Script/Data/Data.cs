using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDetails 
{
    public int itemID;
    public string name;
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDesciption;
    public int itemPrice;
    public ItemType itemType;

}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}

[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x=pos.x;
        this.y=pos.y;
        this.z=pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }

    

}

[System.Serializable]
public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}
