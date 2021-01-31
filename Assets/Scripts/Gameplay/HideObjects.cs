using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    public GameObject IngredientHideSpots;
    public GameObject FingerHideSpots;

    private Transform[] FingerHidePoints;
    private Transform[] IngredientHidePoints;

    public GameObject[] IngredientPrefabs;
    public GameObject[] Fingers;

    [Header("Spawn Settings")]
    public int ingredientAmount;

    void Start()
    {
        FillHidePoints(out FingerHidePoints, FingerHideSpots);
        FillHidePoints(out IngredientHidePoints, IngredientHideSpots);

        HideObjs(IngredientHidePoints, IngredientPrefabs, ingredientAmount);
    }

    private void HideObjs(Transform[] positions, GameObject[] items, int amountOfEach)
    {
        foreach(GameObject item in items)
        {
            for (int i = 0; i < amountOfEach; i++)
            {
                Transform pos = positions[Random.Range(0, positions.Length)];
                Vector3 positionWthOffset = pos.position + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(0, 0.2f), Random.Range(-0.2f, 0.2f));
                Instantiate(item, positionWthOffset, Quaternion.identity);
            }
        }
    }


    private void FillHidePoints(out Transform[] transforms, GameObject objTransform)
    {
        transforms = objTransform.GetComponentsInChildren<Transform>();
    }
}
