using UnityEngine;

namespace Game
{
	public class Store2Controller : MonoBehaviour
	{
		public void Close()
		{
			// TODO
			foreach(var renderer in GetComponentsInChildren<Renderer>())
				renderer.material.color = Color.gray;
		}
	}
}
