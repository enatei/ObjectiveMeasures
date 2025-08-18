using UnityEngine;
using UnityEngine.InputSystem;

public class actionSetEnable : MonoBehaviour
{

       // [SerializeField] private InputActionReference JoyStitckR;

        [SerializeField] private InputActionAsset ActionAsset;

        private void OnEnable()
        {
            if (ActionAsset != null)
            {
                ActionAsset.Enable();
            }
        }

}
