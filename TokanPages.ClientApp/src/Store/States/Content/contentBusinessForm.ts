import { BusinessFormContentDto } from "../../../Api/Models";

export interface ContentBusinessFormState extends BusinessFormContentDto {
    isLoading: boolean;
}
