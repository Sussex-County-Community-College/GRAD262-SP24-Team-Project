using UnityEngine;

public class PlayerMovement : ShipMovement
{
    [Header("Mouse Deadzone")]
    public float deadZoneRadius = 0.5f;

    private Vector3 _centerOfScreen;
    private bool isDocking;
    private DockingPort dockingPort;

    public float rotateSpeed = 5f;
    public float moveSpeed = 5f;

    override protected void Start()
    {
        base.Start();
        GetComponent<DockingAssist>().onDocked.AddListener(base.SetPaused);
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

    virtual protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            paused = !paused;
        }

        if (!isDocking && Input.GetKeyDown(KeyCode.B))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                DockingPort port = hit.collider.GetComponent<DockingPort>();
                if (port != null)
                {
                    isDocking = true;
                    dockingPort = port;
                }
            }
        }
        if (isDocking)
        {
            HandleDocking();
        }
            
    }

    private void HandleDocking()
    {
        Quaternion targetRotation = Quaternion.LookRotation(dockingPort.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, dockingPort.transform.position, moveSpeed * Time.deltaTime);

        float distanceToDock = Vector3.Distance(transform.position, dockingPort.transform.position);
        if (distanceToDock < 0.1f)
        {
            isDocking = false;
            dockingPort.DockShip();
        }
    }
}

