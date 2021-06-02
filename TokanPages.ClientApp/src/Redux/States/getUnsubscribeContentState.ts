import { IUnsubscribeContentDto } from "../../Api/Models";

export interface IGetUnsubscribeContent extends IUnsubscribeContentDto
{
    isLoading: boolean;
}