using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton shootButton = XboxButton.RightBumper;
    [SerializeField] float moveSpeedNormal = 3;
    [SerializeField] float moveSpeedWithBomb = 2;
    [SerializeField] float shootForce = 2;
    [SerializeField] float holdDistance = 0.8f;
    [SerializeField] float throwWaitTime = 0.5f;
    [SerializeField] GameObject turret;

    [HideInInspector]
    public Color color;

    [HideInInspector]
    public int deaths;

    float moveSpeed;
    float throwTimer;

    GameObject bomberang;

    CharacterController cc;

    Vector3 bodyRotation;
    Vector3 turretRotation;

    bool isAlive;

    Material material;

    void Awake()
    {
        deaths = 0;
        cc = GetComponent<CharacterController>();
        material = GetComponent<MeshRenderer>().material;
        color = material.color;

        ResetForNewRound();
    }

    public void ResetForNewRound()
    {
        bomberang = null;
        bodyRotation = new Vector3 { x = 1 };
        turretRotation = new Vector3 { x = 1 };
        moveSpeed = moveSpeedNormal;
        throwTimer = 0;
        isAlive = true;
    }

    void Update ()
    {
        Movement();

        if (bomberang != null)
        {
            moveSpeed = moveSpeedWithBomb;

            bomberang.transform.position = transform.position + turretRotation * holdDistance;


            if (throwTimer <= 0 && XCI.GetButtonDown(shootButton, controllerNumber))
            {
                // Shoot the boomberang!
                bomberang.GetComponent<Bomberang>().Shoot(turretRotation.normalized * shootForce);
                bomberang = null;
            }
            else
            {
                throwTimer -= Time.deltaTime;
            }
        }
        else
        {
            moveSpeed = moveSpeedNormal;
        }
    }

    public void BlownUp()
    {
        isAlive = false;
    }

    public void Hit(GameObject a_bomberang)
    {
        bomberang = a_bomberang;
        throwTimer = throwWaitTime;
    }

    private void Movement()
    {
        // Left stick movement
        Vector3 newPosition = transform.position;
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, controllerNumber);
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, controllerNumber);
        cc.Move(new Vector3(axisX * moveSpeed * Time.deltaTime, 0, axisY * moveSpeed * Time.deltaTime));

        if (axisX != 0 || axisY != 0)
        {
            Vector3 targetRotation = new Vector3 { x = axisX, z = axisY };
            bodyRotation = Vector3.Lerp(bodyRotation, targetRotation, 10 * Time.deltaTime);
            transform.LookAt(transform.position + bodyRotation);
        }

        // Right stick
        axisX = XCI.GetAxis(XboxAxis.RightStickX, controllerNumber);
        axisY = XCI.GetAxis(XboxAxis.RightStickY, controllerNumber);

        if (axisX != 0 || axisY != 0)
        {
            Vector3 targetRotation = new Vector3 { x = axisX, z = axisY };
            turretRotation = Vector3.Lerp(turretRotation, targetRotation, 10 * Time.deltaTime);
        }
        turret.transform.LookAt(transform.position + turretRotation);
    }

}
