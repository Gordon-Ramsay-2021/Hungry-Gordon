using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerComponent : MonoBehaviour
{
    public enum BurgerComponentPosition {bottom, middle, top };

    [SerializeField]private BurgerComponentPosition bugerComponentPosition;

    [SerializeField]private string componentName; 

    private float startCookTime;
    private int burgerIndex;

    public string ComponentName { get { return componentName; } set { componentName = value; } }

    public BurgerComponentPosition ComponentPosition { get { return bugerComponentPosition; } }
   
    public float StartCookTime { get { return startCookTime; } set { startCookTime = value; } }
    public int BurgerIndex { get { return burgerIndex; } set { burgerIndex = value; } }

}
