using System.Collections.Generic;
using UnityEngine;

public class AllowInteract : MonoBehaviour
{

    bool isAllowed = false;
    AllowInteract[] others;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        others = FindObjectsByType<AllowInteract>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllowed)
            return;

        float closes = 100000;
        foreach (AllowInteract allowInteract in others)
        {
            if (allowInteract == this) continue;
            if (allowInteract == null) continue;

            float distance = Vector2.Distance(transform.position, allowInteract.transform.position);
            if (distance < closes)
                closes = distance;
        }

        if (closes > 1)
        {
            Debug.Log("I may be touched now :D");
            gameObject.layer = 0;
            isAllowed = true;
        }

    }
}
