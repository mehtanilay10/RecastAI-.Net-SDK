# RecastAI-.Net-SDK
Basic RecastAI .Net SDK for NPL. 
Currently it supports Methods for Bots, Intents, and Expressions. Feel free to extend this library by adding other methods for NPL, Bot Builder, and Bot Connector.

### Create Client
To create client you need to pass token and slug for user on which you are goung to perform all operations.
```
// Create Helper Instance
RecastAIHelper.RecastAIClient client = new RecastAIHelper.RecastAIClient(token, userSlug);
```

### Using Client
After creating client, you can call appropriate method by passing required parameters.
```
// Retrieve all bots of specific user
IReadOnlyCollection<RecastBotLight> bots = await client.GetAllBotsAsync();

// Retrieve Intent pagewise
IReadOnlyCollection<Intent> intentsPageWise = await client.GetIntentsPagewiseAsync(botSlug, 1, 2);

// Create new Intent
intent = await client.CreateIntentAsync(botSlug, "intentSlug", "description", new List<Expression> {
    new Expression("Hello From SDK", "en")
});

```

Recast API: https://recast.ai/docs/api-reference <br/>
Use of other Metohds: https://github.com/mehtanilay10/RecastAI-.Net-SDK/blob/master/RecastAIHelperUse/Program.cs
