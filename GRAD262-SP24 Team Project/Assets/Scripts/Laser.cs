using UnityEngine;
using UnityEngine.Events;
using VolumetricLines;

public class Laser : MonoBehaviour
{
    public UnityEvent<Laserable.LaserableElements, float> onElementLasered;
    public VolumetricLineBehavior laserLine;
    public AudioSource firingSFX;
    public AudioSource collidingSFX;
    public float maxLaserDistance = 500;

    private void Awake()
    {
        laserLine.EndPos = new Vector3(0, 0, maxLaserDistance);
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ActivateLaser();
        }
        else
        {
            DeactivateLaser();
        }
    }

    private void ActivateLaser()
    {
        laserLine.gameObject.SetActive(true);

        if (firingSFX && !firingSFX.isPlaying)
            firingSFX.Play();

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxLaserDistance)
            && hit.collider.gameObject.GetComponent<Laserable>())
        {
            laserLine.EndPos = new Vector3(0, 0, hit.distance);

            Laserable laserable = hit.collider.gameObject.GetComponent<Laserable>();

            if (laserable.elementAmount > 0)
            {
                float amountLasered = Mathf.Min(laserable.elementAmount, (float)laserable.element * Time.deltaTime);

                laserable.elementAmount -= amountLasered;
                onElementLasered.Invoke(laserable.element, amountLasered);
            }

            if (collidingSFX)
            {
                collidingSFX.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + hit.distance);

                if (laserable.elementAmount > 0)
                {
                    if (!collidingSFX.isPlaying)
                        collidingSFX.Play();
                }
                else if (collidingSFX.isPlaying)
                {
                    collidingSFX.Stop();
                }
            }
        }
    }

    private void DeactivateLaser()
    {
        if (firingSFX && firingSFX.isPlaying)
            firingSFX.Stop();

        if (collidingSFX && collidingSFX.isPlaying)
            collidingSFX.Stop();

        laserLine.gameObject.SetActive(false);
        laserLine.EndPos = new Vector3(0, 0, maxLaserDistance);
    }
}
