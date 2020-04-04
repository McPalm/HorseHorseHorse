using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput : MonoBehaviour
{

    static public Dictionary<string, KeyCode> Bindings;

    public InputToken Token { get; private set; }

    float horizontal = 0f;
    float vertical = 0f;

    float horizontal2 = 0f;
    float vertical2 = 0f;

    private void Start()
    {
        Token = new InputToken();
        foreach (var item in GetComponents<IControllable>())
        {
            item.InputToken = Token;
        }
        if(Bindings == null)
        {
            LoadBindings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessVectors();
        
        Token.SetDirection(new Vector2(horizontal, vertical));
        Token.SetSecondDirection(new Vector2(horizontal2, vertical2));

        if (GetButtonDown("Jump"))
            Token.PressJump();
        if (GetButtonDown("Attack"))
            Token.PressAttack();
        if (GetButtonDown("Special"))
            Token.PressSpecial();
        Token.HoldBlock = GetButton("Block");
    }


    private void ProcessVectors()
    {
        horizontal = 0f;
        if (Input.GetKey(Bindings["Right"]))
            horizontal++;
        if (Input.GetKey(Bindings["Left"]))
            horizontal--;
        if (horizontal == 0f)
            horizontal = Input.GetAxis("Horizontal");
        
        vertical = 0;
        if (Input.GetKey(Bindings["Up"]))
            vertical++;
        if (Input.GetKey(Bindings["Down"]))
            vertical--;
        if (vertical == 0f)
            vertical = Input.GetAxis("Vertical");

        horizontal2 = 0f;
        if (Input.GetKey(Bindings["Right2"]))
            horizontal2++;
        if (Input.GetKey(Bindings["Left2"]))
            horizontal2--;
        if (horizontal2 == 0f)
            horizontal2 = Input.GetAxis("Horizontal2");

        vertical2 = 0f;
        if (Input.GetKey(Bindings["Up2"]))
            vertical2++;
        if (Input.GetKey(Bindings["Down2"]))
            vertical2--;
        if (vertical2 == 0f)
            vertical = Input.GetAxis("Vertical2");
    }

    bool GetButtonDown(string button)
    {
        if (Bindings.ContainsKey(button))
            return Input.GetKeyDown(Bindings[button]);
        Debug.LogWarning($"{button} is not defined in Input.");
        return false;
    }

    bool GetButton(string button)
    {
        if (Bindings.ContainsKey(button))
            return Input.GetKey(Bindings[button]);
        Debug.LogWarning($"{button} is not defined in Input.");
        return false;
    }

    static public void LoadBindings()
    {
        Bindings = new Dictionary<string, KeyCode>();

        LoadBind("Jump", KeyCode.Space);
        LoadBind("Attack", KeyCode.Mouse0);
        LoadBind("Block", KeyCode.Mouse1);
        LoadBind("Special", KeyCode.Mouse2);

        LoadBind("Up", KeyCode.W);
        LoadBind("Left", KeyCode.A);
        LoadBind("Down", KeyCode.S);
        LoadBind("Right", KeyCode.D);

        LoadBind("Up2", KeyCode.I);
        LoadBind("Left2", KeyCode.J);
        LoadBind("Down2", KeyCode.K);
        LoadBind("Right2", KeyCode.L);
    }

    static void LoadBind(string button, KeyCode _default)
    {
        var key = PlayerPrefs.GetString("Key" + button);
        if (String.IsNullOrEmpty(key))
            Bindings[button] = _default;
        else
            Bindings[button] = (KeyCode)Enum.Parse(typeof(KeyCode), key);
    }
}
