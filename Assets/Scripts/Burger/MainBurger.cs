using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBurger : MonoBehaviour
{
    private Stack<GameObject> burgerComponents;
    [SerializeField] int MaxAmountburgerComponents = 5;

    private bool FinishedBurger = false;

    void Start()
    {
        burgerComponents = new Stack<GameObject>(5);


    }

    void Update()
    {
        
    }

    /// <summary>
    /// checks whether the ingredient can be added to this burger, if so adds it 
    /// </summary>
    public bool CheckAddComponent(GameObject burgerIngredient)
    {
        if (!FinishedBurger)
        {
            BurgerComponent ingredientDetails = burgerIngredient.GetComponent<BurgerComponent>();
            BurgerComponent.BurgerComponentPosition burgerType = ingredientDetails.ComponentPosition;

            if (burgerComponents.Count == 0 && burgerType == BurgerComponent.BurgerComponentPosition.bottom)
            {
                AddComponent(burgerIngredient, burgerType);
                return true;
            }
            else if (burgerComponents.Count < MaxAmountburgerComponents)
            {
                AddComponent(burgerIngredient, burgerType);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// adds the component checking whether its a top as well to finalise the burger
    /// </summary>
    private void AddComponent(GameObject burgerIngredient, BurgerComponent.BurgerComponentPosition burgerType)
    {
        if(burgerType == BurgerComponent.BurgerComponentPosition.top)
        {
            burgerComponents.Push(burgerIngredient);
            FinishedBurger = true;
        }
        else burgerComponents.Push(burgerIngredient);

        burgerIngredient.transform.position = transform.position;// + new Vector3(0, burgerComponents.Count / 5, 0);
        Debug.Log(transform.position);
        burgerIngredient.transform.parent = gameObject.transform;
        
    }

}
