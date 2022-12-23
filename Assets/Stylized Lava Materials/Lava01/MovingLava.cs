using UnityEngine;

public class MovingLava : MonoBehaviour

{
    public float scrollSpeed  = 0.1f;
    public float playerSpeed;
    public Vector2 direction = new Vector2(1,0);
    public Vector2 tiling = new Vector2(1,1);
    Renderer rend;
    Texture2D wavesDisplacement;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        wavesDisplacement = (Texture2D)rend.material.GetTexture("_BumpMap");
    }

    // Update is called once per frame
    void Update()
    {
        float moveThis = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", direction * moveThis *-1);
        
        rend.material.SetTextureScale("_MainTex", tiling);
        rend.material.SetTextureOffset("_BumpMap", tiling);
        
        
    }

    public Vector3 GerstnerWave (
			Vector4 wave, Vector3 p, Vector3 tangent, Vector3 binormal
		) {
		    float steepness = wave.z;
		    float wavelength = wave.w;
		    float k = 2 * 3.14159265359f / wavelength;
			float c = Mathf.Sqrt(9.8f / k);
            Vector2 d = new Vector2(wave.normalized.x, wave.normalized.y);
			float f = k * (Vector2.Dot(d, new Vector2(p.x, p.z)) - c * Time.fixedTime);
			float a = steepness / k;

			tangent += new Vector3(
				-d.x * d.x * (steepness * Mathf.Sin(f)),
				d.x * (steepness * Mathf.Cos(f)),
				-d.x * d.y * (steepness * Mathf.Sin(f))
			);
			binormal += new Vector3(
				-d.x * d.y * (steepness *  Mathf.Sin(f)),
				d.y * (steepness *  Mathf.Cos(f)),
				-d.y * d.y * (steepness *  Mathf.Sin(f))
			);
			return new Vector3(
				d.x * (a *  Mathf.Cos(f)),
				a *  Mathf.Sin(f),
				d.y * (a *  Mathf.Cos(f))
			);
		}
    
    public float LiquidYAtPosition(Vector3 position) {
			Vector3 tangent = new Vector3(1, 0, 0);
			Vector3 binormal = new Vector3(0, 0, 1);
			Vector3 p = new Vector3 (position.x, 0, position.z);
			p += GerstnerWave(rend.material.GetVector("_WaveA"), position, tangent, binormal);
			p += GerstnerWave(rend.material.GetVector("_WaveB"), position, tangent, binormal);
			p += GerstnerWave(rend.material.GetVector("_WaveC"), position, tangent, binormal);

            return p.y;
			
		}

}
