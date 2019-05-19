using UnityEngine;

public class Fading : MonoBehaviour
{
    void SetFading()
	{
		GameManager.fading = false;
		gameObject.SetActive(false);
	}
}
