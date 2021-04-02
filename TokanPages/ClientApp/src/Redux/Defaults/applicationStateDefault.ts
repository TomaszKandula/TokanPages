import { ArticleDefaultValues } from "./articleDefault";
import { ArticlesDefaultValues } from "./articlesDefault";
import { UpdateArticleDefaultValues } from "./updateArticleDefault";
import { SendMessageDefaultValues } from "./sendMessageDefault";
import { AddSubscriberDefaultValues } from "./addSubscriberDefault";
import { UpdateSubscriberDefaultValues } from "./updateSubscriberDefault";
import { RemoveSubscriberDefaultValues } from "./removeSubscriberDefault";

export const DefaultAppState = 
{
    selectArticle: ArticleDefaultValues,
    listArticles: ArticlesDefaultValues,
    updateArticle: UpdateArticleDefaultValues,
    sendMessage: SendMessageDefaultValues,
    addSubscriber: AddSubscriberDefaultValues,
    updateSubscriber: UpdateSubscriberDefaultValues,
    removeSubscriber: RemoveSubscriberDefaultValues
};
