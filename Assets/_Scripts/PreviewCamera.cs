using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class PreviewCamera : MonoBehaviour
{
    private Camera previewCamera;
    private CameraRoomTrigger room;

    void OnEnable()
    {
        previewCamera = GetComponent<Camera>();
        room = GetComponentInParent<CameraRoomTrigger>();

        if (Application.isPlaying)
        {
            previewCamera.enabled = false;
            enabled = false;
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            previewCamera.enabled = false;
            return;
        }

        bool roomIsSelected =
            selectedObject.transform == room.transform ||
            selectedObject.transform.IsChildOf(room.transform);

        previewCamera.enabled = roomIsSelected;
#endif
    }
}