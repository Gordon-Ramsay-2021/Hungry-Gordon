using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public GameObject ArmRig;
    public float UpDownSpeed = 0.5f;
    public float SideToSideSpeed = 5f;
    public float ForwardBackwardSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ArmRig.transform.localPosition = new Vector3(
                ArmRig.transform.localPosition.x,
                ArmRig.transform.localPosition.y - UpDownSpeed * Time.deltaTime,
                ArmRig.transform.localPosition.z
           );
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            ArmRig.transform.localPosition = new Vector3(
                ArmRig.transform.localPosition.x,
                ArmRig.transform.localPosition.y + UpDownSpeed * Time.deltaTime,
                ArmRig.transform.localPosition.z
           );
        }

        // Side to side
        ArmRig.transform.localPosition = new Vector3(
            ArmRig.transform.localPosition.x,
            ArmRig.transform.localPosition.y,
            ArmRig.transform.localPosition.z
        );

        // Forwards backwards
        ArmRig.transform.localPosition = new Vector3(
            ArmRig.transform.localPosition.x,
            ArmRig.transform.localPosition.y,
            ArmRig.transform.localPosition.z + Input.GetAxis("Mouse Y") * ForwardBackwardSpeed * Time.deltaTime
        );
    }
}
