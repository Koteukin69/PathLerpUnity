using System;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    [SerializeField] private Vector3[] _points;
    [SerializeField] private float _speed = 1f;

    private bool _reached;
    
    public event EventHandler ReachedEvent;

    private void Start()
    {
#if UNITY_EDITOR
        ReachedEvent += (_, _) => { Debug.Log(1); };
#endif
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        for (int i = 0; i < _points.Length - 1; i++)
            Debug.DrawLine(_points[i], _points[i + 1], Color.red);
#endif
        
        Move(_points, T);
    }

    private void Move(Vector3[] points, float t)
    {
        if (_reached) return;
        transform.position = PathLerp.Lerp(points, t);
        
        if (t < 1) return;
        ReachedEvent?.Invoke(this, EventArgs.Empty);
        _reached = true;
    }

    private float T => Time.time * _speed / PathLerp.SumDistance(_points);

    public void SetPoints(Vector3[] points)
    {
        if (Equals(_points, points)) return;
        _points = points;
    }
}
