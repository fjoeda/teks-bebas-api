using Sentiment = TeksBebasAPI.MLModel;
using Emotion = TeksBebasAPI.EmotionClassificationModel;
using TeksBebasAPI.Models;

namespace TeksBebasAPI.Services
{
    public class TextClassifier
    {
        public static TextClassifier GetInstance { 
            get {
                if(instance == null)
                {
                    instance = new TextClassifier();
                }
                return instance;
            } 
        }
        private static TextClassifier? instance;
        private TextClassifier() { }

        public TextResponseSchema GetSentiment(string text)
        {
            var modelInput = new Sentiment.ModelInput()
            {
                Text = text
            };
            var scores = Sentiment.PredictAllLabels(modelInput);
            string predictedLabel = "";

            float maxProb = -1f;
            foreach (var score in scores)
            {
                if (maxProb < score.Value)
                {
                    predictedLabel = score.Key;
                    maxProb = score.Value;
                }
            }

            
            var result = new TextResponseSchema() { 
                Text = text,
                Label = predictedLabel,
                Score = scores.ToDictionary()
            };

            return result;
        }

        public TextResponseSchema GetEmotion(string text)
        {
            var modelInput = new Emotion.ModelInput()
            {
                Text = text
            };
            var scores = Emotion.PredictAllLabels(modelInput);
            string predictedLabel = "";

            float maxProb = -1f;
            foreach (var score in scores)
            {
                if (maxProb < score.Value)
                {
                    predictedLabel = score.Key;
                    maxProb = score.Value;
                }
            }


            var result = new TextResponseSchema()
            {
                Text = text,
                Label = predictedLabel,
                Score = scores.ToDictionary()
            };

            return result;
        }

    }
}
