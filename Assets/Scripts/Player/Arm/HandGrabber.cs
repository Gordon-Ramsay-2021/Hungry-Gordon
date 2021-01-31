using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGrabber : MonoBehaviour
{
    private Camera cam;
    private bool lookingAtBurger = false;
    private bool onIngredient = false;
    private bool onBurger = false;

    private GameObject currentIngredient;
    private bool canPickup = true;


    private GameObject lastSeenIngredient;
    private MainBurger lastBurger;

    [Header("Grabing settings")]
    public LayerMask foodLayer;
    public LayerMask burgerLayer;
    public Transform holdPoint;
    public float range = 5f;
    public float throwPower = 5f;

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

    private Ray GrabRay()
    {
        return new Ray(holdPoint.position, Vector3.down);
    }

    private bool CheckRayCastIngredient()
    {
        RaycastHit hit;
        Ray ray = GrabRay();

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
        Ray ray = GrabRay();

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
                    onIngredient = false;
                }
            }
        }
        else { if (onIngredient) ChangeUIText("", false); lastBurger = null; }
    }

    private void CheckRayCastAloneBurger()
    {
        RaycastHit hit;
        Ray ray = GrabRay();

        if (Physics.Raycast(ray, out hit, range, burgerLayer))
        {
            if (!onBurger)
            {
                lastBurger = hit.collider.gameObject.GetComponent<MainBurger>();

                if (!lastBurger.FinishedBurger) ChangeUIText("Burger", true);
                else
                {
                    ChangeUIText("E: Finsihed Burger", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) && currentIngredient == null && lastBurger.FinishedBurger) PickupItem(lastBurger.gameObject);   //pickup if pressed e, not holding anything and finsihed burger
        }
        else { if (onBurger) ChangeUIText("", false); lastBurger = null; }
    }


    private void HandleIngredient()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentIngredient == null && onIngredient)
            {
                PickupItem(lastSeenIngredient);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentIngredient != null)
            {
                DropItem();
            }
        }
    }

    private void PickupItem(GameObject item)
    {
        currentIngredient = item;
        currentIngredient.transform.parent = holdPoint;
        currentIngredient.GetComponent<Rigidbody>().isKinematic = true;
        currentIngredient.GetComponent<Rigidbody>().detectCollisions = false;
        currentIngredient.transform.position = holdPoint.position;
        ChangeUIText("", false);
    }

    private void DropItem()
    {
        currentIngredient.transform.parent = null;
        currentIngredient.GetComponent<Rigidbody>().isKinematic = false;
        currentIngredient.GetComponent<Rigidbody>().detectCollisions = true;
        currentIngredient = null;
        ChangeUIText("", false);
    }

    private void ChangeUIText(string text, bool toggle)
    {
        onIngredient = toggle;
        pickupText.enabled = onIngredient;
        pickupText.text = text;
    }
}
