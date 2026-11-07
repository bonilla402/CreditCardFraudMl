using CardFraudDetection.Console.ML;
using Microsoft.ML;

var modelsDir = Path.Combine(AppContext.BaseDirectory, "ML Models");

var modelPath = Path.Combine(modelsDir, "CardFraudModel.zip");

MLContext mlContext = new(seed: 42);

if (!File.Exists(modelPath))
    throw new FileNotFoundException("Fraud model not found.", modelPath);

using var stream = File.OpenRead(modelPath);

var cardFraudModel = mlContext.Model.Load(stream, out _);

var engine = mlContext.Model.CreatePredictionEngine<TransactionInput, FraudPrediction>(cardFraudModel);

var input = new TransactionInput
{
    TransactionAmount = 450.00f,
    HourOfDay = 3,
    MerchantCategory = "Electronics",
    EntryMode = "ManualKeyed",
    Country = "BR",
    IsInternational = 1,
    CardPresent = 0,
    DaysSinceLastTransaction = 0.1f,
    TransactionCount24h = 8,
    AvgAmount7d = 60.00f
};

var prediction = engine.Predict(input);

Console.WriteLine("=== Prediction Input ===");
Console.WriteLine($"TransactionAmount:        {input.TransactionAmount}");
Console.WriteLine($"HourOfDay:                {input.HourOfDay}");
Console.WriteLine($"MerchantCategory:         {input.MerchantCategory}");
Console.WriteLine($"EntryMode:                {input.EntryMode}");
Console.WriteLine($"Country:                  {input.Country}");
Console.WriteLine($"IsInternational:          {input.IsInternational}");
Console.WriteLine($"CardPresent:              {input.CardPresent}");
Console.WriteLine($"DaysSinceLastTransaction: {input.DaysSinceLastTransaction}");
Console.WriteLine($"TransactionCount24h:      {input.TransactionCount24h}");
Console.WriteLine($"AvgAmount7d:              {input.AvgAmount7d}");

Console.WriteLine();
Console.WriteLine("=== Prediction Result ===");
var labelProbability = prediction.IsFraud ? prediction.Probability : 1 - prediction.Probability;

Console.WriteLine($"Predicted label: {(prediction.IsFraud ? "Fraud" : "Not Fraud")}");
Console.WriteLine($"Probability:     {labelProbability:P2}");