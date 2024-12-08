using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private Vector3 direction;
// recieve input from player
// apply movement in direction to sprite -> find current position and transform.position -> determine speed
private void Update()
{
    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");

    direction = new Vector3(horizontal, vertical);

    AnimateMovement(direction);

}
private void FixedUpdate()
{
    this.transform.position += direction * speed * Time.deltaTime;
}
void AnimateMovement(Vector3 direction)
{
    if(animator != null)
    {
        if(direction.magnitude > 0)
        {
            animator.SetBool("isMoving", true);

            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);

        }
        else
        {
            animator.SetBool("isMoving", false);

        }
    }
}

}
