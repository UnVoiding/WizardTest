using Romeno.Utils;
using UnityEngine;


namespace Romeno.WizardTest
{
    public class CameraManager : StrictSingleton<CameraManager>
    {
        // REFERENCES
        [SerializeField] 
        private OverviewCamera OverviewCamera;

        // RUNTIME
        public Camera Current;

        protected override void Setup()
        {
            
        }

        public void ActivateCharacterCamera()
        {
            if (RoundManager.I.MainCharacter != null)
            {
                RoundManager.I.MainCharacter.ActivateCharacterCamera(true);

                Current = RoundManager.I.MainCharacter.CharacterViewCamera;
            }
            else
            {
                Debug.LogError("Trying to Activate Character Camera when MainCharacter is null");
            }
            
            OverviewCamera.Activate(false); 
        }

        public void ActivateOverviewCamera()
        {
            if (RoundManager.I.MainCharacter != null)
            {
                RoundManager.I.MainCharacter.ActivateCharacterCamera(false);
            }
            
            OverviewCamera.Activate(true);
            Current = OverviewCamera.Camera;
        }

        public void DirectOverviewCamera(Vector3 cameraPosition, Vector3 rotationPoint)
        {
            OverviewCamera.Direct(cameraPosition, rotationPoint);
        }
    }
}