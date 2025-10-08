using UnityEngine;

namespace Game
{
	public class Store2Controller : MonoBehaviour
	{
		[SerializeField] new Light light;

		public void Close()
		{
			light.enabled = false;
		}
	}
}
