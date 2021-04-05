import { ConvertPropsToFields, HtmlRenderLines } from "./helpers";
import { 
    MESSAGE_OUT_ERROR, 
    MESSAGE_OUT_SUCCESS, 
    MESSAGE_OUT_WARNING, 
    NEWSLETTER_ERROR, 
    NEWSLETTER_SUCCESS,
    NEWSLETTER_WARNING,
    SUBSCRIBER_DEL_ERROR,
    UNEXPECTED_STATUS,
    UPDATE_ARTICLE_ERROR,
    UPDATE_ARTICLE_SUCCESS,
    UPDATE_ARTICLE_WARNING
} from "Shared/constants";

// EMAIL MESSAGES

const MessageOutSuccess = (): string =>
{
    return MESSAGE_OUT_SUCCESS;
}

const MessageOutWarning = (object: any): string =>
{
    return MESSAGE_OUT_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

const MessageOutError = (error: string): string =>
{
    return MESSAGE_OUT_ERROR.replace("{ERROR}", error);
}

// NEWSLETTERS

const NewsletterSuccess = (): string =>
{
    return NEWSLETTER_SUCCESS;
}

const NewsletterWarning = (object: any): string =>
{
    return NEWSLETTER_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

const NewsletterError = (error: string): string =>
{
    return NEWSLETTER_ERROR.replace("{ERROR}", error);
}

// SUBSCRIBERS

const SubscriberOnDeleteError = (error: string): string =>
{
    return SUBSCRIBER_DEL_ERROR.replace("{ERROR}", error);
}

// ARTICLES

const UpdateArticleSuccess = (): string =>
{
    return UPDATE_ARTICLE_SUCCESS;
}

const UpdateArticleWarning = (object: any): string =>
{
    return UPDATE_ARTICLE_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

const UpdateArticleError = (error: string): string =>
{
    return UPDATE_ARTICLE_ERROR.replace("{ERROR}", error);
}

// OTHER

const UnexpectedStatusCode = (statusCode: number): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", statusCode.toString());
}

export 
{
    MessageOutSuccess,
    MessageOutWarning,
    MessageOutError,
    NewsletterSuccess,
    NewsletterWarning,
    NewsletterError,
    SubscriberOnDeleteError,
    UpdateArticleSuccess,
    UpdateArticleWarning,
    UpdateArticleError,
    UnexpectedStatusCode
}
