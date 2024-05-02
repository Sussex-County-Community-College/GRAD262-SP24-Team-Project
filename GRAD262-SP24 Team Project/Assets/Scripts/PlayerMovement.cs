using UnityEngine;

public class PlayerMovement : ShipMovement
{
    [Header("Mouse Deadzone")]
    public float deadZoneRadius = 0.5f;

    private Vector3 _centerOfScreen;

    override protected void Start()
    {
        base.Start();
        _centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    override protected float GetThrust()
    {
        return Input.GetAxis("Horizontal");
    }

    override protected float GetRoll()
    {
        return Input.GetAxis("Vertical");
    }

    override protected float GetYaw()
    {
        return (Input.mousePosition - _centerOfScreen).x / _centerOfScreen.x;
    }

    override protected float GetPitch()
    {
        return (Input.mousePosition - _centerOfScreen).y / _centerOfScreen.y;
    }

}

