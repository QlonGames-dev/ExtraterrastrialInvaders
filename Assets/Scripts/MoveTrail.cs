using UnityEngine;
using UnityStandardAssets.Copy._2D;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 270;

    private static int caseSwitch;

    void Start()
    {
        if (PlatformerCharacter2D.m_FacingRight == false)
        {
            caseSwitch = 1;
        }

        if (PlatformerCharacter2D.m_FacingRight == true)
        {
            caseSwitch = 2;
        }

    }

    void FixedUpdate () {

        switch (caseSwitch)
        {
            case 1:
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
                Destroy(gameObject, 0.3f);
                break;
            case 2:
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
                Destroy(gameObject, 0.3f);
                break;
        }
    }
}