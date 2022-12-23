using System.Collections.Generic;
using UnityEngine;

public class RunningLights : MonoBehaviour
{
    public int lightsNumber;
    public Color color1;
    public Color color2;
    public float minIntensity;
    public float maxIntensity;
    public float minRange;
    public float maxRange;
    public Vector3 speed;
    public List<Light> lights = new List<Light>();
    public Collider mainCollider;

    // Start is called before the first frame update
    void Start()
    {
        mainCollider = GetComponent<Collider>();
        for (int i = 0; i < lightsNumber; i++)
        {
            GameObject newLightObj = new GameObject("RunningLigth");
            Light newLight = newLightObj.AddComponent<Light>();
            newLight.type = LightType.Point;
            newLight.transform.parent = this.transform;

            float randomX = Random.Range(0,mainCollider.bounds.size.x);
            float randomY = Random.Range(0,mainCollider.bounds.size.y);
            float randomZ = Random.Range(0,mainCollider.bounds.size.z);

            Vector3 newPos = new Vector3(
                mainCollider.bounds.center.x - mainCollider.bounds.extents.x + randomX,
                mainCollider.bounds.center.y - mainCollider.bounds.extents.y + randomY,
                mainCollider.bounds.center.z - mainCollider.bounds.extents.z + randomZ
            );
            newLight.transform.position = newPos;
            newLight.color = Color.Lerp(color1,color2, Random.Range(0.0f, 1.1f));
            newLight.range = Random.Range(minRange, maxRange);
            newLight.intensity = Random.Range(minIntensity, maxIntensity);
            lights.Add(newLight);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            Vector3 newSpeed = speed * Time.deltaTime;
            lights[i].transform.Translate(newSpeed, Space.World);
            if (speed.x>0){
                if (lights[i].transform.position.x > mainCollider.bounds.center.x + mainCollider.bounds.extents.x){
                    lights[i].transform.position = new Vector3(
                        mainCollider.bounds.center.x - mainCollider.bounds.extents.x,
                        lights[i].transform.position.y,
                        lights[i].transform.position.z
                    );
                }
            }
            if (speed.x<0){
                if (lights[i].transform.position.x < mainCollider.bounds.center.x - mainCollider.bounds.extents.x){
                    lights[i].transform.position = new Vector3(
                        mainCollider.bounds.center.x + mainCollider.bounds.extents.x,
                        lights[i].transform.position.y,
                        lights[i].transform.position.z
                    );
                }
            }
            if (speed.y<0){
                if (lights[i].transform.position.y < mainCollider.bounds.center.y - mainCollider.bounds.extents.y){
                    lights[i].transform.position = new Vector3(
                        lights[i].transform.position.x,
                        mainCollider.bounds.center.y + mainCollider.bounds.extents.y,
                        lights[i].transform.position.z
                    );
                }
            }
            if (speed.y>0){
                if (lights[i].transform.position.y > mainCollider.bounds.center.y + mainCollider.bounds.extents.y){
                    lights[i].transform.position = new Vector3(
                        lights[i].transform.position.x,
                        mainCollider.bounds.center.y - mainCollider.bounds.extents.y,
                        lights[i].transform.position.z
                    );
                }
            }
            
        }
        

        
    }
}
