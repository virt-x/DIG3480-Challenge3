using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public float dodge, smoothing, tilt;
    public Vector2 startWait, maneuverTime, maneuverWait;
    public Boundary bound;
    private float targetManeuver;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(Mathf.Min(startWait.x, startWait.y), Mathf.Max(startWait.x, startWait.y)));
        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        body.velocity = new Vector3(Mathf.MoveTowards(body.velocity.x, targetManeuver, Time.deltaTime * smoothing), 0, body.velocity.z);
        body.position = new Vector3(Mathf.Clamp(body.position.x, bound.xMin, bound.xMax), 0, body.position.z);
        body.rotation = Quaternion.Euler(0, 0, body.velocity.x * -tilt);
    }
}
