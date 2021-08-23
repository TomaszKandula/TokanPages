import { ConvertPropsToFields, HtmlRenderLines } from "./helpers";
import { UNEXPECTED_STATUS } from "../Shared/constants";

const ProduceSuccessText = (template: string = ""): string =>
{
    return template;
}

const ProduceWarningText = (object: any, template: string): string =>
{
    return template.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(object), "li"));
}

const ProduceErrorText = (error: string, template: string): string =>
{
    return template.replace("{ERROR}", error);
}

const UnexpectedStatusCode = (statusCode: number): string =>
{
    return UNEXPECTED_STATUS.replace("{STATUS_CODE}", statusCode.toString());
}

export 
{
    ProduceSuccessText,
    ProduceWarningText,
    ProduceErrorText,
    UnexpectedStatusCode
}
