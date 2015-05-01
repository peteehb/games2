using UnityEngine;
using System.Collections;
using BGE;

public class Technic : MonoBehaviour {

    [Header("Seek")]
    public Vector3 seekTarget;
    public bool seekEnabled;
    
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass;
    public float maxSpeed;

    public Technic()
    {
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 10.0f;
    }

    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 desired = seekTarget - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        LineDrawer.DrawTarget(seekTarget, Color.blue);
        return desired - velocity;
    }

	// Update is called once per frame
	void Update () {
        force += Seek(seekTarget);

        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, maxSpeed);


        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity.normalized;
            velocity *= 0.99f;
        }

        force = Vector3.zero;

	}
}
