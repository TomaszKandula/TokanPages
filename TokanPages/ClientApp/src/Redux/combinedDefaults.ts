import { SelectArticleDefault } from "./Defaults/selectArticleDefault";
import { ListArticlesDefault } from "./Defaults/listArticlesDefault";
import { UpdateArticleDefault } from "./Defaults/updateArticleDefault";
import { SendMessageStateDefault } from "./Defaults/sendMessageDefault";
import { AddSubscriberDefault } from "./Defaults/addSubscriberDefault";
import { UpdateSubscriberDefault } from "./Defaults/updateSubscriberDefault";
import { RemoveSubscriberDefault } from "./Defaults/removeSubscriberDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { GetStaticContentDefault } from "./Defaults/getStaticContentDefault";

export const combinedDefaults = 
{
    raiseError: RaiseErrorDefault,
    selectArticle: SelectArticleDefault,
    listArticles: ListArticlesDefault,
    updateArticle: UpdateArticleDefault,
    sendMessage: SendMessageStateDefault,
    addSubscriber: AddSubscriberDefault,
    updateSubscriber: UpdateSubscriberDefault,
    removeSubscriber: RemoveSubscriberDefault,
    getStaticContent: GetStaticContentDefault
};
