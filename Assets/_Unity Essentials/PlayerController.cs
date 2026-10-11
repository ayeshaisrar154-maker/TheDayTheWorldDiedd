using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 8f;
	public float jumpForce = 7f;
	public float rotationSpeed = 10f; // Speed at which the character turns

	private Rigidbody rb;
	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		// Get WASD / Arrow Key Inputs
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveZ = Input.GetAxisRaw("Vertical");

		// Direction vector based on keyboard input
		Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

		// Apply movement velocity
		rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

		// ROTATION LOGIC: If the player is moving, rotate toward the movement direction
		if (moveDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
		}

		// Jump input
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// Reset jump when touching the floor
		if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.name == "Plane")
		{
			isGrounded = true;
		}
	}
}