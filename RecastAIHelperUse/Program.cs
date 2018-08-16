using System.Collections.Generic;
using System.Threading.Tasks;
using RecastAIHelper.Models;

namespace RecastAIHelperUse
{
    class Program
    {
        public const string userSlug = "userSlug";
        public const string botSlug = "botSlug";
        public const string token = "token";

        static void Main(string[] args)
        {
            MainAsync();
        }


        static async Task MainAsync()
        {
            // Create Helper Instance
            RecastAIHelper.RecastAIClient client = new RecastAIHelper.RecastAIClient(token, userSlug);

            // Retrieve all bots of specific user
            IReadOnlyCollection<RecastBotLight> bots = await client.GetAllBotsAsync();

            // Obtain specific bot details
            RecastBot bot = await client.GetBotAsync(botSlug);

            // Update Bot details
            bot = await client.UpdateBotAsync(botSlug, description: "Description via SDK");

            // Obtain List of Entities for specific bot
            IReadOnlyCollection<Entity> entities = await client.GetAllEntitiesAsync(botSlug);

            // Obtain List of Intents for specific bot
            IReadOnlyCollection<Intent> intents = await client.GetAllIntentsAsync(botSlug);

            // Retrieve Intent pagewise
            IReadOnlyCollection<Intent> intentsPageWise = await client.GetIntentsPagewiseAsync(botSlug, 1, 2);

            // Obtain specific intent details
            Intent intent = await client.GetIntentDetailsAsync(botSlug, "intentSlug");

            // Create new Intent
            intent = await client.CreateIntentAsync(botSlug, "intentSlug", "description", new List<Expression> {
                new Expression("Hello From SDK", "en")
            });

            // Update Intent
            intent = await client.UpdateIntentAsync(botSlug, "intentSlug", "Intent Name", "Intent Desciption");

            // Delete Intnet
            await client.DeleteIntentAsync(botSlug, "intentSlug");

            // Obtain Entities of Intent pagewise
            IReadOnlyCollection<Entity> entitiesForIntent = await client.GetEntitiesOfIntentAsync(botSlug, "intentSlug", 1, 1);

            // Obtain List of Expressions
            IReadOnlyCollection<Expression> expressions = await client.GetExpressionsAsync(botSlug, "intentSlug", 1, 2);

            // Obtain expression
            Expression expression = await client.GetExpressionDetailsAsync(botSlug, "intentSlug", "expressionGuid");

            // Update Expression
            Expression updatedExpression = await client.UpdateExpressionAsync(botSlug, "intentSlug", "expressionGuid", "sourceText");

            // Delete specific Expression from Intent
            bool status = await client.DeleteExpressionAsync(botSlug, "intentSlug", "expressionGuid");
        }
    }
}
