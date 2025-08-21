using UnityEngine;
using Game.Core;
public class DevTriggerResults : MonoBehaviour
{
    void Update() { if (Input.GetKeyDown(KeyCode.R)) EventBus.BossDefeated(); }
}