using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Color previousColor;

    void OnEnable()
    {
        previousColor = cam.GetComponent<HDAdditionalCameraData>().backgroundColorHDR;
        cam.GetComponent<HDAdditionalCameraData>().backgroundColorHDR = new Color(0.83f, 0.68f, 0.0f);
    }

    void OnDisable()
    {
        cam.GetComponent<HDAdditionalCameraData>().backgroundColorHDR = previousColor;
    }
}
