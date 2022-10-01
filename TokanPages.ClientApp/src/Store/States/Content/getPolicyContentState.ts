import { IDocumentContentDto } from "../../../Api/Models";

export interface IContentPolicy extends IDocumentContentDto
{
    isLoading: boolean;
}
