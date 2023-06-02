using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (oyuncu)
    public float laneWidth = 2.7f; // Þerit geniþliði

    private Vector3 offset; // Hedef ile kamera arasýndaki konum farký

    private void Start()
    {
        if (target != null)
        {
            // Baþlangýçta offset deðerini ayarla
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Hedefin pozisyonunu takip eden kamera pozisyonunu güncelle
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
    }

    public void UpdateOffset(int laneIndex)
    {
        // Þerit deðiþtirildiðinde offset deðerini güncelle
        float xOffset = laneIndex * laneWidth;
        offset.x = xOffset;
    }
}