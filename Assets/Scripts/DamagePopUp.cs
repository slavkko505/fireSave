using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Create(Transform pfDamagePopUp,Vector3 positon, int damageAmount)
    {
        Transform damagePopUpTransform = Instantiate(pfDamagePopUp, positon, Quaternion.identity);

        damagePopUpTransform.LookAt(Camera.main.transform);
        
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount);

        return damagePopUp;
    }
    
    private TextMeshPro _textMeshPro;
    private float disapearTimer;
    private Color textColor;

    [SerializeField] private RectTransform _rect;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        _rect.Rotate(0,-180,0);
    }

    public void Setup(int damageAmount)
    {
        if (_textMeshPro != null)
        {
            _textMeshPro.text = damageAmount.ToString();
            textColor = _textMeshPro.color;
            disapearTimer = 0.5f; 
        }
    }

    private void Update()
    {
        float speed = 0.5f;

        transform.position += new Vector3(speed, speed,0) * Time.deltaTime;

        disapearTimer -= Time.deltaTime;
        if (disapearTimer < 0)
        {
            float disapearSpeed = 3f;
            
            textColor.a -= disapearSpeed * Time.deltaTime;
            _textMeshPro.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
