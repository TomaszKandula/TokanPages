import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { TestimonialsProps } from "./Types";
import { TestimonialsView } from "./View/testimonialsView";

export const Testimonials = (props: TestimonialsProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const testimonials = data?.components?.sectionTestimonials;

    return (
        <TestimonialsView
            isLoading={data?.isLoading}
            caption={testimonials?.caption}
            subtitle={testimonials?.subtitle}
            Testimonial1={{
                photo: testimonials?.photo1,
                name: testimonials?.name1,
                link: testimonials?.linkedIn1,
                occupation: testimonials?.occupation1,
                text: testimonials?.text1,
            }}
            Testimonial2={{
                photo: testimonials?.photo2,
                name: testimonials?.name2,
                link: testimonials?.linkedIn2,
                occupation: testimonials?.occupation2,
                text: testimonials?.text2,
            }}
            Testimonial3={{
                photo: testimonials?.photo3,
                name: testimonials?.name3,
                link: testimonials?.linkedIn3,
                occupation: testimonials?.occupation3,
                text: testimonials?.text3,
            }}
            className={props.className}
        />
    );
};
