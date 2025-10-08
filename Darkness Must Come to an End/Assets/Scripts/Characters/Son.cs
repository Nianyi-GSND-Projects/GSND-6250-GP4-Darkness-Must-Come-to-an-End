using UnityEngine;
using System.Collections.Generic;

namespace Game
{
	[RequireComponent(typeof(CharacterController))]
	public class Son : MonoBehaviour
	{
		Player player;
		CharacterController controller;

		[SerializeField] Transform jenny;
		[SerializeField][Min(0)] float barkingDistance = 10f, abandonDistance = 25f;
		Vector3 lastPos;
		bool barked = false, shouldBark = false;
		Vector3 PlayerPos => player.transform.position;
		[SerializeField] List<Dialogue> sonBarkings;
		[SerializeField] string sonAbandoned;

		[SerializeField][Min(0)] float followDistance = 3f;
		[SerializeField][Min(0)] float moveSpeed = 1.8f;

		protected void Awake()
		{
			player = FindObjectOfType<Player>();
			controller = GetComponent<CharacterController>();
		}

		protected void Start()
		{
			lastPos = PlayerPos;
		}

		protected void FixedUpdate()
		{
			if(player)
			{
				UpdateLastPos();
				FollowPlayer();
			}
			if(shouldBark && !barked)
			{
				var i = Mathf.FloorToInt(Random.value * sonBarkings.Count);
				GameManager.Instance.PlayDialogue(sonBarkings[i]);
				barked = true;
			}
			if(Vector3.Distance(transform.position, PlayerPos) > abandonDistance)
			{
				GameManager.Instance.ShowControlGuidance(sonAbandoned);
				enabled = false;
			}
		}

		void UpdateLastPos()
		{
			if(Vector3.Distance(jenny.position, lastPos) > Vector3.Distance(jenny.position, PlayerPos))
			{
				lastPos = player.transform.position;
				barked = false;
				shouldBark = false;
			}
			else if(Vector3.Distance(transform.position, PlayerPos) > barkingDistance)
			{
				shouldBark = true;
			}
		}

		void FollowPlayer()
		{
			var delta = Vector3.ProjectOnPlane(lastPos - transform.position, Vector3.up);
			if((float)delta.magnitude <= followDistance)
				return;
			controller.SimpleMove(delta.normalized * moveSpeed);
		}
	}
}
