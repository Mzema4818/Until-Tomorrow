using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildMenuFirstPage : MonoBehaviour
{
    public GridLayoutGroup gridLayout;

    private void OnEnable()
    {
        RenameGridChildren();
    }

    void RenameGridChildren()
    {
        int index = 1; // Start numbering from 1

        foreach (Transform child in gridLayout.transform)
        {
            if (child.gameObject.activeSelf) // Only consider active children
            {
                TextMeshProUGUI tm = child.GetComponent<TextMeshProUGUI>();
                if (tm != null)
                {
                    // Reset text before appending the new number
                    string originalText = tm.text;

                    // Remove any existing numbering (e.g., "1) " at the start)
                    int closeParenIndex = originalText.IndexOf(") ");
                    if (closeParenIndex != -1)
                    {
                        originalText = originalText.Substring(closeParenIndex + 2);
                    }

                    // Apply new numbering
                    tm.text = index + ") " + originalText;
                    index++;
                }
            }
        }
    }


}
