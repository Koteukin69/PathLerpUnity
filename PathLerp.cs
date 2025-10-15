using System.Linq;
using UnityEngine;

public static class PathLerp
{
    public static Vector3 Lerp(Vector3[] path, float t)
    {
        t = Mathf.Clamp01(t);
        if (Mathf.Approximately(t, 1)) return path[^1];
        
        float[] d = Distances(path);
        float dSum = Sum(d);
        
        for (int i = 0; i < path.Length; i++)
        {
            if (Sum(Slice(d, 0, i)) >= dSum * t)
                return Vector3.Lerp(path[i], path[i + 1], 
                    (t * dSum - Sum(Slice(d, 0, i - 1))) / d[i]);
        }
        return path[0];
    }
    
    public static float SumDistance(Vector3[] path) => Sum(Distances(path));
    
    private static float[] Distances(Vector3[] path)
    {
        float[] d = new float[path.Length - 1];
        for (int i = 0; i < path.Length - 1; i++)
            d[i] = (path[i] - path[i + 1]).magnitude;
        return d;
    }
    
    private static float Sum(float[] iterable) => iterable.Length == 0 ? 0 
        : iterable.Aggregate((a, b) => a + b);
    
    private static float[] Slice(float[] array, int start, int end)
    {
        float[] toReturn = new float[end - start + 1];
        for (int i = start; i < end + 1; i++) toReturn[i - start] = array[i];
        return toReturn;
    }
}
