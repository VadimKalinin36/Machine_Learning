namespace Machine_Learning.Models
{
    public class ModelLabel
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public string Name { get; set; }

        public float Score { get; set; }

        public static List<ModelLabel> GetLabels()
        {
            return new List<ModelLabel>()
                {
                    new ModelLabel { Id = 0, Label = "Шелковое пятно (silk_spot)", Name = "silk_spot" },
                    new ModelLabel { Id = 1, Label = "Поясная складка (waist_folding)", Name = "waist_folding" },
                };
        }
    }
}
