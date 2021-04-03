import { getDataFromUrl } from "../request";
import { ITestimonials } from "../../Api/Models";
import { GET_TESTIMONIALS_CONTENT } from "../../Shared/constants";

export const getTestimonialsContent = async (): Promise<ITestimonials> =>
{
    return await getDataFromUrl(GET_TESTIMONIALS_CONTENT);
};
