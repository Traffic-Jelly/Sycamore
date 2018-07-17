using UnityEngine;
using UnityEditor;

namespace Sycamore.FX.UX
{
	[CustomEditor (typeof (BackgroundManager))]
	public class BackgroundManagerEditor : Editor
	{
		private BackgroundManager _target;

		public override void OnInspectorGUI ()
		{
			_target = target as BackgroundManager;

			EditorGUI.BeginChangeCheck ();
			var colorA = EditorGUILayout.ColorField (new GUIContent ("Color A"), _target.GetColorA ());
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target.Material, "Changed Color A");
				_target.SetColorA (colorA);
			}

			EditorGUI.BeginChangeCheck ();
			var colorB = EditorGUILayout.ColorField (new GUIContent ("Color B"), _target.GetColorB ());
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target.Material, "Changed Color B");
				_target.SetColorB (colorB);
			}

			EditorGUI.BeginChangeCheck ();
			var scale = EditorGUILayout.FloatField (new GUIContent ("Scale"), _target.GetScale ());
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target.Material, "Changed Scale");
				_target.SetScale (scale);
			}

			EditorGUI.BeginChangeCheck ();
			var octaves = EditorGUILayout.Slider (new GUIContent ("Octaves"), _target.GetOctaves (), 1f, 3f);
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target.Material, "Changed Octaves");
				_target.SetOctaves (octaves);
			}

			EditorGUI.BeginChangeCheck ();
			var bias = EditorGUILayout.FloatField (new GUIContent ("Bias"), _target.GetBias ());
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target.Material, "Changed Bias");
				_target.SetBias (bias);
			}

			EditorGUI.BeginChangeCheck ();
			var speed = EditorGUILayout.FloatField (new GUIContent ("Speed"), _target.GetSpeed ());
			if (EditorGUI.EndChangeCheck ())
			{
				Undo.RecordObject (_target, "Changed Speed");
				_target.SetSpeed (speed);
			}

			Repaint ();
		}
	}
}