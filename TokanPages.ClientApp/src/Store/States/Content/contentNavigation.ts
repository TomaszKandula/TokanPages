import { NavigationContentDto } from "../../../Api/Models";

export interface ContentNavigationState extends NavigationContentDto
{
    isLoading: boolean;
}