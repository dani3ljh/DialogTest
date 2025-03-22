using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
	[Header("Objects")]
	[SerializeField] private TMP_Text text;
	[SerializeField] private Image speaker;

	[Header("Data")]
	public Dialog[] dialogs;

	private Coroutine currCoroutine;
	private int dialogIndex;
	private bool isRunning;

	public void OpenDialogBox() {
		currCoroutine = StartCoroutine(RunDialog(dialogs[dialogIndex]));
	}

	private void Update() {
		if (Input.GetButtonDown("Interact") && !isRunning) {
			dialogIndex++;
			if (dialogIndex < dialogs.Length) {
				currCoroutine = StartCoroutine(RunDialog(dialogs[dialogIndex]));
			} else {
				gameObject.SetActive(false);
			}
		}
		if (Input.GetButtonDown("Skip") && isRunning) {
			StopCoroutine(currCoroutine);
			isRunning = false;
			text.text = dialogs[dialogIndex].GetText();
		}
	}

	private IEnumerator RunDialog(Dialog dialog) {
		isRunning = true;

		Sprite speakerSprite = dialog.GetSpeakerSprite();
		if (speakerSprite != null)
			speaker.sprite = speakerSprite;

		string textData = dialog.GetText();
		if (!dialog.IsAnimated() || dialog.GetAnimationDelay() <= 0f) {
			text.text = textData;
			isRunning = false;
			yield break;
		}

		for (int i = 1; i <= textData.Length; i++) {
			if (!dialog.IsAnimated())
				i = textData.Length;
			text.text = textData[..i];
			yield return new WaitForSeconds(dialog.GetAnimationDelay());
		}

		isRunning = false;
	}
}

[System.Serializable]
public class Dialog
{
	[SerializeField] private string text;
	[SerializeField] private Sprite speakerSprite;
	[SerializeField] private bool animated;
	[SerializeField] private float animationDelay;

	public Dialog(string text) {
		this.text = text;
	}

	public Dialog(string text, Sprite speakerSprite) {
		this.text = text;
		this.speakerSprite = speakerSprite;
	}

	public Dialog(string text, float animationDelay) {
		this.text = text;
		animated = true;
		this.animationDelay = animationDelay;
	}

	public Dialog(string text, Sprite speakerSprite, float animationDelay) {
		this.text = text;
		this.speakerSprite = speakerSprite;
		animated = true;
		this.animationDelay = animationDelay;
	}

	public string GetText() {
		return text;
	}

	public Sprite GetSpeakerSprite() {
		return speakerSprite;
	}

	public bool IsAnimated() {
		return animated;
	}

	public float GetAnimationDelay() {
		return animationDelay;
	}
	
	public void SetText(string text) {
		this.text = text;
	}

	public void SetSpeakerSprite(Sprite speakerSprite) {
		this.speakerSprite = speakerSprite;
	}

	public void SetAnimated(bool animated) {
		this.animated = animated;
	}

	public void SetAnimationDelay(float animationDelay) {
		this.animated = true;
		this.animationDelay = animationDelay;
	}
}