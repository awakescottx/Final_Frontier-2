using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //How the camera moves
    public float maxPanSpeed = 1;
    public float accelerationTime = 1;
    public float decelerationTime = 1;

    float acceleration, deceleration;
    float previous_X_velocity, previous_Y_velocity;
    float displacement_X, displacement_Y;

    //Boundaries
    public float maxVerticalDistance;
    public float maxHorizontalDistance;

        //transform.Translate(5 * Time.deltaTime, 0f, 0f);
        void Update() {

            // < LEFT / RIGHT Movement >

            // Move camera right
            if (Input.GetAxis("Horizontal") > 0)
            {
                // (1) Calculate acceleration based on: final velocity and final time
                acceleration = maxPanSpeed / accelerationTime;
                // (2) Increase velocity based on: acceleration and elapsed time (last frame) -- Velocity is constrained to MaxSpeed
                previous_X_velocity = Mathf.Clamp(previous_X_velocity + (acceleration * Time.deltaTime), -maxPanSpeed, maxPanSpeed);
                // (3) Determine how much to move the ship based on its velocity and time that has elapsed (last frame)
                displacement_X = previous_X_velocity * Time.deltaTime;
                //Debug.Log ("Accelerating(+X) ---> Displacement = " + displacement_X + ", Velocity = " + prevVelocity_X + "u/s, Time = " + Time.time + "s");

            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                // Move camera left
                acceleration = maxPanSpeed / accelerationTime;
                previous_X_velocity = Mathf.Clamp(previous_X_velocity + (-acceleration * Time.deltaTime), -maxPanSpeed, maxPanSpeed);
                displacement_X = previous_X_velocity * Time.deltaTime;
                //Debug.Log ("Accelerating(-X) ---> Displacement = " + displacement_X + ", Velocity = " + prevVelocity_X + "u/s, Time = " + Time.time + "s");

                
            }
            else // Pressing neither Left/A or Right/D
            {
                // If the camera still has momentum
                if (previous_X_velocity != 0)
                {
                    
                    if (previous_X_velocity > 0) // If the camera was heading in a positive direction...
                {
                        deceleration = -maxPanSpeed / decelerationTime;
                        previous_X_velocity = Mathf.Clamp(previous_X_velocity + (deceleration * Time.deltaTime), 0, maxPanSpeed); //Should be getting closer to zero
                        displacement_X = previous_X_velocity * Time.deltaTime;
                        //Debug.Log ("Decelerating(X) <--- Displacement = " + displacement_X + ", Velocity = " + prevVelocity_X + "u/s, Time = " + Time.time + "s");
                        
                    }
                    else // If the ship was heading in a negative direction...
                    {
                        deceleration = -maxPanSpeed / decelerationTime;
                        previous_X_velocity = Mathf.Clamp(previous_X_velocity + (-deceleration * Time.deltaTime), -maxPanSpeed, 0); //Should be getting closer to zero
                        displacement_X = previous_X_velocity * Time.deltaTime;
                        //Debug.Log ("Decelerating(X) <--- Displacement = " + displacement_X + ", Velocity = " + prevVelocity_X + "u/s, Time = " + Time.time + "s");
                    }

                }
            }

            // < UP / DOWN Movement >

            if (Input.GetAxis("Vertical") > 0) //Holding the UP or W key.
            {

                acceleration = maxPanSpeed / accelerationTime;
                previous_Y_velocity = Mathf.Clamp(previous_Y_velocity + (acceleration * Time.deltaTime), -maxPanSpeed, maxPanSpeed);
                displacement_Y = previous_Y_velocity * Time.deltaTime;
                //Debug.Log ("Accelerating(+Y) ---> Displacement = " + displacement_Y + ", Velocity = " + prevVelocity_Y + "u/s, Time = " + Time.time + "s");

                // DOWN
            }
            else if (Input.GetAxis("Vertical") < 0) //Holding the DOWN or S key.
        {

                acceleration = maxPanSpeed / accelerationTime;
                previous_Y_velocity = Mathf.Clamp(previous_Y_velocity + (-acceleration * Time.deltaTime), -maxPanSpeed, maxPanSpeed);
                displacement_Y = previous_Y_velocity * Time.deltaTime;
                //Debug.Log ("Accelerating(-Y) ---> Displacement = " + displacement_Y + ", Velocity = " + prevVelocity_Y + "u/s, Time = " + Time.time + "s");

                // Pressing neither UP/W or DOWN/S
            }
            else
            {
                // If the camera still has momentum
                if (previous_Y_velocity != 0)
                {
                    
                    if (previous_Y_velocity > 0) // If the camera was heading in a positive direction...
                {
                        deceleration = -maxPanSpeed / decelerationTime;
                        previous_Y_velocity = Mathf.Clamp(previous_Y_velocity + (deceleration * Time.deltaTime), 0, maxPanSpeed); //Should be getting closer to zero
                        displacement_Y = previous_Y_velocity * Time.deltaTime;
                        //Debug.Log ("Decelerating(Y) <--- Displacement = " + displacement_Y + ", Velocity = " + prevVelocity_Y + "u/s, Time = " + Time.time + "s");
                        
                    }
                    else // If the camera was heading in a negative direction...
                    {
                        deceleration = -maxPanSpeed / decelerationTime;
                        previous_Y_velocity = Mathf.Clamp(previous_Y_velocity + (-deceleration * Time.deltaTime), -maxPanSpeed, 0); //Should be getting closer to zero
                        displacement_Y = previous_Y_velocity * Time.deltaTime;
                        //Debug.Log ("Decelerating(Y) <--- Displacement = " + displacement_Y + ", Velocity = " + prevVelocity_Y + "u/s, Time = " + Time.time + "s");
                    }

                }
            }

            transform.Translate(displacement_X, displacement_Y, 0);

            /*
            Vector3 directionToMove = new Vector3(displacement_X, displacement_Y, 0);
            Vector3 directionToMove_RelativeToCamera = mainCam.transform.TransformDirection(directionToMove);
            transform.Translate(directionToMove_RelativeToCamera, Space.World);

            //transform.Translate (displacement_X, displacement_Y, 0, Space.World); -- Doesn't consider camera's rotation
            */
        }
    }
