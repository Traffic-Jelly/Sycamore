using UnityEngine;

[CreateAssetMenu (menuName = "Sycamore/Dialogue/Typing Delay")]
public class TypingDelays : ScriptableObject
{
	public float characterDelay = 0.05f;
	public float sentenceDelay = 0.5f;
	public float commaDelay = 0.1f;
	public float finalDelay = 0.5f;
}