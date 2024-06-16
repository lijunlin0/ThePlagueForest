using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class CollisionHelper
{
    public static bool IsColliding(PolygonCollider2D collider1,PolygonCollider2D collider2)
    {
        return collider1.Distance(collider2).isOverlapped;
    }

    public static void Collide()
    {
        FightModel fightModel=FightModel.GetCurrent();
        PlayerWithEnemyCollide(fightModel.GetEnemies());
        PlayerBulletHitEnemy(fightModel.GetPlayerBullets(),fightModel.GetEnemies());
        EnemyBulletHitPlayer(fightModel.GetEnemyBullets());

    }
    //玩家与敌人碰撞
    private static void PlayerWithEnemyCollide(List<Enemy> enemies)
    {
        Player player= Player.GetCurrent();
        if(player.InCollideProtect())
        {
            return;  
        }
        PolygonCollider2D collider1=player.GetCollider();
        foreach(Enemy enemy in enemies)
        {
            PolygonCollider2D collider2=enemy.GetCollider();
            if(IsColliding(collider1,collider2))
            {
                
                int points=PropertySheet.CollideDamage;
                DamageInfo damageInfo=new DamageInfo(enemy,player,points,null,null);
                FightSystem.Damage(damageInfo);
                player.SetCollideProtect();
                break;
            }
        }
    }

    //玩家子弹命中敌人
    private static void PlayerBulletHitEnemy(List<Bullet> bullets,List<Enemy> enemies)
    {
        foreach(Bullet bullet in bullets)
        {
            PolygonCollider2D collider1=bullet.GetCollider();
            foreach(Character enemy in enemies)
            {
                PolygonCollider2D collider2=enemy.GetCollider();
                if(IsColliding(collider1,collider2))
                {
                    bullet.OnCollideCharacter(enemy);
                }
            }
        }
    }

    //敌人子弹命中玩家
    private static void EnemyBulletHitPlayer(List<Bullet> bullets)
    {
        Player player=Player.GetCurrent();
        PolygonCollider2D collider1=player.GetCollider();
        foreach(Bullet bullet in bullets)
        {
            PolygonCollider2D collider2=bullet.GetCollider();
            if(IsColliding(collider1,collider2)) 
            {
                bullet.OnCollideCharacter(player);
            }
        }
    }
}