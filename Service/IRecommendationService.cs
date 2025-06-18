using Accord.MachineLearning.Rules;
using System.Collections.Generic;

namespace api_app_pizza_flutter.Service
{
    public interface IRecommendationService
    {
        // Gợi ý sản phẩm theo sản phẩm đầu vào
        List<int> Recommend(int productId, int topN = 5);

        // Trả về toàn bộ luật Apriori đã học
        IEnumerable<AssociationRule<int>> GetRules();
    }
}
