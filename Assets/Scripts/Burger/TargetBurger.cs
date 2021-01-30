using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBurger : MonoBehaviour
{
    public Text TargetBurgerUI;
    public GameObject[] ingredients;
    public int MaxBurgerSize = 4;

    private Stack<string> currentTarget;

    public void Start()
    {
        currentTarget = GetRandomBurger();
        Debug.Log(currentTarget.Count);
        foreach(string ingr in currentTarget)
        {
            TargetBurgerUI.text += ingr + "\n";
        }


    }

    public Stack<string> GetRandomBurger()
    {
        int size = Random.Range(3, MaxBurgerSize);
        Stack<string> targetBurger = new Stack<string>();

        targetBurger.Push("Patty");
        for (int i = 0; i < size - 2; i++)
        {
            GameObject ingredient = ingredients[Random.Range(0, ingredients.Length)];
            targetBurger.Push(ingredient.GetComponent<BurgerComponent>().ComponentName);
        }
        targetBurger.Push("Top-Bun");

        return targetBurger;
    }

    public static int CompareBurgers(Stack<string> target, Stack<GameObject> burger )
    {
        int mistakes = 0;
        for (int i = 0; i < target.Count; i++)
        {
            string targetIngr = target.Pop();
            if(burger.Count == 0) { mistakes++; continue; }

            string actualIngr = burger.Pop().GetComponent<BurgerComponent>().ComponentName;
            if(targetIngr != actualIngr)
            {
                mistakes++;
            }

        }
        return mistakes;

    }


}
