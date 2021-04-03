import { getDataFromUrl } from "../request";
import { ITestimonialsContentDto } from "../../Api/Models";
import { GET_TESTIMONIALS_CONTENT } from "../../Shared/constants";

export const getTestimonialsContent = async (): Promise<ITestimonialsContentDto> =>
{
    return await getDataFromUrl(GET_TESTIMONIALS_CONTENT);
};
