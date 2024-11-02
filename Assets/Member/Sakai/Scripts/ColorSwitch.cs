using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Haptics;

// DualShock 4 ゲームパッドの Haptic 機能を実装するクラス
public class DualShock4GamepadHID : DualShockGamepad, IDualShockHaptics, IDualMotorRumble, IHaptics, IEventPreProcessor
{
}

// 色を切り替えるためのクラス
public class ColorSwitch : MonoBehaviour
{
    // 色の設定
    public Color whiteColor;  // 白色
    public Color blueColor;   // 青色
    public float colorSwitchInterval = 2f; // 色が切り替わる間隔（現在は未使用）

    // 色を変更する対象のスプライトリスト
    [SerializeField]
    private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();

    // オブジェクトのレンダラー
    private Renderer objectRenderer;

    // 現在の色状態を管理するフラグ
    private bool isWhite = true;

    // タイマー（現在は未使用）
    [SerializeField]
    private Timer timer;

    void Start()
    {
        // オブジェクトのレンダラーを取得
        objectRenderer = GetComponent<Renderer>();
    }

    // 色を切り替えるメソッド
    public void ColorChange()
    {
        isWhite = !isWhite; // 現在の色状態を反転
        objectRenderer.material.color = isWhite ? whiteColor : blueColor; // オブジェクトの色を変更
        changecolor(); // スプライトの色も変更
    }

    // スプライトの色とデュアルショックのライトバーの色を変更するメソッド
    public void changecolor()
    {
        var _color = Color.white; // デフォルトの色を白に設定
        string[] joystickNames = Input.GetJoystickNames(); // 接続されているジョイスティックの名前を取得

        // 現在の色状態に応じてライトバーの色を設定
        if (isWhite)
        {
            if (joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]))
            {
                DualShock4GamepadHID.current.SetLightBarColor(Color.white); // ライトバーを白に設定
            }
            _color = blueColor; // スプライトの色は青に設定
        }
        else
        {
            if (joystickNames.Length > 0 && !string.IsNullOrEmpty(joystickNames[0]))
            {
                DualShock4GamepadHID.current.SetLightBarColor(Color.blue); // ライトバーを青に設定
            }
            _color = whiteColor; // スプライトの色は白に設定
        }

        // スプライトリストの全てのスプライトに色を適用
        foreach (var sprite in spriteList)
        {
            sprite.color = _color; // スプライトの色を変更
        }
    }
}
