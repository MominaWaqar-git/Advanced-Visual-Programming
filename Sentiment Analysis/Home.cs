using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SentimentApp
{
    public partial class Home : Form
    {
        private MLContext mlContext;
        private PredictionEngine<SentimentData, SentimentPrediction> engine;

        // 🔹 Positive Words
        private List<string> positiveWords = new List<string>
        {
            "good","great","amazing","love","excellent","best","perfect","nice","awesome","fantastic","like",
            "wonderful","brilliant","superb","outstanding","incredible","impressive","fabulous","pleasant",
            "enjoyable","satisfying","beautiful","cool","epic","helpful","efficient","reliable","smooth",
            "fast","powerful","easy","useful","smart","affordable","valuable","happy","excited","glad"
        };

        // 🔹 Negative Words
        private List<string> negativeWords = new List<string>
        {
            "bad","worst","hate","awful","poor","terrible","boring","disappointed","horrible","waste",
            "useless","annoying","slow","buggy","broken","ugly","dirty","confusing","difficult",
            "frustrating","irritating","disgusting","pathetic","stupid","weak","faulty","error",
            "problem","failure","disaster","mess","junk","trash","sad","angry","upset"
        };

        public Home()
        {
            InitializeComponent();
            TrainModel();
        }

        // 🔹 DATA CLASS
        public class SentimentData
        {
            public string Text { get; set; }
            public bool Label { get; set; }
        }

        // 🔹 PREDICTION CLASS
        public class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }
            public float Probability { get; set; }
        }

        // 🔹 GENERATE 5000 TRAINING DATA
        private IEnumerable<SentimentData> GetTrainingData()
        {
            var dataList = new List<SentimentData>();
            var rand = new Random();

            var positiveTemplates = new[]
            {
                "I really {0} this",
                "This is very {0}",
                "I feel so {0} about this",
                "Absolutely {0} experience",
                "One of the most {0} things ever",
                "I am extremely {0}",
                "This product is {0}",
                "Such a {0} moment",
                "Totally {0}",
                "Highly {0} and recommended"
            };

            var negativeTemplates = new[]
            {
                "I really {0} this",
                "This is very {0}",
                "I feel so {0} about this",
                "Absolutely {0} experience",
                "One of the most {0} things ever",
                "I am extremely {0}",
                "This product is {0}",
                "Such a {0} moment",
                "Totally {0}",
                "Highly {0} and not recommended"
            };

            // ✅ 2500 Positive
            for (int i = 0; i < 2500; i++)
            {
                string word = positiveWords[rand.Next(positiveWords.Count)];
                string template = positiveTemplates[rand.Next(positiveTemplates.Length)];
                string sentence = string.Format(template, word);

                dataList.Add(new SentimentData { Text = sentence, Label = true });
            }

            // ✅ 2500 Negative
            for (int i = 0; i < 2500; i++)
            {
                string word = negativeWords[rand.Next(negativeWords.Count)];
                string template = negativeTemplates[rand.Next(negativeTemplates.Length)];
                string sentence = string.Format(template, word);

                dataList.Add(new SentimentData { Text = sentence, Label = false });
            }

            return dataList;
        }

        // 🔹 TRAIN MODEL (Improved)
        private void TrainModel()
        {
            mlContext = new MLContext();

            var trainData = mlContext.Data.LoadFromEnumerable(GetTrainingData());

            var pipeline = mlContext.Transforms.Text.FeaturizeText(
                                outputColumnName: "Features",
                                inputColumnName: nameof(SentimentData.Text))
                           .Append(mlContext.BinaryClassification.Trainers.FastTree(
                                labelColumnName: nameof(SentimentData.Label),
                                featureColumnName: "Features"));

            var model = pipeline.Fit(trainData);

            engine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        // 🔹 PREDICT BUTTON (FIXED LOGIC)
        private void btnPredict_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter text!");
                return;
            }

            string inputText = textBox1.Text.Trim().ToLower();

            // 🔥 WORD SPLIT
            var words = inputText.Split(' ');

            int positiveCount = words.Count(w => positiveWords.Contains(w));
            int negativeCount = words.Count(w => negativeWords.Contains(w));

            // Neutral
            if (positiveCount == 0 && negativeCount == 0)
            {
                lblMessage.Text = "Neutral 😐";
                return;
            }

            // Strong keyword decision
            if (positiveCount > negativeCount)
            {
                lblMessage.Text = "Positive 😊";
                return;
            }
            else if (negativeCount > positiveCount)
            {
                lblMessage.Text = "Negative 😞";
                return;
            }

            // 🤖 ML Model fallback
            var input = new SentimentData { Text = inputText };
            var result = engine.Predict(input);

            lblMessage.Text = result.Prediction
                ? $"Positive 😊 ({result.Probability:P2})"
                : $"Negative 😞 ({result.Probability:P2})";
        }

        // 🔹 EXIT
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 🔹 UI DESIGN
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panel1.ClientRectangle,
                Color.HotPink,
                Color.Purple,
                45F))
            {
                e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panel2.ClientRectangle,
                Color.HotPink,
                Color.Purple,
                45F))
            {
                e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
            }
        }
    }
}