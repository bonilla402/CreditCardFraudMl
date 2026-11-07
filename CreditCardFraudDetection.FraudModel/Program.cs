using CreditCardFraudDetection_FraudModel;
using System;
using System.IO;


internal class Program
{
    static void Main()
    {
        Console.WriteLine("=== Credit Card Fraud Detection AutoML: Train → Save ZIP ===");

        // Save into a predictable location under this project's output
        var outputDir = Path.Combine(AppContext.BaseDirectory, "TrainedModels");
        Directory.CreateDirectory(outputDir);

        var zipPath = Path.Combine(outputDir, "CardFraudModel.zip");

        // CSV is stored in the project root
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        var csvPath = Path.Combine(projectRoot, "credit_card_fraud_1000.csv");

        Console.WriteLine($"CSV: {csvPath}");
        Console.WriteLine($"ZIP: {zipPath}");

        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV not found. Put credit_card_fraud_1000 in the project root.");
            return;
        }

        // This runs the AutoML-selected pipeline and SAVES the model to the zip path.
        CardFraudDetectionMoidel.Train(outputModelPath: zipPath, inputDataFilePath: csvPath);

        Console.WriteLine("Approval model ZIP created.");
    }
}
