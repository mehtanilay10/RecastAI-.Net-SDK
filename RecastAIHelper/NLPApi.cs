using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RecastAIHelper.Models;

namespace RecastAIHelper
{
    public partial class RecastAIClient
    {
        #region /Bots API

        public async Task<IReadOnlyCollection<RecastBotLight>> GetAllBotsAsync()
        {
            IReadOnlyCollection<RecastBotLight> bots = Array.Empty<RecastBotLight>();
            var response = await Get($"bots/");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                bots = JsonConvert.DeserializeObject<IReadOnlyCollection<RecastBotLight>>(result.ToString());
            }
            return bots;
        }

        public async Task<RecastBot> GetBotAsync(string botSlug)
        {
            RecastBot bot = new RecastBot();
            var response = await Get($"bots/{botSlug}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                bot = JsonConvert.DeserializeObject<RecastBot>(result.ToString());
            }
            return bot;
        }

        public async Task<RecastBot> UpdateBotAsync(string botSlug, string name = null, string description = null, int? strictness = null, bool? @public = null, string language = null)
        {
            RecastBot updatedBot = null;
            dynamic requestBody = new ExpandoObject();

            if (!string.IsNullOrEmpty(name))
                requestBody.name = name;
            if (!string.IsNullOrEmpty(description))
                requestBody.description = description;
            if (strictness >= 0 && strictness <= 100)
                requestBody.strictness = strictness;
            if (@public.HasValue)
                requestBody.@public = @public;
            if (!string.IsNullOrEmpty(language))
                requestBody.language = language;

            var response = await Put($"bots/{botSlug}", requestBody);
            if(response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if(serviceResponse.Message.Equals("Bot rendered with success"))
                {
                    updatedBot = JsonConvert.DeserializeObject<RecastBot>(serviceResponse.Results.ToString());
                }
            }

            return updatedBot;
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllEntitiesAsync(string botSlug, int pageNo = 0, int pageSize = 0)
        {
            IReadOnlyCollection<Entity> entities = Array.Empty<Entity>();
            var response = await Get($"bots/{botSlug}/entities?per_page={pageSize}&page={pageNo}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<Entity>>(result.ToString());
            }
            return entities;
        }

        #endregion

        #region /Intents API

        public async Task<IReadOnlyCollection<Intent>> GetAllIntentsAsync(string botSlug)
        {
            IReadOnlyCollection<Intent> intetns = Array.Empty<Intent>();
            var response = await Get($"bots/{botSlug}/intents");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                intetns = JsonConvert.DeserializeObject<IReadOnlyCollection<Intent>>(result.ToString());
            }
            return intetns;
        }

        public async Task<IReadOnlyCollection<Intent>> GetIntentsPagewiseAsync(string botSlug, int pageNo = 0, int pageSize = 0)
        {
            IReadOnlyCollection<Intent> intetns = Array.Empty<Intent>();
            var response = await Get($"bots/{botSlug}/intents?per_page={pageSize}&page={pageNo}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                intetns = JsonConvert.DeserializeObject<IReadOnlyCollection<Intent>>(result.ToString());
            }
            return intetns;
        }

        public async Task<Intent> GetIntentDetailsAsync(string botSlug, string intentSlug)
        {
            Intent intent = new Intent();
            var response = await Get($"bots/{botSlug}/intents/{intentSlug}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                intent = JsonConvert.DeserializeObject<Intent>(result.ToString());
            }
            return intent;
        }

        public async Task<Intent> CreateIntentAsync(string botSlug, string name, string description, List<Expression> expressions)
        {
            Intent newCreatedIntent = null;
            dynamic requestBody = new ExpandoObject();
            requestBody.name = name;
            requestBody.description = description;
            requestBody.expressions = expressions;

            var response = await Post($"bots/{botSlug}/intents", requestBody);
            if (response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if (serviceResponse.Message.Equals("Intent created with success"))
                {
                    newCreatedIntent = JsonConvert.DeserializeObject<Intent>(serviceResponse.Results.ToString());
                }
            }

            return newCreatedIntent;
        }

