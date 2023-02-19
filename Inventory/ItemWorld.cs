using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    public Transform parentObjectInspector;
    private static Transform parentObject;

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(item.GetMesh(), position, Quaternion.identity);
        transform.name = FixName(transform.name);
        transform.parent = parentObject;

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item, Quaternion quaternion)
    {
        Transform transform = Instantiate(item.GetMesh(), position, quaternion);
        transform.name = FixName(transform.name);
        transform.parent = parentObject;

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public static ItemWorld DropItem(Transform dropPosition, Item item)
    {
        Vector3 offset = dropPosition.forward * 3;
        ItemWorld itemWorld = SpawnItemWorld(dropPosition.localPosition + offset + new Vector3(0,4,0), item);
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
        parentObject = parentObjectInspector;
        canvas = transform.Find("Canvas");
        image = transform.Find("Canvas").Find("CanvasHolder").Find("Image").gameObject;
        textMeshPro = transform.Find("Canvas").Find("CanvasHolder").Find("Image").Find("amount").GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        //meshFilter.mesh = item.getMesh();
        //meshRenderer.material.color = item.getColor();
        //transform.DetachChildren();
        //transform.localScale = item.getSize();
        //canvas.parent = transform;
        canvas.localScale = new Vector3(0.45f, 1.8f, 0.1f);
        //transform.name = item.getName();

        if (item.amount > 1)
        {
            image.SetActive(true);
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
            image.SetActive(false);
        }
    }
    public Item GetItem()
    {
        return item;
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
}
