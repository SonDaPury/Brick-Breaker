using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBall : MonoBehaviour
{
    private ExtraBallManager extraBallManager;

    private void Awake()
    {
        extraBallManager = FindAnyObjectByType<ExtraBallManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("ExtraBall"))
        {
            AudioManager.Instance.PlaySoundEffect(1);
            extraBallManager.numberOfExtraBalls++;
            gameObject.SetActive(false);
        }
    }
}
