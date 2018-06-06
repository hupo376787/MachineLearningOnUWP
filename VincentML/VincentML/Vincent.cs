using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// e6c82f6e-c60f-422a-97b6-e0406cba82da_6ed0259c-001e-4895-be7a-4a930321a307

namespace VincentML
{
    public sealed class ModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class ModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public ModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "Donald Trump", float.NaN },
                { "Yinianpeng", float.NaN },
            };
        }
    }

    public sealed class Model
    {
        private LearningModelPreview learningModel;
        public static async Task<Model> CreateModel(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            Model model = new Model();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<ModelOutput> EvaluateAsync(ModelInput input) {
            ModelOutput output = new ModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
