import { WrongPagePromptContentDto } from "../../../Api/Models";

export interface ContentWrongPagePromptState extends WrongPagePromptContentDto
{
    isLoading: boolean;
}