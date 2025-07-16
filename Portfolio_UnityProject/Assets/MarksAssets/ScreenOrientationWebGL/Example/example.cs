using MarksAssets.ScreenOrientationWebGL;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScreenOrientation = MarksAssets.ScreenOrientationWebGL.ScreenOrientationWebGL.ScreenOrientation;

public class example : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;

	private bool rotated = false;

	public void setText(int orient) {
		ScreenOrientation orientation = (ScreenOrientation)orient;
		
		//the 'if' is obviously unnecessary. I'm just testing if the comparisons are working as expected. It's an example after all, might as well be thorough.
		if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown || orientation == ScreenOrientation.LandscapeLeft || orientation == ScreenOrientation.LandscapeRight)
			text.text = orientation.ToString();
	}

	public void rotatePortraitToLandscape(int orient)
	{
        ScreenOrientation orientation = (ScreenOrientation)orient;

        if (orientation == ScreenOrientation.Portrait)
		{
			rotated = true;
			gameObject.transform.Rotate(0, 0, 90);
		}
		else
		{
			if(!rotated)
			{
				return;
			}

			rotated = false;
            gameObject.transform.Rotate(0, 0, -90);
        }
    }
}
