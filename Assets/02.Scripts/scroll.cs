using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float scrollSpeedX = 2f; // x축으로 스크롤 되는 속도
    private Renderer quadRenderer;

    public bool isCloud;  // 구름 오브젝트인지 아닌지 확인용  true면 구름 false면 Ground
    public float cloudScrollSpeedX;

    void Start()
    {
        quadRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isCloud)
        {
            // 이 스크립트가 붙어있는 게임 오브젝트를 현재 좌표에서 지정한 cloudScollSpeedX값만큼 계속 빼서 왼쪽으로 계속 움직여준다.
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - cloudScrollSpeedX * Time.deltaTime,
                                                         gameObject.transform.position.y,
                                                         gameObject.transform.position.z);

            if (gameObject.transform.position.x <= -11f) // 이 게임 오브젝트의 x 좌표가 -11보다 작거나 같으면, 
            {
                float randY = Random.Range(-1f, 4f);
                gameObject.transform.position = new Vector3(11f, randY, 0f);  // 이 게임 오브젝트는 x =11, 랜덤값 y가 1에서 4 사이, z = 0의 좌표로 이동됨
            }
        }
        else
        {
            // 시간이 지나감에 따라서 offset을 계산해준다
            float offsetX = Time.time * scrollSpeedX;

            //Material의 메인 텍스처의 오프셋을 조정
            quadRenderer.material.mainTextureOffset = new Vector2(offsetX, 0);
        }            
    }
}
