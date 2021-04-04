using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class SimpleSampleCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct,
        Agent
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;

    private Vector3 m_currentDirection = Vector3.zero;

    private List<Collider> m_collisions = new List<Collider>();

    private NavMeshAgent navMeshAgent;

    private Vector3 smoothDeltaPosition = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
        m_animator.SetBool("Grounded", true);
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                m_controlMode = ControlMode.Agent;

                Vector3 destination = hit.point;
                destination.x = Mathf.Clamp(destination.x, -3.17f, 4.33f);
                destination.z = Mathf.Clamp(destination.z, -2.55f, 5.68f);
                navMeshAgent.SetDestination(destination);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
    }

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", true);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;
            case ControlMode.Agent:
                AgentUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            Vector3 newPosition = transform.position + m_currentDirection * m_moveSpeed * Time.deltaTime;

            Vector3 restrictedNewPosition = new Vector3(Mathf.Clamp(newPosition.x, -4.78f, 4.33f), newPosition.y, Mathf.Clamp(newPosition.z, -2.55f, 5.68f));
            navMeshAgent.SetDestination(restrictedNewPosition);
            transform.position = restrictedNewPosition;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }
    }

    // https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
    private void AgentUpdate()
    {
        Vector3 worldDeltaPosition = navMeshAgent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector3.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }

        bool shouldMove = navMeshAgent.remainingDistance > navMeshAgent.radius;
        if (shouldMove)
        {
            m_animator.SetFloat("MoveSpeed", velocity.magnitude);
        }
        else
        {
            m_controlMode = ControlMode.Direct;
            m_animator.SetFloat("MoveSpeed", 0f);
        }

        Vector3 newPosition = navMeshAgent.nextPosition;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
