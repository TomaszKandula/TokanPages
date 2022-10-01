import { IAccountContentDto } from "../../../Api/Models";

export interface IContentAccount extends IAccountContentDto
{
    isLoading: boolean;
}
