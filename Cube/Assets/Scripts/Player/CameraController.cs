using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private CoroutineHandler coroutineHandler;
    [SerializeField] private Map map;
    [SerializeField] private VectorVariable LocalX;
    [SerializeField] private VectorVariable LocalZ;
    [SerializeField] private Transform target;
    [SerializeField] private Transform origin;
    [SerializeField] private float distance;
    [SerializeField] private float maxAngle;
    [SerializeField] private FloatVariable playerSpeed;

    private ActionManager actionManager;
    private Action a;
    private Quaternion start;
    private Quaternion end;
    private float t;
    private Quaternion startRotation;
    private Vector3 currentStartRotation;
    private AnimationCurve curve;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
        eventSystem.AddListener<Events.PlayerMoveEvent>(OnPlayerMove);
        actionManager = new ActionManager(coroutineHandler);
        a = new Action(RotateMotherFucka);

        startRotation = Quaternion.LookRotation(transform.forward, LocalZ);
        currentStartRotation = Vector3.Cross(LocalX, LocalZ);
        curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    }

    private void LateUpdate()
    {

        Vector3 dir = (target.position - origin.position).normalized;

        transform.position = target.position + distance * dir;
    }

    private ActionState RotateMotherFucka(float dt)
    {
        t += dt;
        transform.rotation = Quaternion.Lerp(start, end, curve.Evaluate(t / playerSpeed.Value));

        return transform.rotation == end ? ActionState.Success : ActionState.Running;
    }

    private void OnPlayerMove(Events.BaseEvent e)
    {
        Events.PlayerMoveEvent moveEvent = (Events.PlayerMoveEvent)e;

        Vector3 xVector = new Vector3(moveEvent.target.x * LocalX.Value.x, moveEvent.target.y * LocalX.Value.y, moveEvent.target.z * LocalX.Value.z);

        float localX = Mathf.Abs(xVector.magnitude) / (map.CubeSize / 2f);

        Vector3 zVector = new Vector3(moveEvent.target.x * LocalZ.Value.x, moveEvent.target.y * LocalZ.Value.y, moveEvent.target.z * LocalZ.Value.z);

        float localZ = Mathf.Abs(zVector.magnitude) / (map.CubeSize / 2f);

        if (xVector.x > origin.position.x || xVector.y > origin.position.y || xVector.z > origin.position.z)
            localX *= -1;
        if (zVector.x < origin.position.x || zVector.y < origin.position.y || zVector.z < origin.position.z)
            localZ *= -1;


        if (Vector3.Cross(LocalX, LocalZ) != currentStartRotation)
        {
            currentStartRotation = Vector3.Cross(LocalX, LocalZ);

            startRotation = Quaternion.LookRotation(currentStartRotation.normalized, LocalZ);
        }

        t = 0;
        start = transform.rotation;
        end = startRotation * Quaternion.AngleAxis(localX * maxAngle, Vector3.up) * Quaternion.AngleAxis(localZ * maxAngle, Vector3.right);

        actionManager.UseAction(a);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<Events.PlayerMoveEvent>(OnPlayerMove);
    }
}
