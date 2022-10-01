import { INavigationContentDto } from "../../../Api/Models";

export interface IContentNavigation extends INavigationContentDto
{
    isLoading: boolean;
}