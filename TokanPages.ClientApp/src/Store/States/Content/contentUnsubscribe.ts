import { IUnsubscribeContentDto } from "../../../Api/Models";

export interface IContentUnsubscribe extends IUnsubscribeContentDto
{
    isLoading: boolean;
}