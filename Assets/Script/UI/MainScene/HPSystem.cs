using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{

    public static HPSystem Instance { get; private set; }

    [SerializeField] private Image maskHP;
    [SerializeField] private Image maskHP_E;
    //setting maxhp disini mas
    public int maximumHP;
    public float currentHP;

    public int maximumHP_E;
	public float currentHP_E;

    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
//        DontDestroyOnLoad(gameObject); 
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

	public void UpdateMaxhealth(int maxhealth)
		=> maximumHP = maxhealth;

    //update gambar aja
    private void UpdateFillAmountE()
    {
        float fillAmount = currentHP_E / maximumHP_E;
        maskHP_E.fillAmount = Mathf.Clamp01(fillAmount); 
    }


    //Update nilai currentHP
    public void UpdateHealthE(float currentHealth)
    {
        currentHP_E = Mathf.Clamp(currentHealth, 0, maximumHP_E); 
        UpdateFillAmountE();
    }


}
