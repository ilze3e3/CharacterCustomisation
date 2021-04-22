using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentCustomisation : MonoBehaviour
{
    //Helmet Gameobjects
    public GameObject helmet;
    public List<GameObject> helmetOptions = new List<GameObject>();
    //Helmet UI
    public GameObject helmetSlider;

    // Start is called before the first frame update
    void Start()
    {
        helmetSlider.GetComponent<Slider>().maxValue = helmetOptions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHelmet(float _sliderValue)
    {
        helmet = helmetOptions[(int)_sliderValue];
    }
}
