using Oculus.Interaction;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Polaroid : MonoBehaviour
{
    public GameObject photoPrefab = null;
    public MeshRenderer screenRenderer = null;
    public Transform spawnLocation = null;

    private Camera renderCamera = null;

    /*[Header("Zoom Settings")]
    public float minFOV = 1f;
    public float maxFOV = 60f;
    public float zoomSpeed = 30f;

    private InputDevice controllerDevice;
    private float currentFOV;*/

    private void Awake()
    {
        renderCamera = GetComponentInChildren<Camera>();
        //controllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    private void Start()
    {
        CreateRenderTexture();
        TurnOff();
    }

    private void CreateRenderTexture()
    {
        RenderTexture newTexture = new RenderTexture(256, 256, 32, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
        newTexture.antiAliasing = 4;

        renderCamera.targetTexture = newTexture;
        screenRenderer.material.mainTexture = newTexture;
    }

    public void TakePhoto()
    {
        Photo newPhoto = CreatePhoto();
        SetPhotoImage(newPhoto);
    }

    private Photo CreatePhoto()
    {
        GameObject photoObject = Instantiate(photoPrefab, spawnLocation.position, spawnLocation.rotation, transform);
        return photoObject.GetComponent<Photo>();
    }

    private void SetPhotoImage(Photo photo)
    {
        Texture2D newTexture = RenderCameraToTexture(renderCamera);
        photo.SetImage(newTexture);
    }

    private Texture2D RenderCameraToTexture(Camera camera)
    {
        camera.Render();
        RenderTexture.active = camera.targetTexture;

        Texture2D photo = new Texture2D(256, 256, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        photo.Apply();

        return photo;
    }

    public void TurnOn()
    {
        renderCamera.enabled = true;
        screenRenderer.material.color = Color.white;
    }

    public void TurnOff()
    {
        renderCamera.enabled = false;
        screenRenderer.material.color = Color.black;
    }

    /*public void Zoom()
    {
        // Lire la valeur du thumbstick
        if (controllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValue))
        {
            // Utiliser l'axe Y du thumbstick pour zoomer/dézoomer
            // thumbstickValue.y : bas (-1) à haut (1)
            currentFOV -= thumbstickValue.y * zoomSpeed * Time.deltaTime;

            // Limiter le FOV entre min et max
            currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);

            // Appliquer le nouveau FOV
            renderCamera.fieldOfView = currentFOV;
        }
    }

    private void Update()
    {
        Debug.Log(controllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValue));
        if (renderCamera.enabled && controllerDevice.isValid)
        {
            Debug.Log("dans update");
            Zoom();
        }
    }*/
}

