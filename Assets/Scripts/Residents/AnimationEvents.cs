using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject arrow;
    public Animator bowAnimation;
    public Animator playerAnimation;
    public GameObject location;

    public GameObject parent;

    public void ShootBow()
    {
        bowAnimation.SetTrigger("Shoot");
    }

    public void ShootArrow()
    {
        GameObject newArrow = Instantiate(arrow, bowAnimation.gameObject.transform.position, Quaternion.identity, parent.transform);

        Vector3 directionToTarget = (location.transform.position - newArrow.transform.position).normalized;
        newArrow.transform.rotation = Quaternion.LookRotation(directionToTarget);

        newArrow.transform.Rotate(0, 90, 0);

        Rigidbody rigidBody = newArrow.GetComponent<Rigidbody>();
        rigidBody.AddForce(directionToTarget * 2, ForceMode.Impulse);
    }
}
