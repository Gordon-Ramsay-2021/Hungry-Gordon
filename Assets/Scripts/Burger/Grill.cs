using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : MonoBehaviour
{
    public int ingredientLayer;
    public Material cookedMat;
    public Material burntMat;
    public float refreshTime = 2f;
    private float latetsRefresh = 0;

    [Header("Cooking")]
    public float CookTime = 20f;
    public float BurnTime = 5f;

    private int count = 0;
    private List<int> currentBurgers;
    private List<BurgerComponent> burgers;

    private void Start()
    {
        currentBurgers = new List<int>();
        burgers = new List<BurgerComponent>();
    }

    private void Update()
    {
        if(Time.time - latetsRefresh >= refreshTime)
        {
            CheckBurgers();
            latetsRefresh = Time.time;
        }

    }

    private void CheckBurgers()
    {
        (bool, int) Remove = (false,0);
        foreach(int i in currentBurgers)
        {

            if(Time.time - burgers[i].StartCookTime >= BurnTime)
            {

                burgers[i].ComponentName = "Burnt Patty";
                burgers[i].gameObject.GetComponent<Renderer>().material = burntMat;
                Remove = (true, i);
            }
            if (Time.time - burgers[i].StartCookTime >= CookTime)
            {

                burgers[i].ComponentName = "Cooked Patty";
                burgers[i].gameObject.GetComponent<Renderer>().material = cookedMat;
            }
        }
        if (Remove.Item1)
        {
            currentBurgers.Remove(Remove.Item2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == ingredientLayer)
        {
            BurgerComponent ingredient = other.gameObject.GetComponent<BurgerComponent>();

            if(ingredient.ComponentName == "Beef Patty")
            {
                float time = Time.time;
                ingredient.StartCookTime = time;
                ingredient.BurgerIndex = count;
                currentBurgers.Add(count);
                burgers.Add(ingredient);
                count++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == ingredientLayer)
        {
            BurgerComponent ingredient = other.gameObject.GetComponent<BurgerComponent>();
            RemoveBurger(ingredient.BurgerIndex);           
        }

    }

    private void RemoveBurger(int index)
    {
        currentBurgers.Remove(index);
               
    }
}
