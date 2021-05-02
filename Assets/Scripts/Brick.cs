using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitPoint = 1;
    public ParticleSystem destroyEffectPS;
    private SpriteRenderer brickSpriteRenderer;

    public static event Action<Brick> OnBrickDestruction;

    private void Awake()
    {
        this.brickSpriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        CollisionLogic(ball);
    }

    private void CollisionLogic(Ball ball)
    {
        this.hitPoint--;
        if (this.hitPoint <= 0)
        {
            BrickManager.Instance.remainingBricks.Remove(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
            OnBrickDestruction?.Invoke(this);
        }
        else
        {
            this.brickSpriteRenderer.sprite = BrickManager.Instance.Sprites[this.hitPoint - 1];
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brokenBrickPos = gameObject.transform.position;
        Vector3 deathEffectSpawnPos = new Vector3(brokenBrickPos.x, brokenBrickPos.y, brokenBrickPos.x-0.2f);
        GameObject destroyEffect = Instantiate(destroyEffectPS.gameObject, deathEffectSpawnPos, Quaternion.identity);

        MainModule mm = destroyEffect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.brickSpriteRenderer.color;
        Destroy(destroyEffect, destroyEffectPS.main.startLifetime.constant);
    }

    public void Init(Transform containerTransform, Sprite sprite, Color brickColor, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        this.brickSpriteRenderer.sprite = sprite;
        this.brickSpriteRenderer.color = brickColor;
        this.hitPoint = hitpoints;
    }
}
