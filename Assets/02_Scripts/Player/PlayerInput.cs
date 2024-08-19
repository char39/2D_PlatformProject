using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public int Horizontal_1p { get { return left_1p + right_1p; } }
    public int Vertical_1p { get { return up_1p + down_1p; } }
    public int Horizontal_2p { get { return left_2p + right_2p; } }
    public int Vertical_2p { get { return up_2p + down_2p; } }
    private int up_1p;
    private int down_1p;
    private int left_1p;
    private int right_1p;
    private int up_2p;
    private int down_2p;
    private int left_2p;
    private int right_2p;

    void Update()
    {
        Input_1p();
        Input_2p();
    }

    private void Input_1p()     // 1P 플레이어의 입력
    {
        up_1p = Input.GetKey(KeyCode.W) ? 1 : 0;            // W 키를 누르면 1, 아니면 0
        down_1p = Input.GetKey(KeyCode.S) ? -1 : 0;         // S 키를 누르면 -1, 아니면 0
        left_1p = Input.GetKey(KeyCode.A) ? -1 : 0;         // A 키를 누르면 -1, 아니면 0
        right_1p = Input.GetKey(KeyCode.D) ? 1 : 0;         // D 키를 누르면 1, 아니면 0
    }
    private void Input_2p()     // 2P 플레이어의 입력
    {
        up_2p = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;          // ↑ 키를 누르면 1, 아니면 0
        down_2p = Input.GetKey(KeyCode.DownArrow) ? -1 : 0;     // ↓ 키를 누르면 -1, 아니면 0
        left_2p = Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;     // ← 키를 누르면 -1, 아니면 0
        right_2p = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;    // → 키를 누르면 1, 아니면 0
    }
}