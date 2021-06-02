import { IWrongPagePromptContentDto } from "../../Api/Models";

export interface IGetWrongPagePromptContent extends IWrongPagePromptContentDto
{
    isLoading: boolean;
}