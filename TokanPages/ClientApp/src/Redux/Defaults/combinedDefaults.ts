import { ArticleDefaultValues } from "./selectArticleDefault";
import { ArticlesDefaultValues } from "./listArticlesDefault";
import { UpdateArticleDefaultValues } from "./updateArticleDefault";
import { SendMessageDefaultValues } from "./sendMessageDefault";
import { addSubscriberDefault } from "./addSubscriberDefault";
import { UpdateSubscriberDefaultValues } from "./updateSubscriberDefault";
import { RemoveSubscriberDefaultValues } from "./removeSubscriberDefault";

export const combinedDefaults = 
{
    selectArticle: ArticleDefaultValues,
    listArticles: ArticlesDefaultValues,
    updateArticle: UpdateArticleDefaultValues,
    sendMessage: SendMessageDefaultValues,
    addSubscriber: addSubscriberDefault,
    updateSubscriber: UpdateSubscriberDefaultValues,
    removeSubscriber: RemoveSubscriberDefaultValues
};
