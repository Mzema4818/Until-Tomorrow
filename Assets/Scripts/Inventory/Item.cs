using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item
{
    public enum ItemType
    {
        empty,
        nothing,
        pickaxe,
        axe,
        hammer,
        sword,
        rock,
        wood,
        flower,
        mushroom,
        berry,
        bush,
        sapling,
        charredBerry,
    }

    public ItemType itemType;
    public int index;

    public Sprite getSprite()
    {
        switch (itemType){
            default:
            case ItemType.pickaxe: return ItemAssets.Instance.PickaxeSprite;
            case ItemType.axe: return ItemAssets.Instance.AxeSprite;
            case ItemType.hammer: return ItemAssets.Instance.HammerSprite;
            case ItemType.sword: return ItemAssets.Instance.SwordSprite;
            case ItemType.rock: return ItemAssets.Instance.stoneSprite;
            case ItemType.wood: return ItemAssets.Instance.woodSprite;
            case ItemType.mushroom: return ItemAssets.Instance.mushroomSprite;
            case ItemType.flower: return ItemAssets.Instance.flowerSprite;
            case ItemType.berry: return ItemAssets.Instance.berrySprite;
            case ItemType.bush: return ItemAssets.Instance.bushSprite;
            case ItemType.sapling: return ItemAssets.Instance.saplingSprite;
            case ItemType.charredBerry: return ItemAssets.Instance.charredBerrySprite;
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
            case ItemType.axe: return ItemAssets.Instance.pfItemWorld[7];
            case ItemType.pickaxe: return ItemAssets.Instance.pfItemWorld[8];
            case ItemType.sword: return ItemAssets.Instance.pfItemWorld[9];
            case ItemType.hammer: return ItemAssets.Instance.pfItemWorld[10];
            case ItemType.charredBerry: return ItemAssets.Instance.pfItemWorld[11];
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
            case ItemType.charredBerry:
                return true;
            case ItemType.pickaxe:
            case ItemType.axe:
            case ItemType.hammer:
            case ItemType.sword:
                return false;
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
            case ItemType.pickaxe:
            case ItemType.axe:
            case ItemType.hammer:
            case ItemType.sword:
            case ItemType.rock:
            case ItemType.wood:
            case ItemType.flower:
            case ItemType.mushroom:
            case ItemType.charredBerry:
                return true;
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

    public bool IsFood()
    {
        switch (itemType)
        {
            default:
            case ItemType.berry:
                return true;
            case ItemType.rock:
            case ItemType.wood:
            case ItemType.flower:
            case ItemType.mushroom:
            case ItemType.bush:
            case ItemType.sapling:
            case ItemType.pickaxe:
            case ItemType.axe:
            case ItemType.hammer:
            case ItemType.sword:
            case ItemType.charredBerry:
                return false;
        }
    }

    public int MaxAmount()
    {
        switch (itemType)
        {
            default:
            case ItemType.berry: return 5;
            case ItemType.bush: return 5;
            case ItemType.sapling: return 5;
            case ItemType.rock: return 5;
            case ItemType.wood: return 5;
            case ItemType.flower: return 5;
            case ItemType.mushroom: return 5;
            case ItemType.charredBerry: return 5;
        }
    }

    public string GetArmAnimation()
    {
        switch (itemType)
        {
            default:
            case ItemType.berry:
            case ItemType.bush:
            case ItemType.sapling:
            case ItemType.hammer:
            case ItemType.sword:
            case ItemType.rock:
            case ItemType.wood:
            case ItemType.flower:
            case ItemType.mushroom:
            case ItemType.charredBerry:
                return "Holding";
            case ItemType.pickaxe:
            case ItemType.axe:
                return "Holding 2";
        }
    }
}
