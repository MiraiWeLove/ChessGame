using TMPro;
using UnityEngine;

public class ShowCoords : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMP;

    private void OnEnable()
    {
        string coordText = "x:" + gameObject.transform.position.x + " y:" + gameObject.transform.position.z;
        _textMP.text = coordText;
    }

}
