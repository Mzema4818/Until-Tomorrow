using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public Transform[] pfItemWorld;

    public Sprite SwordSprite;
    public Sprite PickaxeSprite;
    public Sprite AxeSprite;
    public Sprite HammerSprite;
    public Sprite woodSprite;
    public Sprite stoneSprite;
    public Sprite mushroomSprite;
    public Sprite flowerSprite;
    public Sprite berrySprite;
    public Sprite bushSprite;
    public Sprite saplingSprite;
    public Sprite charredBerrySprite;
}
