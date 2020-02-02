using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class HeadBob : MonoBehaviour
    {
        public Camera Camera;
        public CurveControlledBob motionBob = new CurveControlledBob();
        public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
        public RigidbodyFirstPersonController rigidbodyFirstPersonController;
        public float StrideInterval;
        [Range(0f, 1f)] public float RunningStrideLengthen;

       // private CameraRefocus m_CameraRefocus;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        public bool debugMode = false;//Test-run/Call ShakeCamera() on start

        public float shakeAmount;//The amount to shake this frame.
        public float shakeDuration;//The duration this frame.

        //Readonly values...
        float shakePercentage;//A percentage (0-1) representing the amount of shake to be applied when setting rotation.
        float startAmount;//The initial shake amount (to determine percentage), set when ShakeCamera is called.
        float startDuration;//The initial shake duration, set when ShakeCamera is called.

        bool isRunning = false; //Is the coroutine running right now?

        public bool smooth;//Smooth rotation?
        public float smoothAmount = 5f;//Amount to smooth


        private void Start()
        {
            
            if (debugMode)
            {
                ShakeCamera();
            }
            else
            {
                motionBob.Setup(Camera, StrideInterval);
                m_OriginalCameraPosition = Camera.transform.localPosition;
            }
            //     m_CameraRefocus = new CameraRefocus(Camera, transform.root.transform, Camera.transform.localPosition);
        }

        void ShakeCamera()
        {

            startAmount = shakeAmount;//Set default (start) values
            startDuration = shakeDuration;//Set default (start) values

            if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
        }

        public void ShakeCamera(float amount, float duration)
        {

            shakeAmount += amount;//Add to the current amount.
            startAmount = shakeAmount;//Reset the start amount, to determine percentage.
            shakeDuration += duration;//Add to the current time.
            startDuration = shakeDuration;//Reset the start time.

            if (!isRunning) StartCoroutine(Shake());//Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
        }
        IEnumerator Shake()
        {
            isRunning = true;

            while (shakeDuration > 0.01f)
            {
                Vector3 rotationAmount = UnityEngine.Random.insideUnitSphere * shakeAmount;//A Vector3 to add to the Local Rotation
                rotationAmount.z = 0;//Don't change the Z; it looks funny.

                shakePercentage = shakeDuration / startDuration;//Used to set the amount of shake (% * startAmount).

                shakeAmount = startAmount * shakePercentage;//Set the amount of shake (% * startAmount).
                shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//Lerp the time, so it is less and tapers off towards the end.


                if (smooth)
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
                else
                    transform.localRotation = Quaternion.Euler(rotationAmount);//Set the local rotation the be the rotation amount.

                yield return null;
            }
            transform.localRotation = Quaternion.identity;//Set the local rotation to 0 when done, just to get rid of any fudging stuff.
            isRunning = false;
        }


        private void Update()
        {
            
            //if (isRunning)
                //return;
          //  m_CameraRefocus.GetFocusPoint();
            Vector3 newCameraPosition;
            if (rigidbodyFirstPersonController.Velocity.magnitude > 0 && rigidbodyFirstPersonController.Grounded)
            {
                Camera.transform.localPosition = motionBob.DoHeadBob(rigidbodyFirstPersonController.Velocity.magnitude*(rigidbodyFirstPersonController.Running ? RunningStrideLengthen : 1f));
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
            }
            else
            {
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();
            }
            Camera.transform.localPosition = newCameraPosition;

            if (!m_PreviouslyGrounded && rigidbodyFirstPersonController.Grounded)
            {
                StartCoroutine(jumpAndLandingBob.DoBobCycle());
            }

            m_PreviouslyGrounded = rigidbodyFirstPersonController.Grounded;
            //  m_CameraRefocus.SetFocusPoint();
           
             
            
        }
    }
}
