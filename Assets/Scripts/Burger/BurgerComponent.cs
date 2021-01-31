using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerComponent : MonoBehaviour
{
    public enum BurgerComponentPosition {bottom, middle, top };

    [SerializeField]private BurgerComponentPosition bugerComponentPosition;

    [SerializeField]private string componentName; 

    public string ComponentName { get { return componentName; } set { componentName = value; } }

    public BurgerComponentPosition ComponentPosition { get { return bugerComponentPosition; } }
   

}
