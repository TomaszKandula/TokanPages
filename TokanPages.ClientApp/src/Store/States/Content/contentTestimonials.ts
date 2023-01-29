import { TestimonialsContentDto } from "../../../Api/Models";

export interface ContentTestimonialsState extends TestimonialsContentDto
{
    isLoading: boolean;
}