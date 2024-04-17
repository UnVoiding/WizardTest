using UnityEngine;


namespace Romeno.WizardTest
{
    public class OverviewCamera : MonoBehaviour
    {
        // REFERENCES
        public Camera Camera;

        // SETTINGS
        [SerializeField] 
        private Vector3 RotationPoint;
        [SerializeField] 
        private float RotationSpeed = 20;

        void Update()
        {
            transform.RotateAround(RotationPoint, Vector3.up, RotationSpeed * Time.unscaledDeltaTime);
            transform.LookAt(RotationPoint);
        }

        public void Direct(Vector3 cameraPosition, Vector3 rotationPoint)
        {
            transform.position = cameraPosition;
            RotationPoint = rotationPoint;
        }

        public void Activate(bool value)
        {
            gameObject.SetActive(value);
            if (value)
            {
                gameObject.tag = "MainCamera";
            }
            else
            {
                gameObject.tag = "Untagged";
            }
        }
    }
}