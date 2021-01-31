using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendZone : MonoBehaviour
{
    public GameObject targetBurger;
    private TargetBurger target;
    public int finsihedBurgerLayer;

    private void Start()
    {
        target = targetBurger.GetComponent<TargetBurger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == finsihedBurgerLayer)
        {
            MainBurger burger = other.gameObject.GetComponent<MainBurger>();

            if (burger.FinishedBurger)
            {
                Stack<GameObject> burgerscomp = burger.BurgersComponents;
                Debug.Log(TargetBurger.CompareBurgers(target.currentTarget, burgerscomp));
                target.currentTarget = target.GetRandomBurger();
                target.UpdateUI();
            }

        }

    }


}
