import { IUpdatePasswordContentDto } from "../../../Api/Models";

export interface IGetUpdatePasswordContent extends IUpdatePasswordContentDto
{
    isLoading: boolean;
}