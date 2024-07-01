using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    
    public BasicunitSpobject basicNPC;
    public float myHealth, myArmor;
    private float currentHealth, currentArmor, currentSpeed, currentMeleeattackspeed, currentAttackRange, currentAggroRange, currentStamina;
    private bool hasAggro = false;
    private Transform aggroTarget;

    public GameObject canvasHP;
    public Image healthBar;
    public TextMeshProUGUI showHealth;
    public GameObject canvasStamina;
    public Image staminaBar;
    public TextMeshProUGUI showStamina;
    
    //private Player aggroUnit;
    
    private void OnEnable()
    {
        basicNPC.SamplePrefab = gameObject;
    }
    void Start()
    {
        ScriptableObject.CreateInstance<BasicunitSpobject>();
        myHealth = basicNPC.health;
        myArmor = basicNPC.armor;
        currentAggroRange = basicNPC.aggrorange;
        currentHealth = myHealth;
        currentArmor = myArmor;
    }
    void Update()
    {
        if (currentHealth == basicNPC.health)
		{
			//canvasHP.SetActive(false);
		}
		else
		{
			//Debug.Log("I'm not at max HP");
			canvasHP.SetActive(true);
			canvasHP.transform.LookAt(Camera.main.transform);
            healthBar.fillAmount = currentHealth / basicNPC.health;
		}
        if (currentStamina == basicNPC.stamina)
		{
			//canvasStamina.SetActive(false);
		}
		else
		{
			canvasStamina.SetActive(true);
			canvasStamina.transform.LookAt(Camera.main.transform);
            staminaBar.fillAmount = currentStamina / basicNPC.stamina;
		}
        Checkforaggro();
    }
    protected void Checkforaggro()
    {
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, currentAggroRange);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider targetCollider in potentialTargets)
        {
            if (targetCollider.gameObject.layer == 1) // == UnitHandler.Instance.pUnitLayer)
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetCollider.transform.position);
                if(distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = targetCollider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            aggroTarget = closestTarget;
            //aggroUnit = aggroTarget.gameObject.GetComponent<Player.PlayerUnit>();
            hasAggro = true;
        }
    }
}
