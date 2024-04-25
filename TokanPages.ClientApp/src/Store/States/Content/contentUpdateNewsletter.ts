import { UpdateNewsletterContentDto } from "../../../Api/Models";

export interface ContentUpdateNewsletterState extends UpdateNewsletterContentDto {
    isLoading: boolean;
}
