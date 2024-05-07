import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { TestimonialsView } from "./View/testimonialsView";

export const Testimonials = (): JSX.Element => {
    const testimonials = useSelector((state: ApplicationState) => state.contentTestimonials);

    const [hasTestimonialOne, setTestimonialOne] = React.useState(false);
    const [hasTestimonialTwo, setTestimonialTwo] = React.useState(false);
    const [hasTestimonialThree, setTestimonialThree] = React.useState(false);

    const buttonTestimonialOne = React.useCallback(() => {
        setTestimonialOne(!hasTestimonialOne);
    }, [hasTestimonialOne]);

    const buttonTestimonialTwo = React.useCallback(() => {
        setTestimonialTwo(!hasTestimonialTwo);
    }, [hasTestimonialTwo]);

    const buttonTestimonialThree = React.useCallback(() => {
        setTestimonialThree(!hasTestimonialThree);
    }, [hasTestimonialThree]);

    return (
        <TestimonialsView 
            isLoading={testimonials?.isLoading}
            hasTestimonialOne={hasTestimonialOne}
            hasTestimonialTwo={hasTestimonialTwo}
            hasTestimonialThree={hasTestimonialThree}
            buttonTestimonialOne={buttonTestimonialOne}
            buttonTestimonialTwo={buttonTestimonialTwo}
            buttonTestimonialThree={buttonTestimonialThree}
            caption={testimonials?.content?.caption}
            subtitle={testimonials?.content?.subtitle}
            photo1={testimonials?.content?.photo1}
            name1={testimonials?.content?.name1}
            occupation1={testimonials?.content?.occupation1}
            text1={testimonials?.content?.text1}
            photo2={testimonials?.content?.photo2}
            name2={testimonials?.content?.name2}
            occupation2={testimonials?.content?.occupation2}
            text2={testimonials?.content?.text2}
            photo3={testimonials?.content?.photo3}
            name3={testimonials?.content?.name3}
            occupation3={testimonials?.content?.occupation3}
            text3={testimonials?.content?.text3}
        />
    );
}
