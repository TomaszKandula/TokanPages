import { DocumentContentDto } from "../../../Api/Models";

export interface ContentPolicyState extends DocumentContentDto {
    isLoading: boolean;
}
