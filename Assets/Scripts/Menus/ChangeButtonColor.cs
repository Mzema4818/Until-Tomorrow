using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    private ResidentHolder residentHolder;
    private ResidentScheudle residentScheudle;

    private RawImage image;
    private int index;

    public int scheduleIndex;

    private void Awake()
    {
        residentHolder = transform.parent.GetComponent<ResidentHolder>();
        residentScheudle = residentHolder.resident.GetComponent<ResidentScheudle>();
        image = transform.GetComponent<RawImage>();
        index = int.Parse(name);
        scheduleIndex = residentScheudle.Schedule[index];

        image.color = GetColor(scheduleIndex);
    }

    public void ChangeColor()
    {
        if (image.color == residentHolder.Sleep) image.color = residentHolder.Work;
        else if (image.color == residentHolder.Work) image.color = residentHolder.Wander;
        else if (image.color == residentHolder.Wander) image.color = residentHolder.Sleep;

        ChangeNum();

        residentScheudle.Schedule[index] = scheduleIndex;
    }

    private Color GetColor(int i)
    {
        if (i == 0) return residentHolder.Sleep;
        else if (i == 1) return residentHolder.Work;
        else if (i == 2) return residentHolder.Wander;

        return Color.black;
    }

    private void ChangeNum()
    {
        if (scheduleIndex == 0) scheduleIndex = 1;
        else if (scheduleIndex == 1) scheduleIndex = 2;
        else if (scheduleIndex == 2) scheduleIndex = 0;
    }
}
