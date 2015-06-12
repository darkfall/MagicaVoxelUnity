using System.Collections.Generic;
using UnityEngine;

namespace AU
{

	public class BezierPath
	{
		List<Vector3> _ControlPoints = new List<Vector3>();

		public List<Vector3> ControlPoints 
		{
			get { return _ControlPoints; }
		}

		public BezierPath() {}

		public BezierPath(Vector3 p1, Vector3 p2, Vector3 p3)
		{
			_ControlPoints.Add (p1);
			_ControlPoints.Add (p2);
			_ControlPoints.Add (p3);
		}	

		public void AddControlPoint(Vector3 p)
		{
			_ControlPoints.Add (p);
		}

		public void ClearControlPoints()
		{
			_ControlPoints = new List<Vector3> ();
		}

		public void RemoveControlPointAtIndex(int index)
		{
			if (index >= 0 && index < _ControlPoints.Count) 
			{
				_ControlPoints.RemoveAt(index);
			}
		}

		public void SetControlPoint(int index, Vector3 pos)
		{
			_ControlPoints [index] = pos;
		}


		public Vector3 Evaluate(float t)
		{
			float u = 1 - t;
			float bc = 1;
			float tn = 1;
			Vector3 tmp = _ControlPoints [0] * u;
			int n = _ControlPoints.Count - 1;
			for(int i=1; i<n; ++i)
			{
				tn *= t;
				bc *= (n -i + 1) / i;
				tmp = (tmp + _ControlPoints[i] * tn * bc) * u;
			}
			return (tmp + _ControlPoints [n] * tn * t);
		}

	};


}