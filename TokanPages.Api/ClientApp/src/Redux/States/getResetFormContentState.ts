import { IResetFormContentDto } from "../../Api/Models";

export interface IGetResetFormContent extends IResetFormContentDto
{
    isLoading: boolean;
}