using Microsoft.ML.Data;

public class SentimentData
{
    [LoadColumn(0)]
    public string Text { get; set; }

    [LoadColumn(1)]
    public string Sentiment { get; set; }
}

public class SentimentPrediction
{
    [ColumnName("PredictedLabel")]
    public string Prediction { get; set; }
}