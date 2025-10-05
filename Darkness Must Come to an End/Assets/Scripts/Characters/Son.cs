using UnityEngine;

namespace Game
{
	[RequireComponent(typeof(CharacterController))]
	public class Son : MonoBehaviour
	{
		Player player;
		CharacterController controller;

		[SerializeField][Min(0)] float followDistance = 3f;
		[SerializeField][Min(0)] float moveSpeed = 1.8f;

		protected void Awake()
		{
			player = FindObjectOfType<Player>();
			controller = GetComponent<CharacterController>();
		}

		protected void FixedUpdate()
		{
			if(player)
				FollowPlayer();
		}

		void FollowPlayer()
		{
			var delta = Vector3.ProjectOnPlane(player.transform.position - transform.position, Vector3.up);
			if((float)delta.magnitude <= followDistance)
				return;
			controller.SimpleMove(delta.normalized * moveSpeed);
		}
	}
}
