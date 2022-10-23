using UnityEngine;

[CreateAssetMenu(fileName ="Vector3", menuName = "Variables/Vector3")]
public class VectorVariable : Variable<Vector3>
{
    public static implicit operator Vector3(VectorVariable v) => v.Value;
}