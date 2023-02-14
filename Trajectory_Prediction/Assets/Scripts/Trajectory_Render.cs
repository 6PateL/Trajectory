using System;
using UnityEngine;

public class Trajectory_Render : MonoBehaviour
{
   private LineRenderer _lineRendererComponent;

   private void Start()
   {
      _lineRendererComponent = GetComponent<LineRenderer>(); 
   }

   public void ShowTrajectory(Vector3 origin, Vector3 speed)
   {
      Vector3[] points = new Vector3[100];
      _lineRendererComponent.positionCount = points.Length;

      for (int i = 0; i < points.Length; i++)
      {
         float time = i * 0.1f;

         points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

         if (points[i].y < 0)
         {
            _lineRendererComponent.positionCount = i + 1; 
            break;
         }
      }
      _lineRendererComponent.SetPositions(points);
   }
}
