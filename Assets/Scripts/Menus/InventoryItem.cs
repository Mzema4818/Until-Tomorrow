using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    public Transform dropableParent;
    public Transform player;
    public Transform root;

    [HideInInspector] public InventoryManager inventoryManager;
    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform ParentAfterDrag;

    [HideInInspector] public bool ClickedLeft;
    [HideInInspector] public bool ClickedRight;

    [HideInInspector] public GameObject halfStack;

    private int originalAmount;
    private bool canRemove;

    private void Awake()
    {
        if(inventoryManager == null && transform.parent.GetComponent<InventorySlot>() != null) inventoryManager = transform.parent.GetComponent<InventorySlot>().inventoryManager;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && canRemove)
        {
            ItemWorld.DropItem(player, item, dropableParent);
            count--;
            //if user is holding an item that shouldnt exist anymore, remove it from hand
            if (count == 0 && transform.parent.GetSiblingIndex() == inventoryManager.selectedSlot) inventoryManager.ListOfHeldItems[inventoryManager.ReturnNumByItem(item.itemType)].SetActive(false);
            RefreshCount();
        }
    }

    public void DropItem()
    {
        ItemWorld.DropItem(player, item, dropableParent);
        count--;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canRemove = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canRemove = false;
    }

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.getSprite();
        RefreshCount();
    }

    public void RefreshCount()
    {
        if (count <= 0) Destroy(gameObject);

        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !inventoryManager.DraggingItem)
        {
            ParentAfterDrag = transform.parent;
            transform.SetParent(root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

            inventoryManager.DraggingItem = true;
            inventoryManager.ItemDragged = transform.gameObject;
            ClickedLeft = true;
        }

        if (Input.GetMouseButton(1) && !inventoryManager.DraggingItem)
        {
            //print(Mathf.CeilToInt((float)count / 2)); original
            //print(Mathf.FloorToInt((float)count / 2)); halfstack
            if (count > 1)
            {
                originalAmount = count;
                count = Mathf.CeilToInt((float)originalAmount / 2);
                RefreshCount();

                halfStack = Instantiate(gameObject, root);
                halfStack.name = FixName(halfStack.name);
                halfStack.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt((float)originalAmount / 2).ToString();
                Destroy(halfStack.GetComponent<InventoryItem>());
                halfStack.transform.SetAsLastSibling();
                halfStack.GetComponent<Image>().raycastTarget = false;
                halfStack.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize.x, transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize.y);

                inventoryManager.DraggingItem = true;
                inventoryManager.ItemDragged = halfStack;
                ClickedRight = true;
            }
            else
            {
                ParentAfterDrag = transform.parent;
                transform.SetParent(root);
                transform.SetAsLastSibling();
                image.raycastTarget = false;

                inventoryManager.DraggingItem = true;
                inventoryManager.ItemDragged = transform.gameObject;
                ClickedLeft = true;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        if (ClickedLeft) transform.position = Input.mousePosition;
        if (ClickedRight) halfStack.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        //print(transform.localScale);
        if (ClickedLeft)
        {
            transform.SetParent(ParentAfterDrag);
            image.raycastTarget = true;

            ClickedLeft = false;
            inventoryManager.ItemDragged = null;
            inventoryManager.DraggingItem = false;
        }

        if (ClickedRight)
        {
            if (halfStack != null)
            {
                Destroy(halfStack);
                count = originalAmount;
                originalAmount = 0;
                RefreshCount();
            }
            ClickedRight = false;
            inventoryManager.ItemDragged = null;
            inventoryManager.DraggingItem = false;
        }
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}