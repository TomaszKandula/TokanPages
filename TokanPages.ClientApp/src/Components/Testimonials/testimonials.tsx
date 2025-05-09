import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { TestimonialsView } from "./View/testimonialsView";

interface TestimonialsProps {
    background?: string;
}

export const Testimonials = (props: TestimonialsProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const testimonials = data?.components?.sectionTestimonials;

    return (
        <TestimonialsView
            isLoading={data?.isLoading}
            caption={testimonials?.caption}
            subtitle={testimonials?.subtitle}
            photo1={testimonials?.photo1}
            name1={testimonials?.name1}
            occupation1={testimonials?.occupation1}
            text1={testimonials?.text1}
            photo2={testimonials?.photo2}
            name2={testimonials?.name2}
            occupation2={testimonials?.occupation2}
            text2={testimonials?.text2}
            photo3={testimonials?.photo3}
            name3={testimonials?.name3}
            occupation3={testimonials?.occupation3}
            text3={testimonials?.text3}
            background={props.background}
        />
    );
};
