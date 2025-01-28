using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident
{
    public enum ResidentType
    {
        prisoner,
        merchant,
    }

    public ResidentType residentType;

    public int[] GetStats()
    {
        switch (residentType)
        {
            default:
            case ResidentType.prisoner: return PrisonerStats();
            case ResidentType.merchant: return MerchantStats();
        }
    }

    private int[] PrisonerStats()
    {
        return ReturnStats(new int[2] { 100, 100 }, //Health
                           new int[2] { 100, 100 }, //Food
                           new int[2] { 20, 20 }, //Moral
                           new int[2] { 15, 20 }); //Strength
    }

    private int[] MerchantStats()
    {
        return ReturnStats(new int[2] { 100, 100 }, //Health
                           new int[2] { 100, 100 }, //Food
                           new int[2] { 20, 20 }, //Moral
                           new int[2] { 5, 10 }); //Strength
    }

    //Min Stat 0, Max Stat 20
    private int[] ReturnStats(int[] Health, int[] Food, int[] Moral, int[] Strength)
    {
        List<int> returnStats = new List<int>();
        returnStats.Add(Random.Range(Health[0], Health[1])); //Health
        returnStats.Add(Random.Range(Food[0], Food[1])); //Food
        returnStats.Add(Random.Range(Moral[0], Moral[1])); //Moral Stat
        returnStats.Add(Random.Range(Strength[0], Strength[1])); //Strength Stat

        return returnStats.ToArray();
    }

    public string ReturnNewName()
    {
        string[] names = {"Robert", "Zofia", "Edwin"};

        return names[Random.Range(0, names.Length)];
    }
}