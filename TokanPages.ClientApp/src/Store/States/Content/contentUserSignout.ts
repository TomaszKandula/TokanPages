import { IUserSignoutContentDto } from "../../../Api/Models";

export interface IContentUserSignout extends IUserSignoutContentDto
{
    isLoading: boolean;
}