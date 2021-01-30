using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodGraber : MonoBehaviour
{
    private Camera cam;
    private bool lookingAtBurger = false;
    private bool onIngredient = false;

    private GameObject currentIngredient;
    private GameObject lastSeenIngredient;
    private MainBurger lastBurger;

    [Header("Grabing settings")]
    public LayerMask foodLayer;
    public LayerMask burgerLayer;
    public Transform holdPoint;
    public float range = 5f;

    [Header("UI")]
    public Text pickupText;

    void Start()
    {
        cam = Camera.main;
        ChangeUIText("", false);
    }

    void Update()
    {
        if (currentIngredient == null)
        {
            if (!CheckRayCastIngredient())
            {
                CheckRayCastAloneBurger();
            }
        }
        if (currentIngredient != null) CheckRayCastBurger();
        HandleIngredient();

    }

    private bool CheckRayCastIngredient()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range, foodLayer))
        {
            lookingAtBurger = true;
            if (!onIngredient && currentIngredient == null)
            {
                ChangeUIText("E: " + hit.collider.gameObject.GetComponent<BurgerComponent>().ComponentName, true);   //ingredients name
                lastSeenIngredient = hit.collider.gameObject;
            }
            return true;
        }
        else 
        { 
            if (onIngredient) ChangeUIText("", false);
            lookingAtBurger = false;
            return false;
        }
    }

    private void CheckRayCastBurger()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range, burgerLayer))
        {

            if (!onIngredient)
            {
                lastBurger = hit.collider.gameObject.GetComponent<MainBurger>();

                if (!lastBurger.FinishedBurger) 
                {
                    ChangeUIText("F: Place on burger", true);        
                }
                else ChangeUIText("Finsihed Burger", true);            
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (lastBurger.CheckAddComponent(currentIngredient))  //if it can't add the ingredient 
                {
                    currentIngredient = null;
                }
            }
        }
        else { if (onIngredient) ChangeUIText("", false); lastBurger = null; }
    }

    private void CheckRayCastAloneBurger()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, range, burgerLayer))
        {
            if (!onIngredient)
            {
                lastBurger = hit.collider.gameObject.GetComponent<MainBurger>();

                if (!lastBurger.FinishedBurger) ChangeUIText("Burger", true);
                else ChangeUIText("Finsihed Burger", true);
            }
        }
        else { if (onIngredient) ChangeUIText("", false); lastBurger = null; }
    }


    private void HandleIngredient()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentIngredient == null && onIngredient )
            {
                currentIngredient = lastSeenIngredient;
                currentIngredient.transform.parent = gameObject.transform;
                currentIngredient.transform.position = holdPoint.position;

                ChangeUIText("", false);
            }
            else if(currentIngredient != null )
            {
                currentIngredient.transform.parent = null;
                currentIngredient = null;
                ChangeUIText("", false);
            }
        }
    }

    private void ChangeUIText(string text, bool toggle)
    {
        onIngredient = toggle;
        pickupText.enabled = onIngredient;
        pickupText.text = text;
    }

}
