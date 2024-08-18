using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//地图生成
public class Map : MonoBehaviour
{
    private static Canvas mCanvas;
    public static int ImgSize=160;
    public static int Width=100;//160
    public static int Height=70;//100
    private static int BorderSize=6;
    private static List<GameObject> mBorderPrefabs=new List<GameObject>();
    private static List<GameObject> mGrassPrefabs=new List<GameObject>();
    private static List<int> mBorderChance=new List<int>{20,40,60,80,100};
    private static List<int> mGrassChance=new List<int>{70,72,74,76,78,80,100};

    public static void Create()
    {
        mCanvas=GameObject.Find("Canvas").GetComponent<Canvas>();
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/0"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/1"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/2"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/3"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/4"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/5"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/6"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/7"));
        mGrassPrefabs.Add(Resources.Load<GameObject>("Other/Map/8"));
        mBorderPrefabs.Add(Resources.Load<GameObject>("Other/Map/Border0"));
        mBorderPrefabs.Add(Resources.Load<GameObject>("Other/Map/Border1"));
        mBorderPrefabs.Add(Resources.Load<GameObject>("Other/Map/Border2"));
        mBorderPrefabs.Add(Resources.Load<GameObject>("Other/Map/Border3"));
        mBorderPrefabs.Add(Resources.Load<GameObject>("Other/Map/Border4"));
        int wHalf=(Width+BorderSize*2)/2;
        int hHalf=(Height+BorderSize*2)/2;
        for(int i=-hHalf;i<hHalf;i++)
        {
            for(int j=-wHalf;j<wHalf;j++)
            { 
                GameObject prefab;
                if(i<-Height/2||j<-Width/2||i>=Height/2||j>=Width/2)
                {
                    prefab=CreateBlock(true);
                    Instantiate(prefab,new Vector3(j*ImgSize,i*ImgSize,0),quaternion.identity,mCanvas.transform);
                }
                else
                {
                    prefab=CreateBlock(false);
                    Instantiate(prefab,new Vector3(j*ImgSize,i*ImgSize,0),quaternion.identity,mCanvas.transform);
                    
                }
            }
        }
        return;
    }

    private static GameObject CreateBlock(bool isBorder)
    {
        List<GameObject> prefabs = new List<GameObject>();
        List<int> chance=new List<int>();
        prefabs=isBorder?mBorderPrefabs:mGrassPrefabs;
        chance=isBorder?mBorderChance:mGrassChance;
        int randomInt=RandomHelper.RandomInt(0,100);
        for(int i=0;i<chance.Count;i++)
        {
            if(randomInt<chance[i])
            {   
                return prefabs[i];
            }
        }
        return null;
    }

}