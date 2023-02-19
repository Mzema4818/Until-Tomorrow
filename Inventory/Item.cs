using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public enum ItemType
    {
        rock,
        wood,
        flower,
        mushroom,
        berry,
        bush,
        sapling,
    }

    public ItemType itemType;
    public int amount;

    public Sprite getSprite()
    {
        switch (itemType){
            default:
            case ItemType.rock: return ItemAssets.Instance.stoneSprite;
            case ItemType.wood: return ItemAssets.Instance.woodSprite;
            case ItemType.mushroom: return ItemAssets.Instance.mushroomSprite;
            case ItemType.flower: return ItemAssets.Instance.flowerSprite;
            case ItemType.berry: return ItemAssets.Instance.berrySprite;
            case ItemType.bush: return ItemAssets.Instance.bushSprite;
            case ItemType.sapling: return ItemAssets.Instance.saplingSprite;
        }
    }

    public Transform GetMesh()
    {
        switch (itemType)
        {
            default:
            case ItemType.rock: return ItemAssets.Instance.pfItemWorld[0];
            case ItemType.wood: return ItemAssets.Instance.pfItemWorld[1];
            case ItemType.mushroom: return ItemAssets.Instance.pfItemWorld[2];
            case ItemType.flower: return ItemAssets.Instance.pfItemWorld[3];
            case ItemType.berry: return ItemAssets.Instance.pfItemWorld[4];
            case ItemType.bush: return ItemAssets.Instance.pfItemWorld[5];
            case ItemType.sapling: return ItemAssets.Instance.pfItemWorld[6];
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.rock:
            case ItemType.wood:
            case ItemType.flower:
            case ItemType.mushroom:
            case ItemType.berry:
            case ItemType.bush:
            case ItemType.sapling:
                return true;
            //case ItemType.mushroom:
                //return false;
        }
    }

    public bool IsEquipable()
    {
        switch (itemType)
        {
            default:
            case ItemType.berry:
            case ItemType.bush:
            case ItemType.sapling:
                return true;
            case ItemType.rock:
            case ItemType.wood:
            case ItemType.flower:
            case ItemType.mushroom:
                return false;
        }
    }

    public int EquipableAction()
    {
        switch (itemType)
        {
            default:
            case ItemType.berry: return 0;
            case ItemType.bush: return 1;
            case ItemType.sapling: return 2;
            case ItemType.rock: return -1;
            case ItemType.wood: return -1;
            case ItemType.flower: return -1;
            case ItemType.mushroom: return -1;
        }
    }
}
