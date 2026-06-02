using UnityEngine;

public class CelestialBodyData : MonoBehaviour
{
    [Header("Planet Information")]
    [SerializeField] public string celestialBodyName = "Earth";
    [SerializeField] public string celestialBodyType = "Terrestrial";
    [SerializeField][TextArea(3,5)] public string description = "The third planet from the Sun and the only astronomical object known to harbor life.";
}
