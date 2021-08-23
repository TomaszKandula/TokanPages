import { INavigationContentDto } from "../../../Api/Models";

export interface IGetNavigationContent extends INavigationContentDto
{
    isLoading: boolean;
}