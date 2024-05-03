import { DocumentContentDto } from "../../../Api/Models";

export interface ContentShowcaseState extends DocumentContentDto {
    isLoading: boolean;
}
