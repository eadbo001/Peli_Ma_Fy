using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBar : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float fillbarAmount = 1.0f; //Controlling the fill amount through the inspector
    [SerializeField] private RectTransform fillbar;//Adds the reference for the filling of the bar

   
    void Start()
    {
        
    }

    
    void Update()
    {

        UpdateFillbar(); //Checks the bar amount every frame
    }

    private void UpdateFillbar()
    {
        fillbar.localScale = new Vector3(fillbarAmount, 1, 1); //changes the scale (aka the fill amount of the bar)
                                                               //according to the inspector modifiable amount
                                                               
    }

}
