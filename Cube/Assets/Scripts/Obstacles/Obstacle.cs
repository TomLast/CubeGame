using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{
    public System.Action OnCollision { get; set; }
    [SerializeField] private CoroutineHandler _coroutineHandler;
    [SerializeField] private ObjectPool _colliderPool;


    private MeshRenderer _renderer;
    private IEnumerator _moveCoroutine;
    private IEnumerator _rotateCoroutine;

    private void Awake() => _renderer = GetComponent<MeshRenderer>();

    public void SetMaterial(Material mat) => _renderer.material = mat;
    public void Move(Vector3 startPos, Vector3 endPos, float time, System.Action reachedTarget = null, AnimationCurve offset = null, Vector3 offsetAxis = default, float offsetStrength = 0)
    {
        _moveCoroutine = MoveCoroutine(startPos, endPos, time, offset, offsetAxis, offsetStrength, reachedTarget);
        _coroutineHandler.StartCoroutine(_moveCoroutine);
    }

    public void Rotate(Vector3 axis, float speed, float time)
    {
        _rotateCoroutine = RotateCoroutine(axis, speed, time);
        _coroutineHandler.StartCoroutine(_rotateCoroutine);
    }
    public void Reset()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        _colliderPool.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision?.Invoke();
    }

    private IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos, float time, AnimationCurve offset = null, Vector3 offsetAxis = default, float offsetStrength = 0, System.Action reachedTarget = null)
    {
        float progress = 0f;
        offset = offset ?? AnimationCurve.Constant(0f, 0f, 0f);

        while (transform.position != endPos)
        {
            progress += Mathf.Clamp01(Time.deltaTime / time);
            transform.position = Vector3.Lerp(startPos, endPos, progress);
            transform.position += (offsetStrength * offset.Evaluate(progress)) * offsetAxis;
            yield return null;
        }
        reachedTarget?.Invoke();
    }

    private IEnumerator RotateCoroutine(Vector3 axis, float speed, float time)
    {
        float t = 0f;
        while ((t += Time.deltaTime) < time)
        {
            transform.Rotate(axis, speed * Time.deltaTime);
            yield return null;
        }
    }
}
