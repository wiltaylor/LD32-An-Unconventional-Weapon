﻿using UnityEngine;
using System.Collections;

public class LeaverController : MonoBehaviour
{

    public GameObject[] TargetObjects;
    public bool Targeted;
    public Sprite SelectedImage;
    public Sprite NormalImage;

    private SpriteRenderer _spriteRenderer;
    private GameController _gameController;

    private bool _switched = false;
    private AudioSource _audio;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = GameController.Instance;
        _audio = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!Targeted)
            return;

        if (coll.transform.tag.Contains("Unit"))
        {
            var unit = coll.transform.gameObject.GetComponent<UnitController>();
            if (unit.PlayerOwned)
            {
                foreach (var o in TargetObjects)
                {
                    o.SendMessage("SwitchTriggered");
                }

                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                _switched = true;
                _spriteRenderer.sprite = NormalImage;
                _audio.Play();

            }
        }
    }

    void OnMouseDown()
    {
        if (_switched)
            return;

        if (_gameController.UILockedOut)
            return;

        if (!Targeted)
        {
            Targeted = true;
            _spriteRenderer.sprite = SelectedImage;
        }
        else
        {
            Targeted = false;
            _spriteRenderer.sprite = NormalImage;
        }
    }


}
