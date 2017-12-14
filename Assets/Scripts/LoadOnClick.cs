using UnityEngine;

public class LoadOnClick : MonoBehaviour
{
    public void LoadScene(int level)
    {
		if (level == -1) {
			Application.Quit ();
		}
#if UNITY_ANDROID
        if (level == 2)
        {
            Application.LoadLevel(4);
        } 
        else
        {
            Application.LoadLevel(level);
        }
#else
        Application.LoadLevel(level);
#endif
    }
}