        public async Task<Intent> UpdateIntentAsync(string botSlug, string intentSlug, string name = null, string description = null)
        {
            Intent updateIntent = null;
            dynamic requestBody = new ExpandoObject();

            if (!string.IsNullOrEmpty(name))
                requestBody.name = name;
            if(!string.IsNullOrEmpty(description))
                requestBody.description = description;

            var response = await Put($"bots/{botSlug}/intents/{intentSlug}", requestBody);
            if (response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if (serviceResponse.Message.Equals("Intent rendered with success"))
                {
                    updateIntent = JsonConvert.DeserializeObject<Intent>(serviceResponse.ToString());
                }
            }

            return updateIntent;
        }

        public async Task<bool> DeleteIntentAsync(string botSlug, string intentSlug)
        {
            var response = await Delete($"bots/{botSlug}/intents/{intentSlug}");
            if (response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if (serviceResponse.Message.Equals("Intent deleted with success"))
                    return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<Entity>> GetEntitiesOfIntentAsync(string botSlug, string intentSlug, int pageNo = 0, int pageSize = 0)
        {
            IReadOnlyCollection<Entity> entities = Array.Empty<Entity>();
            var response = await Get($"bots/{botSlug}/intents/{intentSlug}/entities?per_page={pageSize}&page={pageNo}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                entities = JsonConvert.DeserializeObject<IReadOnlyCollection<Entity>>(result.ToString());
            }
            return entities;
        }

        #endregion

        #region /Expressions

        public async Task<IReadOnlyCollection<Expression>> GetExpressionsAsync(string botSlug, string intentSlug, int pageNo = 0, int pageSize = 0, string filter = null)
        {
            IReadOnlyCollection<Expression> expressions = Array.Empty<Expression>();
            var response = await Get($"bots/{botSlug}/intents/{intentSlug}/expressions/?per_page={pageSize}&page={pageNo}&filter={filter}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                expressions = JsonConvert.DeserializeObject<IReadOnlyCollection<Expression>>(result.ToString());
            }
            return expressions;
        }

        public async Task<Expression> GetExpressionDetailsAsync(string botSlug, string intentSlug, string expressionId)
        {
            Expression expression = new Expression();
            var response = await Get($"bots/{botSlug}/intents/{intentSlug}/expressions/{expressionId}");
            if (response != null)
            {
                object result = JsonConvert.DeserializeObject<ServiceResponse>(response).Results;
                expression = JsonConvert.DeserializeObject<Expression>(result.ToString());
            }
            return expression;
        }

        public async Task<Expression> UpdateExpressionAsync(string botSlug, string intentSlug, string expressionId, string source = null, List<Token> tokens = null)
        {
            Expression updatedExpression = null;
            dynamic requestBody = new ExpandoObject();

            if (!string.IsNullOrEmpty(source))
                requestBody.source = source;
            if (tokens != null)
                requestBody.tokens = tokens;

            var response = await Put($"bots/{botSlug}/intents/{intentSlug}/expressions/{expressionId}", requestBody);
            if (response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if (serviceResponse.Message.Equals("Expression rendered with success"))
                {
                    updatedExpression = JsonConvert.DeserializeObject<Expression>(serviceResponse.ToString());
                }
            }

            return updatedExpression;
        }

        public async Task<bool> DeleteExpressionAsync(string botSlug, string intentSlug, string expressionId)
        {
            var response = await Delete($"bots/{botSlug}/intents/{intentSlug}/expressions/{expressionId}");
            if (response != null)
            {
                ServiceResponse serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(response);
                if (serviceResponse.Message.Equals("Expression deleted with success"))
                    return true;
            }
            return false;
        }

        #endregion

        #region /Tokens

        // TODO

        #endregion

        #region /Words

        // TODO

        #endregion

        #region /Entities

        // TODO

        #endregion

        #region /Logs

        // TODO

        #endregion

        #region /Gazettes

        // TODO

        #endregion

        #region /Synonyms

        // TODO

        #endregion

        #region /Languages

        // TODO

        #endregion
    }
}
