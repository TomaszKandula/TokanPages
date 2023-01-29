import { DocumentContentDto } from "../../../Api/Models";

export interface ContentStoryState extends DocumentContentDto
{
    isLoading: boolean;
}
