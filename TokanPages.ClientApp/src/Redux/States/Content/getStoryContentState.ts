import { IDocumentContentDto } from "../../../Api/Models";

export interface IGetStoryContent extends IDocumentContentDto
{
    isLoading: boolean;
}
