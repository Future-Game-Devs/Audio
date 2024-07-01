using UnityEngine;


public class PropEnemy : BasicUnit
{
    public Transform headBone;

    private float headLookX;
    private float headLookY;
    int headblendXhash, headblendYhash;
    private float velocityX = 0.0f;
    private float velocityY = 0.0f;
    bool targetout= false;
    float targetOutTimer = 0f;
    float raycastDistance = 90f;
    float weightdamp;
    Vector3 targetDirection;
    void Start()
    {
        currenthealth = baseStats.health;
        currentarmor = baseStats.armor;
        navAgent.speed = baseStats.speed;
        headblendXhash = Animator.StringToHash("headblendX");
        headblendYhash = Animator.StringToHash("headblendY");
    }

    private void FixedUpdate()
    {

    }
    void Update()
    {
        if (!alive)
        { return; }
        Debug.Log(weightdamp);
        Vector3 forward=headBone.forward;
        Ray forwardray = new Ray(headBone.position, forward);
        RaycastHit forwardhit;

        if (Physics.Raycast(forwardray, out forwardhit, raycastDistance, layerMask))
        {

            if (forwardhit.collider.gameObject.layer == 6)
            {
                aggrotarget = forwardhit.transform;
                targetFound = true;
                targetout = false;
                
              animator.SetLayerWeight(1, 1);
            }
            

        }
        else
        {
            targetout = true;
        }

        for (int i = -1; i <= 2; i += 1)
        {
            Vector3 horizontalDirection = headBone.forward + (headBone.right * i * 40f * Mathf.Deg2Rad);


            for (int j = -1; j <= 2; j += 1)
            {
                Vector3 direction = horizontalDirection + (headBone.up * j * 30f * Mathf.Deg2Rad);
                Ray ray = new Ray(headBone.position, direction);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
                {
                    if (hit.collider.gameObject.layer == 6)
                    {
                        aggrotarget = hit.transform;
                        if (weightdamp <= 1f)
                            weightdamp += 0.1f;
                            targetFound = true;
                    targetout = false;
                        animator.SetLayerWeight(1, weightdamp);
                        break;

                    }
                    
                }
                else
                {
                    targetout = true;
                }

                Debug.DrawRay(ray.origin, ray.direction, Color.green, 0.1f);
            }
        }
      
        if (targetout)
        {
            targetOutTimer += Time.deltaTime; 

            if (targetOutTimer >= 5f) 
            {
                targetFound = false; 
                targetOutTimer = 0f; 
            }
        }
        else
        {
            targetOutTimer = 0;
        }
        if (!targetFound)
        {

            animator.SetInteger("main", 0);
            if(weightdamp >= 0f)
            {
              weightdamp -= 0.001f;
            }
          
            animator.SetLayerWeight(1, weightdamp);
            aggrounit = null;
            navAgent.ResetPath();
         
           /* headLookX = Mathf.SmoothDamp(headLookX, 0f, ref velocityX, 0.1f);
            headLookY = Mathf.SmoothDamp(headLookY, 0f, ref velocityY, 0.1f);
            animator.SetFloat(headblendXhash, headLookX);
            animator.SetFloat(headblendYhash, headLookY);*/
            return;
        }


        targetDirection = transform.InverseTransformDirection(aggrotarget.position - headBone.position);

        float FheadLookX = Mathf.Clamp(targetDirection.normalized.x, -1f, 1f);
        float FheadLookY = Mathf.Clamp(targetDirection.normalized.y, -1f, 1f);


        headLookX = Mathf.SmoothDamp(headLookX, FheadLookX, ref velocityX, 0.1f);
        headLookY = Mathf.SmoothDamp(headLookY, FheadLookY, ref velocityY, 0.1f);

        animator.SetFloat(headblendXhash, headLookX);
        animator.SetFloat(headblendYhash, headLookY);


       

            Rotate(aggrotarget.transform);
        
        Movetoaggrotarget(aggrotarget);
      
        if (Mathf.Abs(distance.magnitude) <= baseStats.attackrange)
        {
            aggrounit = aggrotarget.gameObject.GetComponent<BasicUnit>();
            animator.SetInteger("main", 2);
          
        }
        if (navAgent.velocity.magnitude >=1f)
        {
            animator.SetInteger("main", 1);
            navAgent.speed = baseStats.speed;
        }
        if (Mathf.Abs(distance.y) >= 3)
        {
            animator.SetInteger("main", 0);
        }



    }
    private void LateUpdate()
    {
        HandleHealth();
    }
    
}



