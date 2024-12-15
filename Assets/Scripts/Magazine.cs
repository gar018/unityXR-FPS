using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Magazine : MonoBehaviour
{
    public int ammoCount = 10;
    public TextMeshPro ammoText;

    public void Awake()
    {
        ammoText.text = ammoCount.ToString();
    }
    public void Decrement()
    {
        ammoCount--;
        ammoText.text = ammoCount.ToString();
    }
}
