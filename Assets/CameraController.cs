using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (oyuncu)
    public float laneWidth = 2.7f; // �erit geni�li�i

    private Vector3 offset; // Hedef ile kamera aras�ndaki konum fark�

    private void Start()
    {
        if (target != null)
        {
            // Ba�lang��ta offset de�erini ayarla
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Hedefin pozisyonunu takip eden kamera pozisyonunu g�ncelle
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
        }
    }

    public void UpdateOffset(int laneIndex)
    {
        // �erit de�i�tirildi�inde offset de�erini g�ncelle
        float xOffset = laneIndex * laneWidth;
        offset.x = xOffset;
    }
}