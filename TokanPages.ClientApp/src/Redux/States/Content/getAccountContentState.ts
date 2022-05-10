import { IAccountContentDto } from "../../../Api/Models";

export interface IGetAccountContent extends IAccountContentDto
{
    isLoading: boolean;
}
