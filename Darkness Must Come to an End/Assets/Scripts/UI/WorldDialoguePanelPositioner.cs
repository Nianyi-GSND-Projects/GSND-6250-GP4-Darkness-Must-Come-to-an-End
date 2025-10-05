using UnityEngine;

namespace Game
{
	public class WorldDialoguePanelPositioner : MonoBehaviour
	{
		public Transform speaker;
		Camera cam;
		RectTransform rt;

		[Min(0)] public float distanceToCam = 1;

		protected void Awake()
		{
			cam = Camera.main;
			rt = GetComponent<RectTransform>();
		}

		protected void LateUpdate()
		{
			Vector3 screenPos = cam.WorldToScreenPoint(speaker.transform.position);
			if(screenPos.z < 0)
			{
				screenPos.x *= -1e5f;
				screenPos.y *= -1;
			}
			screenPos.z = 1;

			Vector3 pos = screenPos;
			Vector2 margin = rt.sizeDelta * .5f;
			pos.x = Mathf.Clamp(pos.x, margin.x, Screen.width - margin.x);
			pos.y = Mathf.Clamp(pos.y, margin.y, Screen.height - margin.y);
			bool outRanged = pos != screenPos;

			transform.position = pos;
		}
	}
}
