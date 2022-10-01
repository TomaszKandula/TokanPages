import { IHeaderContentDto } from "../../../Api/Models";

export interface IContentHeader extends IHeaderContentDto
{
    isLoading: boolean;
}