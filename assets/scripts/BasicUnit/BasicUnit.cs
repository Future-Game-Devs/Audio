using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public class BasicUnit : MonoBehaviour
{
    protected bool alive = true;
    public GameObject deadbody;
    public BasicunitSpobject baseStats;
    
    public Animator animator;
    public NavMeshAgent navAgent;
    

    public Transform aggrotarget;
    protected Vector3 distance;
   
    protected bool targetFound = false;
    public GameObject statdisplay;

    public Image healthbar,staminabar;
  
    public float currenthealth, currentarmor;
   
    protected float lastAggroResetTime;
   
    protected float attackcooldown = 0;

    protected BasicUnit aggrounit;

    //protected int layerMask = 1 << 10;

    public LayerMask layerMask;

    protected void Checkforaggro()
    {
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, baseStats.aggrorange); //baseStats.aggrorange=10);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider targetCollider in potentialTargets)
        {
            if (targetCollider.gameObject.layer == 6)  // Check for enemy unit layer
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetCollider.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = targetCollider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            aggrotarget = closestTarget;

            aggrounit = aggrotarget.gameObject.GetComponent<BasicUnit>();
            targetFound = true;
        }
        else { animator.SetInteger("main", 2); }
    }

    protected void Movetoaggrotarget(Transform target)
    {
        if (aggrotarget != null)
        {
            distance = target.position - transform.position;
         
            navAgent.stoppingDistance = (baseStats.attackrange-0.5f);
            navAgent.SetDestination(target.position);
        }

        
            
           
      
       
       

    }
    protected void HandleHealth()
    {
        healthbar.fillAmount = currenthealth / baseStats.health;
        staminabar.fillAmount = currentarmor / baseStats.armor;
        if (currenthealth <= -1 && currentarmor <= -1)
        {
            Die();
        }
    }
    protected void Die()
    {


        alive = false;
        navAgent.enabled = false;
        animator.SetBool("death", true);
        statdisplay.SetActive(false);
        if (deadbody != null)
        {
            Instantiate(deadbody, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
        Invoke("destroy", 12);
        
    }

    protected void destroy()
    {
        Destroy(gameObject);

    }
    public void TakeDemage(float damage)
    {
        currenthealth -= (damage - baseStats.armor);
       
        Checkforboostraggro();
    }
    public void Attack()
    {

        if ( distance.x <= baseStats.attackrange + 1)
        {

            aggrounit.TakeDemage(baseStats.attack);
           

        }
    }
    protected void Checkforboostraggro()
    {
        Collider[] potentialTargets = Physics.OverlapSphere(transform.position, baseStats.boostaggrorange); //baseStats.aggrorange=90);

        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Collider targetCollider in potentialTargets)
        {
            if (targetCollider.gameObject.layer == 6)  // Check for enemy unit layer
            {
                float distanceToTarget = Vector2.Distance(transform.position, targetCollider.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    closestTarget = targetCollider.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            aggrotarget = closestTarget;
            aggrounit = aggrotarget.gameObject.GetComponent<BasicUnit>();
            targetFound = true;
        }
    }
    protected void Rotate(Transform target)
    {
        Vector3 targetDirectionR = target.position - transform.position;
        if (targetDirectionR != Vector3.zero)
        {
            targetDirectionR.Normalize();

            float currentX = transform.rotation.eulerAngles.x;

            float currentZ = 0;
            float newYAngle = Mathf.Atan2(targetDirectionR.x, targetDirectionR.z) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.Euler(currentX, newYAngle, currentZ);


            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, baseStats.Rotationspeed * Time.deltaTime);

        }

    }
}

