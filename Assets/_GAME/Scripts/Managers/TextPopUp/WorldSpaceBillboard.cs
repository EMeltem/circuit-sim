using UnityEngine;

namespace Project.Managers
{
    public class WorldSpaceBillboard : MonoBehaviour
    {
        private Transform m_CameraTransform;
        public enum Axis { up, down, left, right, forward, back };
        public bool reverseFace = false;
        public Axis axis = Axis.up;

        // return a direction based upon chosen axis
        public Vector3 GetAxis(Axis refAxis)
        {
            switch (refAxis)
            {
                case Axis.down:
                    return Vector3.down;
                case Axis.forward:
                    return Vector3.forward;
                case Axis.back:
                    return Vector3.back;
                case Axis.left:
                    return Vector3.left;
                case Axis.right:
                    return Vector3.right;
            }

            // default is Vector3.up
            return Vector3.up;
        }

        void Awake()
        {
            // if no camera referenced, grab the main camera
            if (!m_CameraTransform) m_CameraTransform = Camera.main.transform;
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            // rotates the object relative to the camera
            Vector3 targetPos = transform.position + m_CameraTransform.rotation * (reverseFace ? Vector3.forward : Vector3.back);
            Vector3 targetOrientation = m_CameraTransform.rotation * GetAxis(axis);
            transform.LookAt(targetPos, targetOrientation);
        }
    }
}
