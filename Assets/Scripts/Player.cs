using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Rigidbody rb;

    public bool isGameStarted;
    public bool isLegOpen;
    public bool isLegFixOpen;
    public bool isLegOpenFinish;

    private float direction = 0f;
    private float startPosx, actualPosx, startPosy, actualPosy;
    private float posValue = 35f;
    public float playerSpeed = 4f;
    public float ikWeight = 0f;
    public float minX;
    public float maxX;

    public int diamondScore = 0;
    public int heelCount;
    public int childCount;

    public Animator anim;

    public BoxCollider verticalCollider;
    public BoxCollider horizontalCollider;

    public GameObject heelPrefab;

    public Transform leftFootIk;
    public Transform rightFootIk;

    public Transform lastLeftHeel;
    public Transform lastRightHeel;

    public Transform leftHeel;
    public Transform rightHeel;

    public Transform leftFootParent;
    public Transform rightFootParent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        verticalCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {

        if (Mathf.Abs(actualPosy - startPosy) > 100 && Mathf.Abs(actualPosy - startPosy) < 250)
            playerSpeed = 12 + (actualPosy - startPosy) / 60;

        if (isGameStarted)
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
            startPosx = Input.mousePosition.x;

        direction = (actualPosx - startPosx) / posValue;

        if (direction < -8f)
            direction = -8;

        else if (direction > 8f)
            direction = 8;

        if (Input.GetMouseButton(0))
        {

            isGameStarted = true;
            actualPosx = Input.mousePosition.x;

            if (direction < 0)
                transform.Translate(direction * 1.4f * Time.deltaTime, 0, 0);
            if (direction > 0)
                transform.Translate(direction * 1.4f * Time.deltaTime, 0, 0);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (isLegOpen)
        {
            float x = 0f;
            DOTween.To(() => ikWeight, x => ikWeight = x, 1, 3);
            transform.DOMoveY(transform.position.y - 0.6f, 1f);
            isLegOpen = false;
        }

        if (!isLegOpenFinish)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeight);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootIk.rotation);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootIk.position);

            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeight);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootIk.rotation);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootIk.position);
            verticalCollider.enabled = false;
            horizontalCollider.enabled = true;
        }
        if(isLegOpenFinish)
        {
            verticalCollider.enabled = true;
        }

        if (!isLegOpen)
        {
            verticalCollider.enabled = true;
            horizontalCollider.enabled = false;
        }

        if (isLegFixOpen)
        {
            float x = 1f;
            DOTween.To(() => ikWeight, x => ikWeight = x, 0, 0.5f);
            transform.DOMoveY(transform.position.y + 0.6f, 1f);
            isLegFixOpen = false;
        }
}
        public void OnTakeHeel()
    {
        transform.DOMoveY(transform.position.y + 0.8f, 0.5f);
        if (lastLeftHeel == null)
        {
            lastLeftHeel = Instantiate(heelPrefab, new Vector3(leftHeel.position.x, leftHeel.position.y, leftHeel.position.z), leftHeel.transform.rotation).transform;
        }
        else
        {
            Transform temp = lastLeftHeel;
            lastLeftHeel = Instantiate(heelPrefab, temp.GetChild(0).position, leftHeel.transform.rotation).transform;
        }
        lastLeftHeel.parent = leftFootParent;

        if (lastRightHeel == null)
        {
            lastRightHeel = Instantiate(heelPrefab, new Vector3(rightHeel.position.x, rightHeel.position.y, rightHeel.position.z), rightHeel.transform.rotation).transform;
        }
        else
        {
            Transform temp = lastRightHeel;
            lastRightHeel = Instantiate(heelPrefab, temp.GetChild(0).position, rightHeel.transform.rotation).transform;
        }
        heelCount++;
        lastRightHeel.parent = rightFootParent;
        verticalCollider.size = new Vector3(verticalCollider.size.x, verticalCollider.size.y + 0.6f, verticalCollider.size.z);
        verticalCollider.center = new Vector3(verticalCollider.center.x, verticalCollider.center.y - 0.55f, verticalCollider.center.z);
    }
    public void DestroyCharacter(int obstacleCount)
    {
        if (obstacleCount > heelCount)
        {
            Destroy(gameObject);
        }
        else
        {
            LowerHeight(obstacleCount);
            for (int i = 0; i < obstacleCount; i++)
            {
                childCount = heelCount + 1;
                Transform leftHeelChild = leftFootParent.GetChild(childCount);
                Destroy(leftHeelChild.gameObject);

                Transform rightHeelChild = rightFootParent.GetChild(childCount);
                Destroy(rightHeelChild.gameObject);

                heelCount--;
            }
        }
    }
    public void LowerHeight(int obstacleCount)
    {
        transform.DOMoveY(transform.position.y - (0.8f * obstacleCount), 1f);
    }

    public void OnTakeDiamond()
    {
        diamondScore++;
    }
}
