using Gvr.Internal;
using UnityEngine;

/// <summary>Provides mouse-controlled head tracking emulation in the Unity editor.</summary>
[HelpURL("https://developers.google.com/vr/unity/reference/class/GvrEditorEmulator")]
public class VREmulator : MonoBehaviour
{

#if UNITY_EDITOR

    private static readonly Vector3 CAMERA_HEIGHT = Vector3.up * 2.2f;

    private static VREmulator instance;
    private static bool instanceSearchedFor = false;

    // Allocate an initial capacity; this will be resized if needed.
    private static Camera[] allCameras = new Camera[32];

    public AmongCars controls;

    private float mouseX = 0;
    private float mouseY = 0;
    private float mouseZ = 0;

    /// <summary>Gets the instance for this singleton class.</summary>
    /// <value>The instance for this singleton class.</value>
    public static VREmulator Instance
    {
        get
        {
            if (instance == null && !instanceSearchedFor)
            {
                instance = FindObjectOfType<VREmulator>();
                instanceSearchedFor = true;
            }

            return instance;
        }
    }

    /// <summary>Gets the emulated head position.</summary>
    /// <value>The emulated head position.</value>
    public Vector3 HeadPosition { get; private set; }

    /// <summary>Gets the emulated head rotation.</summary>
    /// <value>The emulated head rotation.</value>
    public Quaternion HeadRotation { get; private set; }

    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    /// <summary>Recenters the emulated headset.</summary>
    public void Recenter()
    {
        mouseX = mouseZ = 0;  // Do not reset pitch, which is how it works on the phone.
        UpdateHeadPositionAndRotation();
        ApplyHeadOrientationToVRCameras();
    }

    /// <summary>Single-frame updates for this module.</summary>
    /// <remarks>Should be called in one MonoBehavior's `Update` method.</remarks>
    public void UpdateEditorEmulation()
    {
        if (InstantPreview.IsActive)
        {
            return;
        }

        if (GvrControllerInput.Recentered)
        {
            Recenter();
        }

        Vector2 input = controls.Player.Look.ReadValue<Vector2>();
        float x = input.x * 0.4f;
        float y = input.y * 0.4f;

        bool rolled = false;
        /*if (CanChangeYawPitch())
        {*/
        GvrCursorHelper.HeadEmulationActive = true;

        mouseX += x * 5;
        if (mouseX <= -180)
            mouseX += 360;
        else if (mouseX > 180)
            mouseX -= 360;

        mouseY -= y * 2.4f;
        mouseY = Mathf.Clamp(mouseY, -85, 85);
        /*}
        else if (CanChangeRoll())
        {
            GvrCursorHelper.HeadEmulationActive = true;
            rolled = true;
            mouseZ += x * 5;
            mouseZ = Mathf.Clamp(mouseZ, -85, 85);
        }
        else
        {
            GvrCursorHelper.HeadEmulationActive = false;
        }*/

        if (!rolled)
        {
            // People don't usually leave their heads tilted to one side for long.
            mouseZ = Mathf.Lerp(mouseZ, 0, Time.deltaTime / (Time.deltaTime + 0.1f));
        }

        UpdateHeadPositionAndRotation();
        ApplyHeadOrientationToVRCameras();
    }

    private void Awake()
    {
        controls = new AmongCars();

        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError("More than one active GvrEditorEmulator instance was found in your " +
                           "scene.  Ensure that there is only one active GvrEditorEmulator.");
            this.enabled = false;
            return;
        }
    }

    private void Start()
    {
        LockMouse(true);
        UpdateAllCameras();
        for (int i = 0; i < Camera.allCamerasCount; ++i)
        {
            Camera cam = allCameras[i];

            // Only check camera if it is an enabled VR Camera.
            if (cam && cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {
                if (cam.nearClipPlane > 0.1
                    && GvrSettings.ViewerPlatform == GvrSettings.ViewerPlatformType.Daydream)
                {
                    Debug.LogWarningFormat(
                        "Camera \"{0}\" has Near clipping plane set to {1} meters, which might " +
                        "cause the rendering of the Daydream controller to clip unexpectedly.\n" +
                        "Suggest using a lower value, 0.1 meters or less.",
                        cam.name, cam.nearClipPlane);
                }
            }
        }
    }

    private void Update()
    {
        // GvrControllerInput automatically updates GvrEditorEmulator.
        // This guarantees that GvrEditorEmulator is updated before anything else responds to
        // controller input, which ensures that re-centering works correctly in the editor.
        // If GvrControllerInput is not available, then fallback to using Update().
        if (GvrControllerInput.ApiStatus != GvrControllerApiStatus.Error)
            return;

        UpdateEditorEmulation();
    }

    private void UpdateHeadPositionAndRotation()
    {
        HeadRotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
        HeadPosition = CAMERA_HEIGHT;
    }

    private void ApplyHeadOrientationToVRCameras()
    {
        UpdateAllCameras();

        // Update all VR cameras using Head position and rotation information.
        for (int i = 0; i < Camera.allCamerasCount; ++i)
        {
            Camera cam = allCameras[i];

            // Check if the Camera is a valid VR Camera, and if so update it to track head motion.
            if (cam && cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {
                cam.transform.localPosition = HeadPosition * cam.transform.lossyScale.y;
                cam.transform.localRotation = HeadRotation;
            }
        }
    }

    // Avoids per-frame allocations. Allocates only when allCameras array is resized.
    private void UpdateAllCameras()
    {
        // Get all Cameras in the scene using persistent data structures.
        if (Camera.allCamerasCount > allCameras.Length)
        {
            int newAllCamerasSize = Camera.allCamerasCount;
            while (Camera.allCamerasCount > newAllCamerasSize)
            {
                newAllCamerasSize *= 2;
            }

            allCameras = new Camera[newAllCamerasSize];
        }

        // The GetAllCameras method doesn't allocate memory (Camera.allCameras does).
        Camera.GetAllCameras(allCameras);
    }

    private void LockMouse(bool b)
    {
        Cursor.visible = !b;
        Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
    }

#endif  // UNITY_EDITOR

    public static Quaternion GetRotation()
    {
#if UNITY_EDITOR
        return Camera.main.transform.rotation;
#else
        return GvrVRHelpers.GetHeadRotation();
#endif
    }
}
