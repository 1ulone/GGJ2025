using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
	[SerializeField] private Button stage1;
	[SerializeField] private Button stage2;
	[SerializeField] private Button stage3;

	private void OnEnable()
	{
		stage1.enabled = !GameManager.stage1;
		stage2.enabled = !GameManager.stage2;
		stage3.enabled = !GameManager.stage3;
	}

    public void stageOnebutton()
    {
        SceneManager.LoadScene(2);
    }

    public void stageTwobutton()
    {
        SceneManager.LoadScene(3);
    }

    public void stageThreebutton()
    {
        SceneManager.LoadScene(4);
    }
}
