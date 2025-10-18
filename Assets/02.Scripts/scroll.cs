using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public float scrollSpeedX = 2f;
    private Renderer quadRenderer;

    public bool iscloud;
    public float cloudScrollSpeedX;
    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iscloud)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - cloudScrollSpeedX * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
            if (gameObject.transform.position.x <= -11f)
            {
                gameObject.transform.position = new Vector3(11f, Random.Range(-1f, 4f), 0f);
            }
        }
        else
        {
            float offsetX = Time.time * scrollSpeedX;
            quadRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }
        

    }
}
