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

	[Space]

	[SerializeField] private Toggle roundToWholeNumbersToggle;

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
		string text = nettoMoney.text;

		//Error check
		if (string.IsNullOrEmpty(text))
		{
			return;
		}

		//Detect if there is a comma instead of dot in the input string (to avoid errors)
		if (text.Contains(","))
		{
			text = text.Replace(",", ".");
		}

		float currentNetto = float.Parse(text, CultureInfo.InvariantCulture);
		float currentBrutto = CalculateBruttoFromNetto(currentNetto, CurrentNettoPercent, roundToWholeNumbersToggle.isOn);
		bruttoMoney.text = currentBrutto.ToString();
	}

	public void OnBruttoMoneyChanged()
	{
		string text = bruttoMoney.text;

		//Error check
		if (string.IsNullOrEmpty(text))
		{
			return;
		}

		//Detect if there is a comma instead of dot in the input string (to avoid errors)
		if (text.Contains(","))
		{
			text = text.Replace(",", ".");
		}

		float currentBrutto = float.Parse(text, CultureInfo.InvariantCulture);
		float currentNetto = CalculateNettoFromBrutto(currentBrutto, CurrentBruttoProcent, roundToWholeNumbersToggle.isOn);
		nettoMoney.text = currentNetto.ToString();
	}

	private float CalculateBruttoFromNetto(float nettoMoney, float nettoPercentage, bool roundToWholeNumbers = true)
	{
		float bruttoMoney = nettoMoney + (nettoMoney * nettoPercentage);
		if (roundToWholeNumbers)
		{
			bruttoMoney = Mathf.Round(bruttoMoney);
		}
		return bruttoMoney;
	}

	private float CalculateNettoFromBrutto(float bruttoMoney, float bruttoPercentage, bool roundToWholeNumbers = true)
	{
		float nettoMoney = bruttoMoney + (bruttoMoney * -bruttoPercentage);
		if (roundToWholeNumbers)
		{
			nettoMoney = Mathf.Round(nettoMoney);
		}
		return nettoMoney;
	}
}