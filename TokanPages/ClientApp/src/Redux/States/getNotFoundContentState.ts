import { INotFoundContentDto } from "../../Api/Models";

export interface IGetNotFoundContent extends INotFoundContentDto
{
    isLoading: boolean;
}