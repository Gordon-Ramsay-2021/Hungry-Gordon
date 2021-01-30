using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetBurger : MonoBehaviour
{
    public Text TargetBurgerUI;
    public GameObject[] ingredients;
    public int MaxBurgerSize = 4;

    public Stack<string> currentTarget;

    public void Start()
    {
        currentTarget = GetRandomBurger();
        foreach(string ingr in currentTarget)
        {
            TargetBurgerUI.text += ingr + "\n";
        }
        TargetBurgerUI.text += "Bottom-Bun";


    }

    public Stack<string> GetRandomBurger()
    {
        int size = Random.Range(3, MaxBurgerSize);
        Stack<string> targetBurger = new Stack<string>();

        targetBurger.Push("Beef Patty");
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
        int iterations = target.Count;
        for (int i = 0; i < iterations; i++)
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
