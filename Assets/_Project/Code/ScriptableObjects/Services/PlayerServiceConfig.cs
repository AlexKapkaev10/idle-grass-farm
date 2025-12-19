using Project.Game;
using UnityEngine;

namespace Project.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerServiceConfig), menuName = "Config/Service/Player")]
    public class PlayerServiceConfig : ScriptableObject
    {
        [field: SerializeField] public Player PlayerPrefab { get; private set; }
        [field: SerializeField] public Tool ToolPrefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5.0f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 180.0f;
        [field: SerializeField] public SearchModelConfig SearchModelConfig { get; private set; }
        
        public int IsRun => Animator.StringToHash("IsRun");
     }
}