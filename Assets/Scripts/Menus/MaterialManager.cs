using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public bool placed;

    public TextMeshProUGUI woodText;
    public int woodAmount;
    public TextMeshProUGUI stoneText;
    public int stoneAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (placed)
        {
            int num = int.Parse(woodText.GetComponent<TextMeshProUGUI>().text) - woodAmount;
            woodText.GetComponent<TextMeshProUGUI>().text = num.ToString();

            int num2 = int.Parse(stoneText.GetComponent<TextMeshProUGUI>().text) - stoneAmount;
            stoneText.GetComponent<TextMeshProUGUI>().text = num2.ToString();

            woodAmount = 0;
            stoneAmount = 0;
            placed = false;
        }
    }
}
