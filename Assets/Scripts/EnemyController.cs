using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform player;
    public float detectionDist;
    public float detectionAngle;
    public Vector3[] points;

    public AudioClip roar;
    public AudioSource step;

    private Animator anim;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    private int destPoint = 0;
    private float dist, angle;
    private bool coroutineStarted = false;
    private bool spotted;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GotoNextPoint();
    }

    void Update() {
        Debug.DrawRay(transform.position, transform.forward * 5);
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f && coroutineStarted == false) {
                    StartCoroutine("PauseToLook");
                }
            }
        }
    }

    void FixedUpdate() {
        dist = Vector3.Distance(player.position, transform.position);
        angle = Vector3.Angle(transform.forward, player.position - transform.position);
        //Debug.DrawRay(transform.position + new Vector3(0, 3f, 0), player.position - transform.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), player.position - transform.position, out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
            if (hit.transform.tag == "Player" && dist < detectionDist && angle < detectionAngle && angle > -detectionAngle) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Roar") ) anim.SetInteger("spotted", 0);

                anim.SetInteger("moving", 0);
                anim.SetInteger("waiting", 0);
                if (!spotted) {
                    step.Stop();
                    audioSource.PlayOneShot(roar, 0.7f);
                    anim.SetInteger("spotted", 1);
                }

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                transform.LookAt(player);
                spotted = true;
            }
            if (hit.transform.tag != "Player" && spotted) {
                step.Play();
                spotted = false;
                Debug.Log("No longer spotted");
                anim.SetInteger("spotted", 0);
                anim.SetInteger("moving", 1);
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            }
        }
    }

    IEnumerator PauseToLook() {
        step.Stop();
        coroutineStarted = true;
        anim.SetInteger("moving", 0);
        anim.SetInteger("waiting", 1);
        yield return new WaitForSeconds(3.5f);
        GotoNextPoint();
    }

    void GotoNextPoint() {
        coroutineStarted = false;
        anim.SetInteger("moving", 1);
        anim.SetInteger("waiting", 0);
        step.Play();
        if (points.Length == 0) {
            return;
        }

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
