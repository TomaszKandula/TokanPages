import { IDocumentContentDto } from "../../../Api/Models";

export interface IContentStory extends IDocumentContentDto
{
    isLoading: boolean;
}
