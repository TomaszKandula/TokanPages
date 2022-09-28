import { IDocumentContentDto } from "../../../Api/Models";

export interface IGetTermsContent extends IDocumentContentDto
{
    isLoading: boolean;
}
