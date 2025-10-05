using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

namespace NaniCore.Bordure {
	public class Trigger : MonoBehaviour {
		#region Serialized fields
		public bool oneTime, hasExit;
		public UnityEvent onEnter;
		[ShowIf("hasExit")] public UnityEvent onExit;
		#endregion

		#region Life cycle
		protected void OnTriggerEnter(Collider other) {
			onEnter?.Invoke();
			if(oneTime && !hasExit) {
				Destroy(this);
			}
		}

		protected void OnTriggerExit(Collider other) {
			onExit?.Invoke();
			if(oneTime)
			{
				Destroy(this);
			}
		}
		#endregion
	}
}
