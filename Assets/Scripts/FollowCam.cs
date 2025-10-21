using UnityEngine;

public class FollowCam : MonoBehaviour {
    public Transform target;
    public float smoothTime = 0.5f;

    private Vector3 velocity = Vector3.zero;

    // effectively attaches camera to player
    private void LateUpdate() {
        // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        // camera lags behind player, by some time. in this case, it's 0.5f (0.5 seconds).

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
    
    
}
