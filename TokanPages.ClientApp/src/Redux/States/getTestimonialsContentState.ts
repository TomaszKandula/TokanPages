import { ITestimonialsContentDto } from "../../Api/Models";

export interface IGetTestimonialsContent extends ITestimonialsContentDto
{
    isLoading: boolean;
}