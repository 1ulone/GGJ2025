using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
	[SerializeField] private Button stage1;
	[SerializeField] private Button stage2;
	[SerializeField] private Button stage3;
	[SerializeField] private CanvasGroup tutorial;
    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
		tutorial.alpha = 0;
    }

    private void OnEnable()
	{
		stage1.enabled = !GameManager.stage1;
		stage2.enabled = !GameManager.stage2;
		stage3.enabled = !GameManager.stage3;

		if (!stage1.enabled && !stage2.enabled && !stage3.enabled)
			SceneManager.LoadScene(5);
	}

    public void stageOnebutton()
    {
        audioSource.PlayOneShot(impact, 2F);
        StartCoroutine(LoadTutorial(2));
    }

    public void stageTwobutton()
    {
        audioSource.PlayOneShot(impact, 2F);
        StartCoroutine(LoadTutorial(3));
    }

    public void stageThreebutton()
    {
        audioSource.PlayOneShot(impact, 2F);
        StartCoroutine(LoadTutorial(4));
    }

	private IEnumerator LoadTutorial(int i)
	{
		tutorial.alpha = 1;
		yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.E));
		SceneManager.LoadScene(i);
	}
}
