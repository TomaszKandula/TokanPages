import { UnsubscribeContentDto } from "../../../Api/Models";

export interface ContentUnsubscribeState extends UnsubscribeContentDto {
    isLoading: boolean;
}
