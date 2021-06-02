import { IFooterContentDto } from "../../Api/Models";

export interface IGetFooterContent extends IFooterContentDto
{
    isLoading: boolean;
}