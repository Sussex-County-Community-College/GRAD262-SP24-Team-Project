using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes the camera to try to move to camPoint and LookAt lookPoint, which are
/// both empty GameObject children of Plane.
/// </summary>
[DisallowMultipleComponent]
public class ChaseCam : MonoBehaviour
{
    [Header("Inscribed")]
    public Transform camPoint;
    public Transform lookPoint;
    public float easing = 0.15f;


    void FixedUpdate()
    {
        // Call Follow() every FixedUpdate().
        Follow();

        // This happens in FixedUpdate rather than Update so that the Camera
        //  moves in synchronization with the movement of the plane.
    }


    void Follow()
    {
        // We want the camera position to move toward the CamPoint child of
        //  Plane and look at the LookPoint child of Plane
        // Set transform.position based on a Zeno's Paradox version of linear
        //  interpolation with easing as the U value. Look up Zeno's Paradox
        //  in Appendix B or look at how the camera moved in Mission Demolition
        transform.position = Vector3.Lerp(transform.position, camPoint.position, easing);

        // Use transform.LookAt to make the Camera look at lookPoint.position
        transform.LookAt(lookPoint.position);

    }


    /// <summary>
    /// You can use this to call Follow() while working in the Editor. It doesn't
    ///  always work perfectly, because the Editor doesn't draw gizmos every
    ///  frame when you're not playing, but it is still helpful to automatically
    ///  position the Camera.
    /// </summary>
    private void OnDrawGizmos()
    {
        // I've written this method for you; you don't need to add anything.
        if (camPoint == null || lookPoint == null) return;
        if (!Application.isPlaying)
        {
            Follow();
        }
    }
}
