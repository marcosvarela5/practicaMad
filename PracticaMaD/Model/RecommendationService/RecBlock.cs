using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecommendationService
{
    public class RecBlock
    {
        /// <summary>
        /// Gets the recommendations.
        /// </summary>
        /// <value>
        /// The recommendations.
        /// </value>
        public List<RecData> recommendations { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [exists more].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exists more]; otherwise, <c>false</c>.
        /// </value>
        public bool existsMore { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecBlock"/> class.
        /// </summary>
        /// <param name="recomendations">The recomendations.</param>
        /// <param name="existMoreRecomendations">if set to <c>true</c> [exist more recomendations].</param>
        public RecBlock(List<RecData> recomendations, bool existMoreRecomendations)
        {
            this.recommendations = recomendations;
            this.existsMore = existMoreRecomendations;
        }
    }
}
