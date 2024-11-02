using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{

    public static LogRotation Instance {get; set;}

    [System.Serializable]
    private class RotationElement
    {
        //..Disabling warning code
        #pragma warning disable 0649
        public float Speed;
        public float Duration;
        #pragma warning restore 0649
    }

    [SerializeField]
    private RotationElement[] rotationPattern;

    //..Allows the simulation of wheels by providing a constraint
    //..suspension motion with an optional motor;
    private WheelJoint2D wheelJoint;

    //..Parameters for the optional motor force applied to the a Joint2D;
    private JointMotor2D motor;

    private void Awake()
    {
        Instance = this;
            
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();

        StartCoroutine("PlayRotationPattern");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;

        //..Infinite Loop;
        while(true)
        {
            yield return new WaitForFixedUpdate();

            //..Setting the motors;
            motor.motorSpeed = rotationPattern[rotationIndex].Speed;
            motor.maxMotorTorque = 10000;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex++;

            //..If its less than the rotation pattern
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;

        }
    }
}
