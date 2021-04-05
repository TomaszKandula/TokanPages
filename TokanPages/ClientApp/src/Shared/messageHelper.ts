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

export function GetMessageOutSuccess(): string
{
    return MESSAGE_OUT_SUCCESS;
}

export function GetMessageOutWarning(object: any): string
{
    return MESSAGE_OUT_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

export function GetMessageOutError(error: string): string
{
    return MESSAGE_OUT_ERROR.replace("{ERROR}", error);
}

// NEWSLETTERS

export function GetNewsletterSuccess(): string
{
    return NEWSLETTER_SUCCESS;
}

export function GetNewsletterWarning(object: any): string
{
    return NEWSLETTER_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

export function GetNewsletterError(error: string): string
{
    return NEWSLETTER_ERROR.replace("{ERROR}", error);
}

// SUBSCRIBERS

export function GetSubscriberOnDeleteError(error: string): string
{
    return SUBSCRIBER_DEL_ERROR.replace("{ERROR}", error);
}

// ARTICLES

export function GetUpdateArticleSuccess(): string
{
    return UPDATE_ARTICLE_SUCCESS;
}

export function GetUpdateArticleWarning(object: any): string
{
    return UPDATE_ARTICLE_WARNING.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

export function GetUpdateArticleError(error: string): string
{
    return UPDATE_ARTICLE_ERROR.replace("{ERROR}", error);
}

// OTHER

export function GetUnexpectedStatusCode(statusCode: number): string
{
    return UNEXPECTED_STATUS.replace("{ERROR}", statusCode.toString());
}
