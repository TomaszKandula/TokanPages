import { IWrongPagePromptContentDto } from "../../../Api/Models";

export interface IContentWrongPagePrompt extends IWrongPagePromptContentDto
{
    isLoading: boolean;
}