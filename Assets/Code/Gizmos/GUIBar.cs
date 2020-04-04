using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// using System;


public class GUIBar : MonoBehaviour, IResourceDisplay
{
	public RectTransform Bar;
    public RectTransform Drag;
    public Image Flash;
    public Text text;
    public RectTransform.Edge Anchor;
	Color baseColor;

	int old;
	int oldMax;
	float maxSize;

	void Awake()
	{
        switch (Anchor)
        {
            case RectTransform.Edge.Top:
            case RectTransform.Edge.Bottom:
                maxSize = Bar.sizeDelta.y;
                break;
            case RectTransform.Edge.Left:
            case RectTransform.Edge.Right:
                maxSize = Bar.sizeDelta.y;
                break;
        }
		baseColor = Flash.color;
	}

	public void SetScale(int current, int max)
	{
		if (max == 0) max = 1; // divide by zero
		if (current < 0) current = 0;
		Bar.SetInsetAndSizeFromParentEdge(Anchor, 0f, maxSize * current / max);

        if (Drag && old != current)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateDrag(current / (float)max));
        }
		if(old > current) StartCoroutine(DamageFlash());
		else Flash.color = baseColor;
		old = current;

        if(text)
		    text.text = current + "/" + max;
	}

	public void SetCurrent(int current)
	{
		if (oldMax == 0) return;
		if (current < 0) current = 0;
		Bar.SetInsetAndSizeFromParentEdge(Anchor, 0f, maxSize * current / oldMax);
        if (Drag && old != current)
        {
            StopAllCoroutines();
            StartCoroutine(AnimateDrag(current / (float)oldMax));
        }
		if (old > current) StartCoroutine(DamageFlash());
		else Flash.color = baseColor;
		old = current;
        if (text)
            text.text = current + "/" + oldMax;
	}

	public void Setmax(int max)
	{
		oldMax = max;
		if (max == 0) max = 1; // divide by zero
		Bar.SetInsetAndSizeFromParentEdge(Anchor, 0f, maxSize * old / oldMax);
		StopAllCoroutines();
		if (Drag)
			StartCoroutine(AnimateDrag(old / (float)oldMax));
        if (text)
            text.text = old + "/" + oldMax;
	}

	IEnumerator AnimateDrag(float targetValue)
	{
		float startValue = Drag.sizeDelta.x;
		float endValue = targetValue * maxSize;
        if (endValue < startValue)
        {
		    yield return new WaitForSeconds(0.75f);
            for (float t = 0f; t < 1f; t += Time.deltaTime)
            {
                // Drag.SetSizeWithCurrentAnchors(Anchor, Mathf.Lerp(startValue, endValue, t));
                Drag.SetInsetAndSizeFromParentEdge(Anchor, 0f, Mathf.Lerp(startValue, endValue, t));
                // drag.localScale = new Vector3(Mathf.Lerp(startValue, targetValue, i / 20f), 1f);
                yield return new WaitForSeconds(0f);
            }
        }
		Drag.SetInsetAndSizeFromParentEdge(Anchor, 0f, endValue);
	}

	IEnumerator DamageFlash()
	{
		Flash.color = Color.white;
		yield return new WaitForSeconds(0.03f);
		Flash.color = Color.Lerp(Color.white, baseColor, 0.33f);

		yield return new WaitForSeconds(0.03f);
		Flash.color = Color.Lerp(Color.white, baseColor, 0.77f);

		yield return new WaitForSeconds(0.03f);
		Flash.color = baseColor;
	}
}
