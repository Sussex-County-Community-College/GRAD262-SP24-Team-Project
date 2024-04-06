using UnityEngine;

public class Laserable : MonoBehaviour
{
    // Assigned integer can be interpreted 
    // value that can be mined per second.
    public enum LaserableElements
    {
        None = 0,
        Cobalt = 1,
        Gold = 10,
        Platinum = 12,
        Silver = 5,
        Titanium = 3
    };

    public LaserableElements element = LaserableElements.None;
    public float elementAmount = 0;

    private void Start()
    {
        elementAmount *= transform.localScale.x;
    }

    private void Update()
    {
        if (elementAmount <= 0)
            Destroy(gameObject);
    }
}
