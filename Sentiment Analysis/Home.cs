using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace SentimentApp
{
    public partial class Home : Form
    {
        private MLContext mlContext;
        private PredictionEngine<SentimentData, SentimentPrediction> engine;

        // 🔹 Positive and Negative Keywords
        private List<string> positiveWords = new List<string>
        {
            "good","great","amazing","love","excellent","best","perfect","nice","awesome","fantastic","like",
            "wonderful","brilliant","superb","outstanding","incredible","impressive","fabulous","marvelous","pleasant","delightful",
            "charming","enjoyable","satisfying","beautiful","attractive","cool","epic","genius","helpful","efficient",
            "reliable","smooth","fast","powerful","clean","easy","useful","smart","affordable","valuable",
            "top","favorite","liked","loved","enjoyed","recommended","positive","happy","excited","glad",
            "pleased","thrilled","grateful","fantabulous","spectacular","magnificent","exceptional","terrific","nice-looking","well-done",
            "solid","fine","decent","worthwhile","remarkable","stunning","lovely","adorable","friendly","kind",
            "supportive","comfortable","safe","secure","trustworthy","accurate","precise","neat","organized","flexible",
            "responsive","quick","stable","innovative","creative","intelligent","advanced","improved","upgraded","strong",
            "durable","long-lasting","efficiently","bright","vibrant","fresh","coolest","best-ever","top-notch","five-star"
   };

        private List<string> negativeWords = new List<string>
        {
            "bad","worst","hate","awful","poor","terrible","boring","disappointed","horrible","waste","unlike",
            "useless","annoying","slow","laggy","buggy","broken","cheap","ugly","dirty","noisy",
            "confusing","difficult","hard","complicated","frustrating","irritating","disgusting","pathetic","ridiculous","stupid",            "weak","low","inferior","faulty","damaged","unreliable","unstable","crashing","freezing","error",
            "issue","problem","failure","failed","worst-ever","disaster","mess","junk","trash","garbage",
            "unhappy","sad","angry","upset","depressed","regret","regretful","hate-it","not-good","not-working",
            "badly","poorly","inefficient","slowest","worst-service","bad-quality","low-quality","overpriced","underwhelming","mediocre",
            "lame","boring","dull","uninteresting","tired","old","outdated","obsolete","rough","harsh",
            "uncomfortable","unsafe","risky","insecure","wrong","incorrect","inaccurate","fake","false","misleading",
            "unfair","biased","limited","restricted","blocked","failed-again","worse","declined","downgraded","broken-down"

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

        // 🔹 GENERATE TRAINING SENTENCES FROM KEYWORDS
        private IEnumerable<SentimentData> GetTrainingData()
        {
            var dataList = new List<SentimentData>();
            var rand = new Random();

            // Sentence templates
            var positiveTemplates = new[]
            {
                "I am {0}",
                "Have a {0} day",
                "I feel {0} today",
                "This is {0}",
                "I am very {0}",
                "Such a {0} experience",
                "Absolutely {0}!",
                "I love how {0} this is",
                "Feeling {0} and great",
                "This app is {0}",
                "I think it is {0}",
                "Everything looks {0}"
            };

            var negativeTemplates = new[]
            {
                
                "I feel {0} today",
                "This is {0}",
                "I am very {0}",
                "Such a {0} experience",
                "Absolutely {0}!",
                "I hate how {0} this is",
                "Feeling {0} and bad",
                "This app is {0}",
                "I think it is {0}",
                "Everything looks {0}"
            };

            // 🔹 Generate 100 positive sentences
            for (int i = 0; i < 100; i++)
            {
                string word = positiveWords[rand.Next(positiveWords.Count)];
                string template = positiveTemplates[rand.Next(positiveTemplates.Length)];
                string sentence = string.Format(template, word);
                dataList.Add(new SentimentData { Text = sentence, Label = true });
            }

            // 🔹 Generate 100 negative sentences
            for (int i = 0; i < 100; i++)
            {
                string word = negativeWords[rand.Next(negativeWords.Count)];
                string template = negativeTemplates[rand.Next(negativeTemplates.Length)];
                string sentence = string.Format(template, word);
                dataList.Add(new SentimentData { Text = sentence, Label = false });
            }

            return dataList;
        }

        // 🔹 TRAIN MODEL
        private void TrainModel()
        {
            mlContext = new MLContext();

            var trainData = mlContext.Data.LoadFromEnumerable(GetTrainingData());

            var pipeline = mlContext.Transforms.Text.FeaturizeText(
                                outputColumnName: "Features",
                                inputColumnName: nameof(SentimentData.Text))
                           .Append(mlContext.BinaryClassification.Trainers
                                .SdcaLogisticRegression(
                                    labelColumnName: nameof(SentimentData.Label),
                                    featureColumnName: "Features"));

            var model = pipeline.Fit(trainData);

            engine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }

        // 🔹 PREDICT BUTTON
        private void btnPredict_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter text!");
                return;
            }

            string inputText = textBox1.Text.Trim().ToLower();

            // Check if user input contains any of the training words
            bool containsPositiveWord = positiveWords.Any(w => inputText.Contains(w));
            bool containsNegativeWord = negativeWords.Any(w => inputText.Contains(w));

            if (!containsPositiveWord && !containsNegativeWord)
            {
                // Completely unknown words → Neutral
                lblMessage.Text = "Neutral 😐";
                return;
            }

            // Otherwise, ML model decides Positive/Negative
            var input = new SentimentData { Text = inputText };
            var result = engine.Predict(input);

            lblMessage.Text = result.Prediction ? "Positive 😊" : "Negative 😞";
        }

        // 🔹 EXIT
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 🔹 PANEL DESIGN
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