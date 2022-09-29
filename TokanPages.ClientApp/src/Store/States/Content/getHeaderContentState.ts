import { IHeaderContentDto } from "../../../Api/Models";

export interface IGetHeaderContent extends IHeaderContentDto
{
    isLoading: boolean;
}