using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class UpdatableData : ScriptableObject {

	public event System.Action OnValuesUpdated;
	public bool autoUpdate;

	protected virtual void OnValidate() {
		if (autoUpdate) {
			#if UNITY_EDITOR
						EditorApplication.update += NotifyOfUpdatedValues;
			#endif
		}
	}

	public void NotifyOfUpdatedValues() {
		#if UNITY_EDITOR
				EditorApplication.update -= NotifyOfUpdatedValues;
		#endif
		if (OnValuesUpdated != null) {
			OnValuesUpdated ();
		}
	}

}
