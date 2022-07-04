using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendleLight : MonoBehaviour
{
    /// <summary>
    /// =Pendle Script=
    /// Working on Rotations in general will lead to a lot of confusion when compared to Vector transformations
    /// This is, because rotations rely on four Axis and are not comparable with Vectors in general.
    /// Although, the Math behind it needs no further explaination when working with basic Unity stuff.
    /// Because Unity provides simple methods to convert Vectors into Quaternions. How handy is that :P
    /// 
    /// Starting with some variables needed to achieve a pendle effect on a single axis.
    /// 
    /// The Pendle() Method switches between two operating modes, PingPong and SineWave which are declared in an pendleMethod enum
    /// While PingPong is a linear "wave", SineWave will create a more smooth Pendle effect, select as you prefer.
    /// 
    /// the t variable is a constant linear rising value
    /// while r is the calculated rotation value
    /// 
    /// pendleMaxRotation sets the amplitude of our wave functions
    /// rotationOffset can be used to offset our rotation
    /// pendleSpeed modifies the time value to make the movement faster or slower
	///
	/// Made by Jonas Walter, 2019
    /// </summary>

    public enum PendleMethod
    {
        PingPong,
        SineWave
    };

    [Header("Pendle Setup")]
    public PendleMethod pendleMethod = PendleMethod.PingPong;
    public float pendleSpeed = 1.0f;
    public float pendleMaxRotation = 10.0f;
    public float rotationOffset = 0.0f;

    //saved rotation and time values
    private float r;
    private float t;

    void Update()
    {
        Pendle();
    }

    void Pendle()
    {
        t += Time.deltaTime * pendleSpeed;

        switch(pendleMethod)
        {
            case PendleMethod.PingPong:
                r = (2 * Mathf.PingPong(t, 1f) - 1f) * pendleMaxRotation;
                break;
            case PendleMethod.SineWave:
                r = pendleMaxRotation * Mathf.Sin(t);
                break;
        }

        transform.rotation = Quaternion.Euler(0, 0, r + rotationOffset);
    }
}
