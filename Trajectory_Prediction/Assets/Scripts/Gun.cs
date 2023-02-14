using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Gun : MonoBehaviour
{
  [SerializeField] private GameObject _bulletPrefab;
  [SerializeField] private float power = 100;

  [SerializeField] private Trajectory_Render Trajectory;
  //[SerializeField] private Trajectory_Render_Advanced Trajectory; 

  private Camera _camera;

  private void Start()
  {
    _camera = Camera.main;
  }

  private void Update()
  {
    float enter;
    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    new Plane(-Vector3.forward,transform.position).Raycast(ray,out enter);
    Vector3 mouseInWorld = ray.GetPoint(enter);

    Vector3 speed = (mouseInWorld - transform.position) * power;
    transform.rotation = Quaternion.LookRotation(speed);
    Trajectory.ShowTrajectory(transform.position, speed);

    if (Input.GetMouseButtonDown(0))
    {
      Rigidbody bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
      bullet.AddForce(speed, ForceMode.VelocityChange);
      //Trajectory.AddBody(bullet); 
    }
  }
}
