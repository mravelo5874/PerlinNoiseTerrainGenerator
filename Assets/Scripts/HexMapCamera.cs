using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapCamera : MonoBehaviour {

    public bool isOrthographic;

    Transform swivel, stick;
    static HexMapCamera instance;
    public Camera camera;

    bool followingGameObject;
    GameObject gameobjectToFollow;

    public float cameraMoveSpeed;

    // zoom
    float zoom = 1f;
    public float maxSize, minSize;
    public float maxFOV, minFOV;

    // position
    public float moveSpeedMinZoom, moveSpeedMaxZoom;

    // rotation
    public float rotationSpeed;
    float rotationAngle;

    // bounds of camera movement
    public HexGrid grid;

    void Awake() {
        instance = this;
        followingGameObject = false;
        swivel = transform.GetChild(0);
        stick = swivel.GetChild(0);

        if (isOrthographic) {
            camera.orthographic = true;
        }
        else {
            camera.orthographic = false;
        }
    }

    public static bool Locked {
        set {
            instance.enabled = !value;
        }
    }

    public void SetGameObjectToFollow(GameObject _object) {
        followingGameObject = true;
        gameobjectToFollow = _object;
    }

    public void UnsetGameObjectToFollow() {
        followingGameObject = false;
        gameobjectToFollow = null;
    }

    void Update() {
        // zoom input
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0f) {
            AdjustZoom(zoomDelta);
        }

        // adjust rotation
        float rotationDelta = Input.GetAxis("Rotation");
        if (rotationDelta != 0f) {
            //AdjustRotation(rotationDelta);
        }

        if (!followingGameObject) {
            // movement input
            float xDelta = Input.GetAxis("Horizontal");
            float zDelta = Input.GetAxis("Vertical");
            if (xDelta != 0f || zDelta != 0f) {
                AdjustPosition(xDelta, zDelta);
            }
        }
        else {
            KeepGameObjectCentered();
        }      
    }

    void KeepGameObjectCentered() {
        Vector3 newPos = gameobjectToFollow.transform.position;
        //newPos.z += 40;

        transform.position = Vector3.Lerp(transform.position, newPos, cameraMoveSpeed * Time.deltaTime);
    }

    void AdjustZoom(float delta) {
        if (isOrthographic) {
            zoom = Mathf.Clamp01(zoom + delta);

            // adjust the position of the stick
            float distance = Mathf.Lerp(maxSize, minSize, zoom);
            camera.orthographicSize = distance;
        }
        else {
            zoom = Mathf.Clamp01(zoom + delta);

            // adjust the position of the stick
            float distance = Mathf.Lerp(maxFOV, minFOV, zoom);
            camera.fieldOfView = distance;
        }
    }

    void AdjustRotation(float delta) {
        rotationAngle += delta * rotationSpeed * Time.deltaTime;
        if (rotationAngle < 0f) {
            rotationAngle += 360f;
        }
        else if (rotationAngle >= 360f) {
            rotationAngle -= 360f;
        }
        transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }

    void AdjustPosition(float xDelta, float zDelta) {
        Vector3 direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        float distance =
            Mathf.Lerp(moveSpeedMaxZoom, moveSpeedMinZoom, zoom) *
            damping * Time.deltaTime;

        Vector3 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = ClampPosition(position);
    }

    Vector3 ClampPosition(Vector3 position) {
        float xMax = (grid.cellCountX - 0.5f) * (2f * HexMetrics.innerRadius) / 2f;
        position.x = Mathf.Clamp(position.x, -xMax, xMax);

        float zMax = (grid.cellCountZ - 1) * (1.5f * HexMetrics.outerRadius) / 2f;
        position.z = Mathf.Clamp(position.z, -zMax, zMax);

        return position;
    }

    public static void ValidatePosition() {
        instance.AdjustPosition(0f, 0f);
    }
}
