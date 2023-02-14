using System.Collections.Generic;
using UnityEngine;

public class Trajectory_Render_Advanced : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab; 
    private LineRenderer _lineRendererComponent;
    private Dictionary<Rigidbody, BodyData> savedBodies = new Dictionary<Rigidbody, BodyData>(); 

    private void Start()
    { 
        _lineRendererComponent = GetComponent<LineRenderer>();

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            savedBodies.Add(rb, new BodyData());
        }
    }

    public void AddBody(Rigidbody rb) => savedBodies.Add(rb, new BodyData()); 

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        foreach (var body in savedBodies)
        {
            body.Value.position = body.Key.transform.position;
            body.Value.rotation = body.Key.transform.rotation;
            body.Value.velocity = body.Key.velocity;
            body.Value.angularVelocity = body.Key.angularVelocity;
        }
        
        GameObject bullet = Instantiate(_bulletPrefab, origin, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(speed, ForceMode.VelocityChange);

        Physics.autoSimulation = false; 
            
        Vector3[] points = new Vector3[100];

        for (int i = 0; i < points.Length; i++)
        {
            Physics.Simulate(.1f);

            points[i] = bullet.transform.position;
        }
        _lineRendererComponent.SetPositions(points);

        Physics.autoSimulation = true;
        
        foreach (var body in savedBodies)
        {
            body.Value.position = body.Key.transform.position;
            body.Value.rotation = body.Key.transform.rotation;
            body.Value.velocity = body.Key.velocity;
            body.Value.angularVelocity = body.Key.angularVelocity;
        }
        
        Destroy(bullet.gameObject);
    }
}

public class BodyData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity; 
}
