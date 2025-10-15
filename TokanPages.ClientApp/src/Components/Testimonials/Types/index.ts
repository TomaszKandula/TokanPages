import { ViewProperties } from "../../../Shared/Types";

export interface TestimonialsProps {
    className?: string;
}

export interface TestimonialItemProps {
    photo: string;
    name: string;
    link: string;
    occupation: string;
    text: string;
}

export interface TestimonialsViewProps extends ViewProperties {
    caption: string;
    subtitle: string;
    Testimonial1: TestimonialItemProps;
    Testimonial2: TestimonialItemProps;
    Testimonial3: TestimonialItemProps;
    className?: string;
}
