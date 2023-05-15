using MachineLearningApp;
using Microsoft.ML;

static void ProcessInput()
{
    // Initial input
    OutputMessage("Give a review and I'll analyze the sentiment:");
    string? userInput = Console.ReadLine();

    // Predict input's sentiment
    var data = new SentimentModel.ModelInput() { Col0 = userInput ?? string.Empty };
    var result = SentimentModel.Predict(data);

    // Show output
    OutputResult(userInput ?? string.Empty, result.PredictedLabel);

    // Prompt for feedback
    OutputMessage("Is my analysis accurate? (Y/N)");
    string? feedbackInput = Console.ReadLine();
    bool needsAdjustment 
        = !string.IsNullOrWhiteSpace(feedbackInput) && (feedbackInput.Trim().ToUpper() == "N");
    OutputMessage(needsAdjustment ? "Nice!" : "Sounds good. Thanks for the feedback!");

    // Retrain model
    ReTrainModel(userInput ?? string.Empty, feedbackInput ?? string.Empty, result.PredictedLabel);

    // Recursive call
    ProcessInput();
}

static void OutputMessage(string message)
{
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine(message);
    Console.ResetColor();
}

static void OutputResult(string userInput, float predictedLabel)
{
    var sentiment = predictedLabel == 1 ? "Positive" : "Negative";
    Console.ForegroundColor = predictedLabel == 1 ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine($"\n\n\n\n" +
        $"Text: {userInput ?? "None"}\n" +
        $"Sentiment: {(userInput == null ? string.Empty : sentiment)}\n\n\n\n");
    Console.ResetColor();
}

static void ReTrainModel(string userInput, string feedbackInput, float predictedLabel)
{
    /* Future implementation */

    //if (!string.IsNullOrWhiteSpace(userInput) && !string.IsNullOrWhiteSpace(feedbackInput))
    //{
    //    string datasetFilePath 
    //        = $"{Environment.CurrentDirectory.Split("MachineLearningApp\\bin")[0]}"
    //        + $"yelp_labelled.txt";
    //    var mlContext = new MLContext();

    //    var existingModel = mlContext.Model.Load(SentimentModel.MLNetModelPath, out _);

    //    using (var reader = new StreamReader(datasetFilePath))
    //    {
    //        var existingData = new List<SentimentModel.ModelInput>();
            
    //        while (!reader.EndOfStream)
    //        {
    //            var line = reader.ReadLine();
    //            var values = line?.Split('\t');
    //            var input = new SentimentModel.ModelInput 
    //            { 
    //                Col0 = values![0].ToString(), 
    //                Col1 = float.Parse(values[1]) 
    //            };
    //            existingData.Add(input);
    //        }

    //        var label = feedbackInput.Trim().ToUpper() == "Y"
    //        ? predictedLabel : (predictedLabel == 1 ? 0 : 1);
    //        var newData = new SentimentModel.ModelInput { Col0 = userInput, Col1 = label };
    //        var updatedData = existingData.Append(newData);
    //        var updatedDataView = mlContext.Data.LoadFromEnumerable(updatedData);
    //        var updatedModel = SentimentModel.RetrainPipeline(mlContext, updatedDataView);

    //        mlContext.Model.Save(updatedModel, updatedDataView.Schema, SentimentModel.MLNetModelPath);
    //    }
    //}
}

ProcessInput();