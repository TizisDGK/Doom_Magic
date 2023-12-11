using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ProceduralQuard : MonoBehaviour
{
    //ExecuteAlways --  означает что методы класса будут исполняться даже в редакторе. Нам нужна чтоб анимация проирывалась в редакторе


    //Скрипт для процедурного кварда.
    [SerializeField] Material targetMaterial;
    [SerializeField] float width = 1;
    [SerializeField] float height = 1;

    [SerializeField] Texture2D texture;

    private void OnDidApplyAnimationProperties() => Refresh();

#if UNITY_EDITOR
    private void OnValidate() //вызывается каждый раз когда меняется свойство объекта. Существует в редакторе но не в рантайме.Поэтому в решетки завернули
    {
        Refresh();
    }
#endif


    [ContextMenu("Refresh")]
    void Refresh()   //Обновляем меш
    {
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;

        if(!gameObject.TryGetComponent(out meshFilter)) //если не сомгли найти компонент мешФильтр
        {
            //тогда мы его добавляем
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        if (!gameObject.TryGetComponent(out meshRenderer)) //если не сомгли найти компонент мешРендерер
        {
            //тогда мы его добавляем
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4] //формируем прямоугольник
        {
            new Vector3(-width/2, 0, 0),
            new Vector3(width/2, 0, 0),
            new Vector3(-width/2, height, 0),
            new Vector3(width/2, height, 0)
        };

        mesh.vertices = vertices;

        int[] triangles = new int[6] //формируем треугольник
        {
            2, 3, 1,
            0, 2, 1
        };

        mesh.triangles = triangles;

        //нормалей должно быть столько сколько у меша вершин
        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uvs = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),

        };


        mesh.uv = uvs;

        meshFilter.mesh = mesh; //указываем нашему фильтру наш меш

        //кижаем материал
        meshRenderer.material = targetMaterial;

        if(Application.isPlaying)
            meshRenderer.material.SetTexture("_MainTex", texture);
        else
            meshRenderer.sharedMaterial.SetTexture("_MainTex", texture);

    }
}
