using UnityEngine;
using UnityEngine.AI;

namespace Romeno.WizardTest
{
    public class TestSampleNavMesh : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                // bool result = NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas);
                // Debug.Log(result);

                Debug.Log(CameraManager.I.Current.WorldToViewportPoint(transform.position));
            }
        }
    }
}