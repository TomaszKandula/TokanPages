import { IArticleFeatContentDto } from "../../Api/Models";

export interface IGetArticleFeatContent extends IArticleFeatContentDto
{ 
    isLoading: boolean;
}