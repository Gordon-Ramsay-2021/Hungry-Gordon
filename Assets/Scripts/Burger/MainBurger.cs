using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBurger : MonoBehaviour
{
    private Stack<GameObject> burgerComponents;
    [SerializeField] int MaxAmountburgerComponents = 5;

    public bool FinishedBurger = false;

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
            
            if (burgerComponents.Count < MaxAmountburgerComponents - 1)
            {
                AddComponent(burgerIngredient, burgerType);
                return true;
            }
            else if ( burgerType == BurgerComponent.BurgerComponentPosition.top)
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
        burgerComponents.Push(burgerIngredient);
        burgerIngredient.transform.parent = gameObject.transform;
        burgerIngredient.transform.position = transform.position + new Vector3(0, burgerComponents.Count * 0.06f, 0);

        if (burgerType == BurgerComponent.BurgerComponentPosition.top)
        {
            FinishedBurger = true;
            AssembleBurger();
        } 
    }


    private IEnumerator AssembleBurger()
    {
        GameObject[] burgerIngredients = gameObject.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < burgerIngredients.Length; i++)
        {
            Rigidbody ingredient = burgerIngredients[i].GetComponent<Rigidbody>();
            ingredient.isKinematic = false;
        }

        yield return new WaitForSeconds(20f);

        for (int i = 0; i < burgerIngredients.Length; i++)
        {
            GameObject ingredient = burgerIngredients[i];
            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            Destroy(rb);
        }

    }

}
