using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning.Preview;

// e6c82f6e-c60f-422a-97b6-e0406cba82da_6ed0259c-001e-4895-be7a-4a930321a307

namespace VincentML
{
    public sealed class E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelInput
    {
        public VideoFrame data { get; set; }
    }

    public sealed class E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelOutput
    {
        public IList<string> classLabel { get; set; }
        public IDictionary<string, float> loss { get; set; }
        public E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelOutput()
        {
            this.classLabel = new List<string>();
            this.loss = new Dictionary<string, float>()
            {
                { "Donald Trump", float.NaN },
                { "Yinianpeng", float.NaN },
            };
        }
    }

    public sealed class E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307Model
    {
        private LearningModelPreview learningModel;
        public static async Task<E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307Model> CreateE6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307Model(StorageFile file)
        {
            LearningModelPreview learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);
            E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307Model model = new E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307Model();
            model.learningModel = learningModel;
            return model;
        }
        public async Task<E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelOutput> EvaluateAsync(E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelInput input) {
            E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelOutput output = new E6c82f6e_x002D_c60f_x002D_422a_x002D_97b6_x002D_e0406cba82da_6ed0259c_x002D_001e_x002D_4895_x002D_be7a_x002D_4a930321a307ModelOutput();
            LearningModelBindingPreview binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("data", input.data);
            binding.Bind("classLabel", output.classLabel);
            binding.Bind("loss", output.loss);
            LearningModelEvaluationResultPreview evalResult = await learningModel.EvaluateAsync(binding, string.Empty);
            return output;
        }
    }
}
