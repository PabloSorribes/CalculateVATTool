using UnityEngine;

public class MomsCalculator : MonoBehaviour
{
	//--- PROCENTSATSER(NETTO VS BRUTTO) ---
	//NETTO		BRUTTO
	//25%		20%
	//12%		10,71%
	//6%		5,66%

	public float netto;
	[Tooltip("Normal values: 25%, 12% & 6%")]
	public float nettoProcent;
	public float NettoProcent { get { return nettoProcent / 100; } }

	public float brutto;
	[Tooltip("Normal values: 20%, 10.71% & 5.66%")]
	public float bruttoProcent;
	public float BruttoProcent { get { return bruttoProcent / 100; } }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("Netto " + netto + ", Brutto " + brutto + "\n" +
				"<color=yellow>Brutto based on Netto: " + CalculateBruttoFromNetto() + "</color>");
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Debug.Log("Netto " + netto + ", Brutto " + brutto + "\n" +
				"<color=blue>Netto based on Brutto: " + CalculateNettoFromBrutto() + "</color>");
		}
	}

	private float CalculateBruttoFromNetto()
	{
		brutto = netto + (netto * NettoProcent);
		brutto = Mathf.Round(brutto);
		return brutto;
	}

	private float CalculateNettoFromBrutto()
	{
		netto = brutto + (brutto * -BruttoProcent);
		netto = Mathf.Round(netto);
		return netto;
	}
}