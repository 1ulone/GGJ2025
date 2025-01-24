using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{

    public static HPSystem Instance { get; private set; }

    [SerializeField] private Image maskHP;
    //setting maxhp disini mas
    public int maximumHP;
    private float currentHP;

    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        
        currentHP = maximumHP;
        UpdateFillAmount();
    }



    //update gambar aja
    private void UpdateFillAmount()
    {
        float fillAmount = currentHP / maximumHP;
        maskHP.fillAmount = Mathf.Clamp01(fillAmount); 
    }


    //Update nilai currentHP
    public void UpdateHealth(float currentHealth)
    {
        currentHP = Mathf.Clamp(currentHealth, 0, maximumHP); 
        UpdateFillAmount();
    }

}
