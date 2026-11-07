using Microsoft.ML.Data;

namespace CardFraudDetection.Console.ML
{
    // Input must match the training columns (TransactionId was ignored in training)
    public class TransactionInput
    {
        public float TransactionAmount { get; set; }
        public float HourOfDay { get; set; }
        public string MerchantCategory { get; set; } = string.Empty;
        public string EntryMode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public float IsInternational { get; set; }
        public float CardPresent { get; set; }
        public float DaysSinceLastTransaction { get; set; }
        public float TransactionCount24h { get; set; }
        public float AvgAmount7d { get; set; }

        // Label column; the trained pipeline expects it to exist, value is ignored at prediction
        public bool Fraud { get; set; }
    }

    // Output of the fraud (binary classification) model
    public class FraudPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsFraud { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
