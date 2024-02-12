import { ActivateAccountContentDto } from "../../../Api/Models";

export interface ContentActivateAccountState extends ActivateAccountContentDto {
    isLoading: boolean;
}
