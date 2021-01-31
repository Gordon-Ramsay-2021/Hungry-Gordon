using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    public int ingredientLayer;
    public Material cookedMat;

    [Header("Cooking")]
    public float CookTime = 20f;
    public float BurnTime = 5f;

    private Dictionary<int, float> currentBurgers;
    private Dictionary<int, BurgerComponent> keyedBurgers;
    private int count = 0;

    private void Start()
    {
        currentBurgers = new Dictionary<int, float>();
        keyedBurgers = new Dictionary<int, BurgerComponent>();
    }

    private void Update()
    {
        foreach(var burger in currentBurgers)
        {
            if(Time.time - burger.Value >= BurnTime)
            {
                keyedBurgers[burger.Key].ComponentName = "Burnt Patty";
                currentBurgers.Remove(burger.Key);           
            }
            else if(Time.time - burger.Value >= CookTime)
            {
                keyedBurgers[burger.Key].ComponentName = "Cooked Patty";
               keyedBurgers[burger.Key].gameObject.GetComponent<Renderer>().material = cookedMat;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ingredientLayer)
        {
            BurgerComponent ingredient = other.gameObject.GetComponent<BurgerComponent>();

            if(ingredient.ComponentName == "Beef Patty")
            {
                currentBurgers.Add(count, Time.time);
                keyedBurgers.Add(count, ingredient);
                count++;
            }
        }
    }
}
