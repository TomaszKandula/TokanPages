import { UpdatePasswordContentDto } from "../../../Api/Models";

export interface ContentUpdatePasswordState extends UpdatePasswordContentDto
{
    isLoading: boolean;
}