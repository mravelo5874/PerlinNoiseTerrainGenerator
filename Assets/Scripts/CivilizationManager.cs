using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilizationManager : MonoBehaviour
{
    List<CityManager> cities;

    private void Awake() {
        cities = new List<CityManager>();
    }

    public void AddCityToCiv(CityManager city) {
        cities.Add(city);
    }

    public void UnselectAllCities() {
        foreach(CityManager city in cities) {
            city.DeselectCity();
        }
    }
}
