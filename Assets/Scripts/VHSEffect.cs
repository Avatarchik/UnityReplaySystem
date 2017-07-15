using UnityEngine;

/// <summary>
/// VHS post process effect found at https://github.com/staffantan/unity-vhsglitch
/// </summary>
[ExecuteInEditMode]
public class VHSEffect : MonoBehaviour 
{
    [SerializeField]
    MovieTexture vhsTexture;

    private Material material;

    void Awake()
    {
        material = new Material(Shader.Find("VHS"));

        material.SetTexture("_VHSTex", vhsTexture);
    }

    void Start()
    {
        vhsTexture.loop = true;
        vhsTexture.Play();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
