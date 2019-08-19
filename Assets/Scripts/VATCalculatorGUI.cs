using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class VATCalculatorGUI : MonoBehaviour
{
	//--- PROCENTSATSER(NETTO VS BRUTTO) ---
	//NETTO		BRUTTO
	//25%		20%
	//12%		10,71%
	//6%		5,66%

	[SerializeField] private Dropdown nettoPercent;
	[SerializeField] private Dropdown bruttoPercent;
	private readonly float[] possibleNettoPercentages = { 6.0f, 12.0f, 25.0f };
	private readonly float[] possibleBruttoPercentages = { 5.66f, 10.71f, 20.0f };

	[Space]

	[SerializeField] private InputField nettoMoney;
	[SerializeField] private InputField bruttoMoney;

	private float currentNettoPercent;
	public float CurrentNettoPercent { get { return currentNettoPercent / 100; } }

	private float currentBruttoProcent;
	public float CurrentBruttoProcent { get { return currentBruttoProcent / 100; } }

	private void Start()
	{
		//Set initial values to the most common percentages (25% vs 20%) and update float-versions of these values.
		nettoPercent.value = 2;
		UpdateCurrentPercentages();
	}

	public void OnNettoPercentageChanged()
	{
		bruttoPercent.value = nettoPercent.value;
		UpdateCurrentPercentages();
		OnBruttoMoneyChanged();
		OnNettoMoneyChanged();
	}

	public void OnBruttoPercentageChanged()
	{
		nettoPercent.value = bruttoPercent.value;
		UpdateCurrentPercentages();
		OnNettoMoneyChanged();
		OnBruttoMoneyChanged();
	}

	private void UpdateCurrentPercentages()
	{
		currentNettoPercent = possibleNettoPercentages[nettoPercent.value];
		currentBruttoProcent = possibleBruttoPercentages[bruttoPercent.value];
	}

	public void OnNettoMoneyChanged()
	{
		float currentNetto = float.Parse(nettoMoney.text, CultureInfo.InvariantCulture.NumberFormat);
		float currentBrutto = CalculateBruttoFromNetto(currentNetto, CurrentNettoPercent);
		bruttoMoney.text = currentBrutto.ToString();
	}

	public void OnBruttoMoneyChanged()
	{
		float currentBrutto = float.Parse(bruttoMoney.text, CultureInfo.InvariantCulture.NumberFormat);
		float currentNetto = CalculateNettoFromBrutto(currentBrutto, CurrentBruttoProcent);
		nettoMoney.text = currentNetto.ToString();
	}

	private float CalculateBruttoFromNetto(float nettoMoney, float nettoPercentage)
	{
		float bruttoMoney = nettoMoney + (nettoMoney * nettoPercentage);
		bruttoMoney = Mathf.Round(bruttoMoney);
		return bruttoMoney;
	}

	private float CalculateNettoFromBrutto(float bruttoMoney, float bruttoPercentage)
	{
		float nettoMoney = bruttoMoney + (bruttoMoney * -bruttoPercentage);
		nettoMoney = Mathf.Round(nettoMoney);
		return nettoMoney;
	}
}