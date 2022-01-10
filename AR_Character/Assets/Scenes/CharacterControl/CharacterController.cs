using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterController : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private Animator _anim;
    protected Transform Destination;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.speed = 1.5f;
        Destination = GameObject.FindWithTag("Player").transform;
        _raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) return;

        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(TouchHelper.TouchPosition, hits, TrackableType.Planes);
        if (hits.Count == 0) return;
        Destination.transform.position = hits[0].pose.position;
        Rotate();
        MoveTo(hits[0].pose.position);
    }
    protected virtual void Rotate()
    {
        var direction = Destination.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    protected virtual void MoveTo(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
    }
}
