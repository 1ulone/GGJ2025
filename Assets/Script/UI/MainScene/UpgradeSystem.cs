using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    // Inisialisasi
    [SerializeField] GameObject rootPanel;
    private CanvasGroup CGPanel;
    private bool isTransitioning = false;
    private const int SPEEDTRANSITION = 5;
    public static UpgradeSystem Instance { get; private set; }

    private GameObject Addantional;

    private Coroutine activeCoroutine;
    public TextMeshProUGUI description;

    private void Awake()
    {
        CGPanel = rootPanel.GetComponent<CanvasGroup>();
        Addantional = rootPanel.transform.Find("Addantional").gameObject;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (CGPanel == null)
        {
            Debug.LogError("CGPanel is not assigned in the Inspector!");
        }
    }

    // Fungsi untuk menampilkan panel
    public void ShowUpgradeMenu()
    {
        if (!isTransitioning)
        {
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(TransitionAktif());
        }
        else
        {
            Debug.Log("is Transitioning");
        }
    }

    //fungsi untuk menampilkan button konfirmasi
    private void ShowConfirm()
    {
        Addantional.SetActive(true);
    }

    public void HideConfirm()
    {
        Addantional.SetActive(false);
    }

    // Fungsi untuk menyembunyikan panel
    public void HideUpgradeMenu()
    {
        if (!isTransitioning)
        {
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(TransitionNonAktif());
        }
        else
        {
            Debug.Log("is Transitioning");
        }
    }

    // Fungsi transisi aktif
    private IEnumerator TransitionAktif()
    {
        rootPanel.SetActive(true);
        isTransitioning = true;
        while (CGPanel.alpha < 0.99f)
        {
            CGPanel.alpha = Mathf.Lerp(CGPanel.alpha, 1, Time.deltaTime * SPEEDTRANSITION);
            yield return null;
        }

        CGPanel.alpha = 1;
        isTransitioning = false;
    }

    // Fungsi transisi nonaktif
    private IEnumerator TransitionNonAktif()
    {
        isTransitioning = true;
        while (CGPanel.alpha > 0.01f)
        {
            CGPanel.alpha = Mathf.Lerp(CGPanel.alpha, 0, Time.deltaTime * SPEEDTRANSITION);
            yield return null;
        }

        CGPanel.alpha = 0;
        isTransitioning = false;
        rootPanel.SetActive(false);
    }

    //fungsi ketika item di click
    public void SelectedItem1()
    {
        description.text = "Item 1 selected";
        ShowConfirm();
    }

    public void SelectedItem2()
    {
        description.text = "Item 2 Selected";
        ShowConfirm();
    }
}
