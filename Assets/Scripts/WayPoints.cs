using UnityEngine;

public class WayPoints : MonoBehaviour
{
    //Fixa så att path uppdateras med ny map! !TODO!
    public int path;
    public static int pathsOnMap;
    public static Transform[] path1;
    public static Transform[] path2;
    public static Transform[] path3;
    public static Transform[] path4;
    public static Transform[] path5;
    public static Transform[] path6;

    public static WayPoints instance;

    private void Awake()
    {
    }

    public void Start()
    {

        pathsOnMap = 0;
        instance = this;
        switch (path)
        {
            case 1:
                path1 = new Transform[transform.childCount];
                for (int i = 0; i < path1.Length; i++)
                {
                    path1[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
            case 2:
                path2 = new Transform[transform.childCount];
                for (int i = 0; i < path2.Length; i++)
                {
                    path2[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
            case 3:
                path3 = new Transform[transform.childCount];
                for (int i = 0; i < path3.Length; i++)
                {
                    path3[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
            case 4:
                path4 = new Transform[transform.childCount];
                for (int i = 0; i < path4.Length; i++)
                {
                    path4[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
            case 5:
                path5 = new Transform[transform.childCount];
                for (int i = 0; i < path5.Length; i++)
                {
                    path5[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
            case 6:
                path6 = new Transform[transform.childCount];
                for (int i = 0; i < path6.Length; i++)
                {
                    path6[i] = transform.GetChild(i);
                }
                pathsOnMap++;
                break;
        }
    }
}
