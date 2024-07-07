using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectArea:MonoBehaviour
{
    private PolygonCollider2D mCollider;
    private Callback<Character> mCollideCallback;

    
    public static EffectArea Create(string prefabName,Callback<Character> collideCallback)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaArea=Instantiate(effectAreaPrefab);
        effectAreaArea.transform.SetParent(Player.GetCurrent().transform);
        EffectArea effectArea=effectAreaArea.AddComponent<EffectArea>();
        effectArea.Init(collideCallback);
        return effectArea; 
    }

    private void Init(Callback<Character> collideCallback)
    {
        mCollideCallback=collideCallback;
        mCollider=GetComponent<PolygonCollider2D>();
    }

    public void Collide()
    {
        List<Enemy> enemies=FightModel.GetCurrent().GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            PolygonCollider2D collider2=enemy.GetCollider();
            if(CollisionHelper.IsColliding(mCollider,collider2))
            {
                mCollideCallback(enemy);
            }
        }
    }
}