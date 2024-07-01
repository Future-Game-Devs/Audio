using UnityEngine;


[CreateAssetMenu(fileName = "New Unit", menuName = "New Unit/Basic")]
public class BasicunitSpobject :ScriptableObject
{
    public enum unitType
    {
       MainPlayer,
       Npc,
       BasicEnemy,
       PropEnemy
    }
    [Space(15)]
    [Header("Unit Settings")]
    public unitType type;
    public new string name;
    public GameObject SamplePrefab;
   

    //[Space(15)]


    public float unitsize, Rotationspeed, speed, aggrorange, boostaggrorange, attackrange, attackspeed, health, armor, stamina, attack;

}
