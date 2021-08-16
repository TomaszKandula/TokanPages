import { IUserSignoutContentDto } from "../../Api/Models";

export interface IGetUserSignoutContent extends IUserSignoutContentDto
{
    isLoading: boolean;
}