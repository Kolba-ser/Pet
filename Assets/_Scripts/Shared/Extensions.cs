using Pet.Data;
using System;
using UnityEngine;

public static class Extensions
{
    public static Vector3Reference AsVector3Data(this Vector3 v3) =>
        new Vector3Reference(v3.x, v3.y, v3.z);

    public static Vector3 AsUnityVector3(this Vector3Reference v3) =>
        new Vector3(v3.X, v3.Y, v3.Z);

    public static string ToJson(this object obj) => JsonUtility.ToJson(obj);

    public static T ToDeserialized<T>(this string json) =>
        JsonUtility.FromJson<T>(json);

    public static void Log<T>(this T data) =>
        Debug.Log(data);

    public static Vector3 AddY(this Vector3 v3, float offsetY)
    {
        v3.y += offsetY;
        return v3;
    }

    public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
    {
        if (component)
            action?.Invoke(component);
    }

    public static void DrawRay(Vector3 worldPosition, float radius, float secods)
    {
        Debug.DrawRay(worldPosition, radius * Vector3.up, Color.red, secods);
        Debug.DrawRay(worldPosition, radius * Vector3.down, Color.red, secods);
        Debug.DrawRay(worldPosition, radius * Vector3.right, Color.red, secods);
        Debug.DrawRay(worldPosition, radius * Vector3.left, Color.red, secods);
        Debug.DrawRay(worldPosition, radius * Vector3.forward, Color.red, secods);
        Debug.DrawRay(worldPosition, radius * Vector3.back, Color.red, secods);
    }
}