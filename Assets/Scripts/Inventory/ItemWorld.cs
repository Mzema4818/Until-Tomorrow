using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    public Transform parentObjectInspector;
    private static Transform parentObject;
    private bool isPickedUp = false;

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item, Transform parent)
    {
        Transform transform = Instantiate(item.GetMesh(), position, Quaternion.identity);
        transform.name = FixName(transform.name);
        transform.parent = parent;

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Transform dropPosition, Item item, Transform parent)
    {
        Vector3 offset = dropPosition.forward * 3;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition.localPosition + offset + new Vector3(0,4,0), item, parent);
        itemWorld.GetComponent<Rigidbody>().AddForce(offset * 2f, ForceMode.Impulse);

        return itemWorld;
    }

    private Item item;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Transform canvas;
    private GameObject image;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        //meshFilter = GetComponent<MeshFilter>();
        //meshRenderer = GetComponent<MeshRenderer>();
        //parentObject = parentObjectInspector;
        //canvas = transform.Find("Canvas");
        //image = transform.Find("Canvas").Find("CanvasHolder").Find("Image").gameObject;
        //textMeshPro = transform.Find("Canvas").Find("CanvasHolder").Find("Image").Find("amount").GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        //meshFilter.mesh = item.getMesh();
        //meshRenderer.material.color = item.getColor();
        //transform.DetachChildren();
        //transform.localScale = item.getSize();
        //canvas.parent = transform;
        //canvas.localScale = new Vector3(0.45f, 1.8f, 0.1f);
        //transform.name = item.getName();

        //if (item.amount > 1)
        //{
            //image.SetActive(true);
            //textMeshPro.SetText(item.amount.ToString());
        //}
        //else
        //{
           // textMeshPro.SetText("");
            //image.SetActive(false);
        //}
    }
    public Item GetItem()
    {
        return item;
    }

    public void SetAmount(int amount)
    {
        //item.amount = amount;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private static string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }

    public bool TryPickUp()
    {
        if (isPickedUp) return false;

        isPickedUp = true;
        return true;
    }
}
