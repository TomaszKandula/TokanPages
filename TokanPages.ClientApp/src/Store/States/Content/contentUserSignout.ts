import { UserSignoutContentDto } from "../../../Api/Models";

export interface ContentUserSignoutState extends UserSignoutContentDto
{
    isLoading: boolean;
}