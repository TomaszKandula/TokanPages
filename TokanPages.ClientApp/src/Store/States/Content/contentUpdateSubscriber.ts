import { UpdateSubscriberContentDto } from "../../../Api/Models";

export interface ContentUpdateSubscriberState extends UpdateSubscriberContentDto {
    isLoading: boolean;
}
