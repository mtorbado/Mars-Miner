using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTweening : MonoBehaviour
{

    public GameObject panel, button;
    private bool isDisplayed;

    // Start is called before the first frame update
    void Awake()
    {
        isDisplayed = true;

        panel = gameObject.transform.Find("Panel").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlternatePanel()
    {
        if (isDisplayed)
        {
            LeanTween.moveLocalX(panel, 520f, 0.2f);
        }
        else
        {
            LeanTween.moveLocalX(panel, 280f, 0.2f);
        }
        isDisplayed = !isDisplayed;
    }
}
