using UnityEngine;

public class CaptureUIToSprite : MonoBehaviour
{
    public Canvas uiCanvas;  // The canvas to capture
    public Sprite[] bookPages;  // Array to store the generated sprites
    public Book book;  // The book to store the page sprite

    private int height;
    private int width;

    void Start()
    {
        height = (int)GetComponent<RectTransform>().rect.height;
        width = (int)GetComponent<RectTransform>().rect.width;

        CaptureUI();

        gameObject.SetActive(false);
    }

    void CaptureUI()
    {
        // Create a temporary camera to render the UI
        GameObject cameraObject = new GameObject("Temporary Camera");
        Camera renderCamera = cameraObject.AddComponent<Camera>();

        // Set the camera to render to a RenderTexture
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        renderCamera.targetTexture = renderTexture;

        // Set the camera's clear flags to Solid Color so it doesn't render the skybox
        renderCamera.clearFlags = CameraClearFlags.SolidColor;
        renderCamera.backgroundColor = Color.clear;  // Set background to black (for debugging)

        // Set the camera's culling mask to only render the UI layer
        renderCamera.cullingMask = 1 << LayerMask.NameToLayer("UI");

        // Set up the Canvas to use the temporary camera for rendering
        uiCanvas.renderMode = RenderMode.ScreenSpaceCamera;  // Use ScreenSpaceCamera for capturing
        uiCanvas.worldCamera = renderCamera;  // Set the camera that renders the UI

        // Ensure the camera is positioned correctly
        renderCamera.transform.position = new Vector3(0, 0, 0);  // Adjust to fit your scene
        renderCamera.transform.LookAt(uiCanvas.transform);  // Ensure it is looking at the canvas

        // Disable all other renderers that might affect the result

        // Render the UI to the RenderTexture
        renderCamera.Render();

        // Create a new Texture2D from the RenderTexture
        RenderTexture.active = renderTexture;

        // Create the Texture2D
        Texture2D texture = new Texture2D(width, height);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        sprite.name = transform.name;

        // Store the sprite in the bookPages array
        print(transform.GetSiblingIndex());
        book.bookPages[transform.GetSiblingIndex()] = sprite;

        // Clean up
        RenderTexture.active = null;
        Destroy(cameraObject);  // Destroy the temporary camera object
    }
}
