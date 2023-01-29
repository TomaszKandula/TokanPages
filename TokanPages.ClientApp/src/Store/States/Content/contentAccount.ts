import { AccountContentDto } from "../../../Api/Models";

export interface ContentAccountState extends AccountContentDto
{
    isLoading: boolean;
}
